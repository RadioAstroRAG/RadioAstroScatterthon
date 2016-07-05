using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeteorWatch
{
    public partial class Scatterthon
    {
        bool doNotShowMissingImageWarning = false;

        private void btnCopyScreenshot_Click(object sender, EventArgs e)
        {
            if (lblScreenshotName.Text.StartsWith("SCREENSHOT"))
            {
                btnCopyScreenshot.Enabled = false;
                return;
            }

            string copiedVersion = Path.Combine(config.HighResolutionScreenshotsDirectory, Path.GetFileName(lblScreenshotName.Text));

            if (File.Exists(lblScreenshotName.Text) &&
                Directory.Exists(config.HighResolutionScreenshotsDirectory) &&
                !File.Exists(copiedVersion))
            {
                File.Copy(lblScreenshotName.Text, copiedVersion);
            }

            DisplayHighResolutionThumbnails();
        }

        private void ProcessRmobFileData(string[] logViewerContent, string logFileName, bool useVirtualRmobDate)
        {
            Dictionary<int, int> currentDatesValues = SumUpTimePeriods(logViewerContent);

            bool addVisuals = false;
            DateTime rmobDate = new DateTime();

            // Are we called from navigation's "Next" button click in "Cleanse" tab, or from "recreate" options in RMOB tab?
            // If logFileName the same as the label, then it's the former...
            if (!useVirtualRmobDate && GetCurrentLogDate(logFileName, out currentLogDate))
            {
                // Set current log date member along the way...
                rmobDate = currentLogDate;
                addVisuals = true;
            }
            else if (GetCurrentLogDate(logFileName, out currentVirtualRmobDate))
            {
                rmobDate = currentVirtualRmobDate;
                addVisuals = false;
            }
            else
            {
                MessageBox.Show(string.Format("Could not process file name {0}", logFileName), "Error...");
                return;
            }

            if (colorgram.ContainsKey(rmobDate))
            {
                colorgram.Remove(rmobDate);
            }

            // Add the dictionary to our colorgram.
            // Char count of "event_log" is 9.            
            colorgram.Add(rmobDate, currentDatesValues);

            if (addVisuals)
            {
                // TODO: add this date to the colourgram preview...
                AddCleansedDataToRmobPreview();
            }

            AddCleansedDataToRmobFile(rmobDate);
        }

        private int FindHighestCountThisMonth(DateTime dt)
        {
            int highestSoFar = 23;

            foreach (var datesAndValues in colorgram.Where(e => (e.Key.Month == dt.Month && e.Key.Year == dt.Year)))
            {
                if (datesAndValues.Value.Count > 0)
                {
                    var hourCount = datesAndValues.Value.First(m => m.Value == (datesAndValues.Value.Max(e => e.Value)));

                    if (hourCount.Value > highestSoFar)
                    {
                        highestSoFar = hourCount.Value;
                    }
                }
            }
            return highestSoFar;
        }

        #region Log file helpers...

        private bool GetCurrentLogDate(string fileName, out DateTime logDate)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                logDate = new DateTime();
                return false;
            }
            else
            {
                string date = Regex.Match(fileName,
                        string.Format(@"event_log(?<dateStamp>\d+$?)"))
                              .Groups["dateStamp"].Value;

                return (DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.None, out logDate));
            }
        }

        public void LoadLogFiles()
        {
            if (config != null && Directory.Exists(config.OriginalLogsDirectory))
            {
                // Load the first log file...
                LoadFirstLogFile();
            }
        }

        /// <summary>
        /// Displays the file in rich text box.
        /// </summary>
        /// <param name="fileIndex"></param>
        /// <returns>Empty string if there are no files</returns>  
        private string LoadLogFile()
        {
            string[] logFiles = Directory.GetFiles(config.OriginalLogsDirectory, "event_log20*.txt"); //.Where(file => file.StartsWith(".."));                     

            Console.WriteLine("Checking config.OriginalLogsDirectory at {0}", config.OriginalLogsDirectory);
            Console.WriteLine("Checking for 'event_log20*.txt' gives {0} files...", logFiles.Length);

            string logFileName = string.Empty;
            totalLogFiles = logFiles.Length;

            if (totalLogFiles == 0)
            {
                return logFileName;
            }

            try
            {
                logFileName = logFiles[currentLogIndex];
                currentLogFileName = Path.GetFileNameWithoutExtension(logFileName);

                Console.WriteLine("Log at index[{0}] is: {1}", currentLogIndex, logFiles[currentLogIndex]);

                logFileName = SelectFileVersionAndUpdateLabels(logFileName);

                // Console.WriteLine("Using updated log: {0}", updatedLogVersion);
                logFileComponent.LoadLogFileContent(logFileName, config.OriginalScreenshotsDirectory, config.ScreenshotsDelay, checkShowSignal.Checked, checkShowNoise.Checked, checkShowFreq.Checked);
                logFileComponent.SelectFirstRow();

                // Take note of the log's date for RMOB preview tab...
                currentRmobDate = (DateTime)logFileComponent.GetCurrentLogDate(logFileName);

                // If we are at the end of the month, load the next month's data...
                if (currentRmobDate.Day == 1 || currentRmobDate.Day == DateTime.DaysInMonth(currentRmobDate.Year, currentRmobDate.Month))
                {
                    string rmobFileToLoad;

                    MakeRmobFileName(false, out rmobFileToLoad);

                    LoadRmobFile(rmobFileToLoad);
                }

                SetScreenshotPageControls();

                //chartAnalysis.Titles[0].Text = string.Format("Trend analysis for {0}", logFileName);
            }
            catch (Exception ex)
            {
                // TODO: Add log4net...
            }

            return logFileName;
        }

        private string SelectFileVersionAndUpdateLabels(string logFileName)
        {
            string shortLogFileName = Path.GetFileNameWithoutExtension(logFileName);

            // Check if an updated version of this file exists..
            string updatedLogVersion = Path.Combine(config.UpdatedLogsDirectory,
                                                    string.Format("{0}_saved.txt",
                                                    shortLogFileName));

            if (File.Exists(updatedLogVersion))
            {
                // Update the temp variable...
                logFileName = updatedLogVersion;
            }
            
            // Update the more permanent reference...
            currentLogFileName = Path.GetFileNameWithoutExtension(logFileName);

            // Update the displayed file name...
            lblLogName.Text = logFileName;

            return logFileName;
        }

        private void SetScreenshotPageControls()
        {
            bool configLoading = (startup || (totalLogFiles == 0 && !string.IsNullOrEmpty(config.OriginalLogsDirectory)));
            
            string screenshotName = lblScreenshotName.Text.StartsWith("SCREENSHOT") ? "" : Path.GetFileNameWithoutExtension(lblScreenshotName.Text);

            if (string.IsNullOrEmpty(screenshotName))
            {
                btnCopyScreenshot.Enabled = false;
            }
            else
            {
                btnCopyScreenshot.Enabled = true;
            }

            // int fileOrder = configLoading ? currentLogIndex : currentLogIndex + 1;
            int fileOrder = currentLogIndex + 1;

            // Display the log file position...
            lblLogScroll.Text = string.Format("{0} [index {1} of {2}]... {3}", currentLogFileName, fileOrder, totalLogFiles, screenshotName);
        }

        private void LoadFirstLogFile()
        {
            currentLogIndex = 0;

            string firstLog = LoadLogFile();

            if (string.IsNullOrEmpty(firstLog))
            {
                MessageBox.Show("Please specify an 'Original log file directory' containing log files...", "No log files found...");
            }
            else
            {
                // Log file loaded, so we can enable navigation...
                btnNextLog.Enabled = true;
            }
        }

        private Dictionary<int, int> SumUpTimePeriods(string[] processedLogLines)
        {
            // Will contain hour/sequence pairs...
            Dictionary<int, int> currentDatesValues = new Dictionary<int, int>();
            string id = "";
            int hour = -1;           
            int mainID = 0;
            int nextHour = 0;                          
            int occurencesInHour = 0;            
            int numberOfSplitRecords = 0;
            int wrongClassEvents = 0;

            DateTime timestamp = new DateTime();

            string currentClass = string.Empty;

            for (int i = 0; i < processedLogLines.Length; i++ )
            {
                string line = processedLogLines[i];

                // Break up each line into a string array, then get
                // index 0 for [hour] and index 1 for [id] from 14:16:18,12,-93.4,-112.9,2335.8,2...
                if (line.Contains(','))
                {
                    string[] lineParts = line.Split(',');

                    if (lineParts.Length > 2)
                    {
                        try
                        {
                            int idPreview = 0;
                            id = lineParts[1];
                            currentClass = lineParts[6];

                            // Get the meteor counter...
                            int.TryParse(lineParts[1], out idPreview);

                            if (idPreview == 0 && id.Contains("."))
                            {
                                // This is a follow up of our split operation..
                                numberOfSplitRecords++;
                            }

                            if (currentClass != string.Empty && currentClass != "Meteor trail" && currentClass != "Head echo")
                            {
                                // We will subtract these afterwards...
                                wrongClassEvents++;
                            }

                            // See if this count overflows from the previous hour (due to 30 second delay)...
                            timestamp = DateTime.ParseExact(lineParts[0], "HH:mm:ss", CultureInfo.CurrentCulture);

                            if (hour == -1) { hour = timestamp.Hour; }

                            occurencesInHour++;

                            string idForParsing = (id.Contains(".") ? (id.Substring(0, id.Length - id.IndexOf(".") - 1)) : id);

                            int.TryParse(idForParsing, out mainID);

                            if (i < processedLogLines.Length - 1)
                            {
                                nextHour = GetNextHour(processedLogLines[i + 1], hour);
                            }

                            if (hour == 23)
                            {
                                // Preserve the previous id as last ID and hour count...
                                continue;
                            }

                            if (nextHour != hour)
                            {
                                TotalUpLastHour(currentDatesValues, timestamp.Hour, mainID, numberOfSplitRecords, occurencesInHour, wrongClassEvents);
                                
                                // Reset...
                                occurencesInHour = 0;
                                numberOfSplitRecords = 0;
                                wrongClassEvents = 0;
                                hour = nextHour;
                            }
                        }
                        catch
                        {
                            // Must be a poorly formatted line that we are not interested in anyway.
                        }
                    }
                }
            }

            // Take care of the last hour's data.
            if ((mainID + numberOfSplitRecords) >= occurencesInHour)
            {
                currentDatesValues.Add(hour, occurencesInHour - wrongClassEvents);
            }

            return currentDatesValues;
        }

        private static int GetNextHour(string line, int currentHour)
        {
            if (!line.Contains(','))
            {
                return currentHour;
            }

            string[] partsOfLine = line.Split(',');

            DateTime time = new DateTime(1, 1, 1, currentHour, 0, 0);

            if (partsOfLine.Length > 0)
            {
                time = DateTime.ParseExact(partsOfLine[0], "HH:mm:ss", CultureInfo.CurrentCulture);
            }
            // If the parsing failed, return current hour...
            return time.Hour;
        }

        private static void TotalUpLastHour(Dictionary<int, int> currentDatesValues, int lastHour, int lastID, int numberOfSplits, int occurencesInHour, int eventsToRemove)
        {
            #region Comment
            // See if the number of meteor records matches the logged ids.
            // The two should be the same if there was no interruption in logging.
            // OR lastID will be higher if there was a manual line removal 
            // (such as a false positive recognised on a screenshot).
            #endregion
                
            if ((lastID + numberOfSplits) >= (occurencesInHour))
            {
                // This period is NOT corrupt, add it to our data.
                currentDatesValues.Add(lastHour, occurencesInHour - eventsToRemove); 
            }
            else
            {
                // This period must have had discontinuously logged data...
                currentDatesValues.Add(lastHour, -1);
            }
        }

        #endregion

        #region Screenshot helpers...

        /// <summary>
        /// Displays the file in rich text box. Updates text of lblScreenshotName.
        /// Returns an empty string if there are no files, or the name of the file loaded.
        /// </summary>
        /// <param name="fileIndex"></param>
        /// <returns>Empty string if there are no files, or the name of the file loaded</returns>  
        private string LoadScreenshotInPictureBox(string pathToScreenshot, PictureBox picBox)
        {
            try
            {
                if (picBox.Name == "picBoxScreenshot")
                {
                    // Enable the "copy" button...
                    btnCopyScreenshot.Enabled = true;

                    // Display its file name...
                    lblScreenshotName.Text = pathToScreenshot;
                    // Save the name of the dislayed screenshot...
                    this.currentScreenshotFile = pathToScreenshot;

                    SetScreenshotPageControls();
                }

                if (!File.Exists(pathToScreenshot) && !doNotShowMissingImageWarning)
                {
                    if (MessageBox.Show("Screenshot does not exist at this location... Do not show this warning again?", "Missing screenshot", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        doNotShowMissingImageWarning = true;
                    }
                }
                else
                {
                    picBox.Image = CreateBitmap(pathToScreenshot);
                    picBox.ImageLocation = pathToScreenshot;
                    picBox.Update();
                }
            }
            catch
            {
                // TODO: Add log4net...
            }

            return pathToScreenshot;
        }

        private void ClearScreenshots()
        {
            // Reset the image...
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scatterthon));
            picBoxScreenshot.Image = ((System.Drawing.Image)(resources.GetObject("picBoxScreenshot.Image")));
            // picBoxScreenshot.BackColor = Color.Black;
            picBoxScreenshot.Update();

            // Display the screenshot file's position...
            lblScreenshotCount.Text = "";

            this.currentScreenshotFile = String.Empty;
            this.lblScreenshotName.Text = "SCREENSHOT: Double-click \'Original screenshots directory\' in Config tab to set screenshots\' folder...";

            SetScreenshotPageControls();
        }


        private void btnImageScrollLeft_Click(object sender, EventArgs e)
        {
            if (hrStartOfDisplayedArray > 0)
            {
                btnImageScrollRight.Enabled = true;
                hrStartOfDisplayedArray--;
                ScrollThumbnails(false);
            }
            else
            {
                btnImageScrollLeft.Enabled = false;
            }
        }

        private void btnImageScrollRight_Click(object sender, EventArgs e)
        {
            if (hrStartOfDisplayedArray < capturesWithinInterval.Count)
            {
                btnImageScrollLeft.Enabled = true;
                hrStartOfDisplayedArray++;
                ScrollThumbnails(true);
            }
            else
            {
                btnImageScrollRight.Enabled = false;
            }
        }

        private void DisplayHighResolutionThumbnails()
        {
            // TODO: Figure out which screenshots belong to the selected interval...
            if (!string.IsNullOrEmpty(config.HighResolutionScreenshotsDirectory))
            {
                // For now just take all the ones we find...
                string[] captures = Directory.GetFiles(config.HighResolutionScreenshotsDirectory, string.Format("*.jpg"));

                numberOfCaptures = captures.Length;

                capturesWithinInterval.Clear();
                capturesWithinInterval.AddRange(captures.ToArray());

                if (numberOfCaptures <= 3)
                {
                    switch (numberOfCaptures)
                    {
                        case 1:
                            LoadScreenshotInPictureBox(capturesWithinInterval[0], picBoxHighRes1);
                            break;
                        case 2:
                            LoadScreenshotInPictureBox(capturesWithinInterval[0], picBoxHighRes1);
                            LoadScreenshotInPictureBox(capturesWithinInterval[1], picBoxHighRes2);
                            break;
                        case 3:
                            LoadScreenshotInPictureBox(capturesWithinInterval[0], picBoxHighRes1);
                            LoadScreenshotInPictureBox(capturesWithinInterval[1], picBoxHighRes2);
                            LoadScreenshotInPictureBox(capturesWithinInterval[2], picBoxHighRes3);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    LoadScreenshotInPictureBox(capturesWithinInterval[0], picBoxHighRes1);
                    LoadScreenshotInPictureBox(capturesWithinInterval[1], picBoxHighRes2);
                    LoadScreenshotInPictureBox(capturesWithinInterval[2], picBoxHighRes3);
                }
            }
        }

        private void ScrollThumbnails(bool moveRight)
        {
            if (hrStartOfDisplayedArray >= 0 && capturesWithinInterval.Count > hrStartOfDisplayedArray)
            {
                LoadScreenshotInPictureBox(capturesWithinInterval[hrStartOfDisplayedArray], picBoxHighRes1);

                // Do a check on second image box...
                if (hrStartOfDisplayedArray + 1 < capturesWithinInterval.Count)
                {
                    LoadScreenshotInPictureBox(capturesWithinInterval[hrStartOfDisplayedArray + 1], picBoxHighRes2);
                }
                else if (picBoxHighRes2.Image != null)
                {
                    picBoxHighRes2.Image.Dispose();
                    picBoxHighRes2.Image = null;
                    picBoxHighRes2.Update();
                }

                // Do a check on third image box...
                if (hrStartOfDisplayedArray + 2 < capturesWithinInterval.Count)
                {
                    LoadScreenshotInPictureBox(capturesWithinInterval[hrStartOfDisplayedArray + 2], picBoxHighRes3);
                }
                else if (picBoxHighRes3.Image != null)
                {
                    picBoxHighRes3.Image.Dispose();
                    picBoxHighRes3.Image = null;
                    picBoxHighRes3.Update();
                }
            }
            else
            {
                hrStartOfDisplayedArray--;
                btnImageScrollLeft.Enabled = true;
                btnImageScrollRight.Enabled = false;
            }
        }

        #endregion

        #region General helpers...

        private void SetDecimalTimestamp(string hour, string minutes)
        {
            // Convert minutes to decimals...
            decimal minutesDecimal = Math.Round((decimal)(double.Parse(minutes) * 100 / 60));

            timestampDecimal = decimal.Parse(hour) + (minutesDecimal / (decimal)100);
        }

        #endregion

    }
}
