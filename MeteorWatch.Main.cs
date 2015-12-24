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
        #region Navigation button click events...

        /// <summary>
        /// Increments the index of a file inside the directory and 
        /// moves to a file at that index, unless it overflows, in 
        /// which case it moves to index 0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextLog_Click(object sender, EventArgs e)
        {
            scrollCaret = true;

            SaveUpdatedLogFile();

            // Move onto the next file..
            currentLogIndex++;
            Console.WriteLine("Incrementing currentLogIndex to: {0}", currentLogIndex);

            LoadLogFile(currentLogIndex);

            ClearScreenshots();

            UpdateCalendarCurrentDate();
        }

        /// <summary>
        /// Decrements the index of a file inside the directory and 
        /// moves to a file at that index, unless it overflows to -1, in 
        /// which case it moves to max index.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevLog_Click(object sender, EventArgs e)
        {
            scrollCaret = true;

            SaveUpdatedLogFile();

            // Move onto the previous file..
            currentLogIndex--;

            LoadLogFile(currentLogIndex);

            ClearScreenshots();

            UpdateCalendarCurrentDate();
        }

        private void btnPrevScreenshot_Click(object sender, EventArgs e)
        {
            scrollCaret = true;
            MoveToPreviousScreenshot();
        }

        private void btnNextScreenshot_Click(object sender, EventArgs e)
        {
            scrollCaret = true;
            MoveToNextScreenshot();
        }

        /// <summary>
        /// Log file has just been loaded, let us create an object responsible 
        /// for iterating images and highlighting lines...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSyncScreens_Click(object sender, EventArgs e)
        {
            bool success = LoadFirstScreenshot();

            EnableScreenshotControls(success);
            
            if (!success)
            {
                NoScreenshotsAction();
            }

            UpdateCalendarCurrentDate();
        }

        private void UpdateCalendarCurrentDate()
        {
            if (SetCurrentLogDate())
            {
                if (currentLogDate <= DateTime.Now)
                {                   
                    monthCalendar2.TodayDate = currentLogDate;
                }

                monthCalendar2.TitleBackColor = System.Drawing.Color.Blue;
                monthCalendar2.TrailingForeColor = System.Drawing.Color.Red;
                monthCalendar2.TitleForeColor = System.Drawing.Color.Yellow;
            }
        }

        #endregion

        #region Action button click events...

        private void richTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            scrollCaret = false;

            if (logsAndShots == null || logsAndShots.Screenshots.Length == 0)
            {
                MessageBox.Show("Click 'Sync' to check if screenshots for this log file are available...", "No images loaded...");
                return;
            }
            // Which line in the log file does it correspond to?
            int lineIndex = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);

            // Remove the corresponding line in the log file...
            string selectedLine = richTextBox1.Lines[lineIndex];

            GroupCollection groups = Regex.Match(selectedLine, @"(?<timestamp>\d{2}:\d{2}:\d{2}?),").Groups;

            TimeSpan timePart = TimeSpan.ParseExact(groups["timestamp"].Value.ToString(),
                "g", CultureInfo.CurrentCulture);

            string logName = Path.GetFileName(lblLogName.Text);

            GroupCollection groupsDate = Regex.Match(logName, @"log(?<datestamp>\d{8}?).txt").Groups;

            string shortDate = groupsDate["datestamp"].Value.ToString();

            // Parse selected text... 00:01:30,2,-87,-107.9,2239.8,2

            //event_log20140629.txt
            DateTime stamp = DateTime.ParseExact(shortDate, "yyyyMMdd", CultureInfo.CurrentCulture);
            stamp = stamp.Add(timePart);

            //20140606 - we need only 140606...
            shortDate = shortDate.Substring(2);

            // Loop for the screenshot...
            int delayIncrements = 0;
            string screenshotFile = string.Empty;
            string startImage = string.Empty;
            string endImage = string.Empty;
            bool fileFound = false;

            // Quite often the delay is further delayed by an extra second or two - let us not miss those entries.
            do
            {
                DateTime offset = stamp.AddSeconds(config.ScreenshotsDelay + delayIncrements);

                string timePlusOffset = string.Format("{0:D2}{1:D2}{2:D2}", offset.Hour, offset.Minute, offset.Second);

                screenshotFile = string.Format("event{0}{1}.jpg", shortDate, timePlusOffset);
                screenshotFile = Path.Combine(config.OriginalScreenshotsDirectory, screenshotFile);

                if (string.IsNullOrEmpty(startImage))
                {
                    startImage = screenshotFile;
                }

                if (File.Exists(screenshotFile))
                {
                    fileFound = true;
                    break;
                }
                else
                {
                    // Increment the number of attempts...
                    delayIncrements++;
                }
                endImage = screenshotFile;
            }
            while (delayIncrements <= 5);

            if (fileFound && SetScreenshotIndex(screenshotFile) > -1)
            {
                LoadScreenshotAndHighlightLogFile();
            }
            else
            {
                MessageBox.Show(string.Format("Searched in the file range from {0} to {1}. No matching image found for {2}.", startImage, endImage, selectedLine), "Image not found...");
            }
        }
        
        private void btnCopyScreenshot_Click(object sender, EventArgs e)
        {
            string copiedVersion = Path.Combine(config.CopiedScreenshotsDirectory, Path.GetFileName(lblScreenshotName.Text));

            if (File.Exists(lblScreenshotName.Text) &&
                Directory.Exists(config.CopiedScreenshotsDirectory) &&
                !File.Exists(copiedVersion))
            {
                File.Copy(lblScreenshotName.Text, copiedVersion);
            }
        }

        private void btnRemoveScreenshot_Click(object sender, EventArgs e)
        {
            bool carryOn = true;

            if (logsAndShots == null)
            {
                // Ask them to load a log file...
                MessageBox.Show("Please set the directory for log files...", "Please specify destination directory...");
                carryOn = false;
            }

            if (carryOn)
            {
                // What screenshot are we removing? Where from? Where to?
                File.Move(this.currentScreenshotFile, Path.Combine(config.RemovedScreenshotsDirectory, Path.GetFileName(this.currentScreenshotFile)));

                RemoveLineFromDisplayedLog();

                SaveUpdatedLogFile();

                // Update the screenshot count for the current log file...
                logsAndShots = null;
                logsAndShots = new DisplaySync(lblLogName.Text, config.OriginalScreenshotsDirectory);                

                LoadScreenshotAndHighlightLogFile();
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            Dictionary<int, int> currentDatesValues = SanitizeLogEntries();

            // Add the dictionary to our colorgram.
            // Char count of "event_log" is 9.
            if (SetCurrentLogDate())
            {
                if (colorgram.ContainsKey(currentLogDate))
                {
                    colorgram.Remove(currentLogDate);
                }

                colorgram.Add(currentLogDate, currentDatesValues);
                
                // TODO: add this date to the colourgram preview...
                AddCleansedDataToPreview();

                AddCleansedDataToRmobFile();

                SaveUpdatedLogFile();
            }
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = 0;
        }

        private int FindHighestCountThisMonth(DateTime dt)
        {
            int highestSoFar = 23;

            foreach (var datesAndValues in colorgram.Where(e => (e.Key.Month == dt.Month && e.Key.Year == dt.Year)))
            {
                var hourCount = datesAndValues.Value.First(m => m.Value == (datesAndValues.Value.Max(e => e.Value)));

                if (hourCount.Value > highestSoFar )
                {
                    highestSoFar = hourCount.Value;
                }
            }
            return highestSoFar;
        }


        private void btnDoneScreenshotValidation_Click(object sender, EventArgs e)
        {
            btnClean.Enabled = true;
        }


        #endregion

        #region Log file helpers...

        private bool SetCurrentLogDate()
        {
            if (!string.IsNullOrEmpty(lblLogName.Text))
            {
                string date = Regex.Match(lblLogName.Text,
                        string.Format(@"event_log(?<dateStamp>\d+$?)"))
                              .Groups["dateStamp"].Value;
                
                return (DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.None, out currentLogDate));
            }

            return false;
        }

        public void LoadLogFiles()
        {
            bool success = false;

            if (Directory.Exists(config.OriginalLogsDirectory))
            {
                // Load the first log file...
                success = LoadFirstLogFile();
            }
            EnableLogFileControls(success);
        }

        /// <summary>
        /// Displays the file in rich text box.
        /// </summary>
        /// <param name="fileIndex"></param>
        /// <returns>Empty string if there are no files</returns>  
        private string LoadLogFile(int fileIndex)
        {
            richTextBox1.SelectionStart = 0; 
            richTextBox1.SelectionLength = 0;
            
            richTextBox1.Text = String.Empty;

            // Moving onto a different log file...
            // Don't allow to clean until screenshots are checked.
            btnClean.Enabled = false;

            // Just starting a new file, reset the EOF indicator.
            reachedEOF = false;
            createNewSeries = true;

            string[] logFiles = Directory.GetFiles(config.OriginalLogsDirectory, "event_log20*.txt"); //.Where(file => file.StartsWith(".."));

            Console.WriteLine("Checking config.OriginalLogsDirectory at {0}", config.OriginalLogsDirectory);
            Console.WriteLine("Checking for 'event_log20*.txt' gives {0} files...", logFiles.Length);

            string logFileName = string.Empty;
            int totalLogs = logFiles.Length;

            if (totalLogs == 0)
            {                
                return logFileName;
            }

            try
            {
                // Reset the counter if overflows...
                currentLogIndex = Helper.ValidateCounter(config.OriginalLogsDirectory, currentLogIndex);

                // Display the log file position...
                lblFileCount.Text = string.Format("[{0} of {1}]", (currentLogIndex + 1), totalLogs);

                logFileName = logFiles[currentLogIndex];

                Console.WriteLine("Log at index[{0}] is: {1}", currentLogIndex, logFiles[currentLogIndex]);

                // Check if an updated version of this file exists..
                string updatedLogVersion = Path.Combine(config.UpdatedLogsDirectory, string.Format("updated_{0}", Path.GetFileName(logFileName)));

                if (File.Exists(updatedLogVersion))
                {
                    logFileName = updatedLogVersion;
                    Console.WriteLine("Using updated log: {0}", updatedLogVersion);
                }

                // Display its file name..
                lblLogName.Text = logFileName;
                //chartAnalysis.Titles[0].Text = string.Format("Trend analysis for {0}", logFileName);

                // Get the content of the log file.
                richTextBox1.Lines = File.ReadAllLines(logFileName);
            }
            catch
            {
                // TODO: Add log4net...
            }

            return logFileName;
        }

        private bool LoadFirstLogFile()
        {
            bool returnValue = false;
            currentLogIndex = 0;

            string firstLog = LoadLogFile(currentLogIndex);

            if (!string.IsNullOrEmpty(firstLog))
            {
                returnValue = true;
            }
            else
            {
                MessageBox.Show("Please specify an 'Original log file directory' containing log files...", "No log files found...");
            }

            return returnValue;
        }

        private void EnableLogFileControls(bool enableControls)
        {
            btnNextLog.Enabled = enableControls;
            btnPrevLog.Enabled = enableControls;
            btnSyncScreens.Enabled = enableControls;

            // This button will be dependent on the image files
            // being checked first...
            //btnClean.Enabled = enableControls;
        }

        private Dictionary<int, int> SanitizeLogEntries()
        {
            if (richTextBox1.Lines.Length == 0)
            {
                return null;
            }
            // Will contain hour/sequence pairs...
            Dictionary<int, KeyValuePair<int, bool>> currentDatesValues = new Dictionary<int, KeyValuePair<int, bool>>();
            int hour = 0, id = 0;
            int lastHour = -1, lastID = 0;
            int occurencesInHour = 0;
            DateTime timestamp = new DateTime();

            int firstHourOnRecord = SetFirstHourOnRecord(richTextBox1.Lines[0]);
            bool rolledOver = false;
            bool rolledOverCounted = false;

           // int.TryParse(richTextBox1.Lines[0].Split(':')[0], out firstHourOnRecord);

            foreach (string line in richTextBox1.Lines)
            {
                // Break up each line into a string array, then get
                // index 0 for [hour] and index 1 for [id] from 14:16:18,12,-93.4,-112.9,2335.8,2...
                string[] lineParts = line.Split(',');
                
                if (lineParts.Length > 2)
                {
                    try
                    {
                        // Get the meteor counter...
                        int.TryParse(lineParts[1], out id);

                        // See if this count overflows from the previous hour (due to 30 second delay)...
                        timestamp = DateTime.ParseExact(lineParts[0], "HH:mm:ss", CultureInfo.CurrentCulture);

                        // Only increments if we had a valid timestamp.
                        occurencesInHour++;                        

                        if (rolledOver)
                        {
                            rolledOverCounted = true;
                        }

                        rolledOver = false;

                        // There is a chance we've overflown, is the count low enough for that?
                        // Assumption: count always begins at 2.                        
                        if (timestamp.Minute == 59 && id < 3)
                        {
                            rolledOver = true;
                            // Carry this counter as the first one of the next hour,
                            // unless the hour is 23 already...
                            if (hour != 23)
                            {
                                hour = timestamp.AddHours(1).Hour;                                
                            }
                            else
                            {
                                // Preserve the previous id as last ID and hour count...
                                continue;
                            }
                        }
                        else
                        {
                            // Just roll on current hour.
                            hour = timestamp.Hour;
                        }

                        occurencesInHour = TotalUpLastHour(currentDatesValues, lastHour, lastID, occurencesInHour, timestamp, firstHourOnRecord, rolledOver, rolledOverCounted);

                        // Take note of the id and hour...
                        lastID = id;
                        lastHour = hour;
                    }
                    catch
                    {
                        // Must be a poorly formatted line that we are not interested in anyway.
                    }
                }
            }

            // Take care of the last hour's data.
            if (lastID >= occurencesInHour)
            {
                currentDatesValues.Add(lastHour, new KeyValuePair<int, bool>(occurencesInHour, rolledOverCounted));
            }

            if (currentDatesValues.Count > 0)
            {
                // Remove corrupt entries from the log file.
                RemoveCorruptHourlyData(currentDatesValues);
            }

            // Convert to a cut-down version of the dictionary...
            Dictionary<int, int> returnCollection = new Dictionary<int, int>();

            foreach(KeyValuePair<int, KeyValuePair<int, bool>> pairToCutDown in currentDatesValues)
            {
                returnCollection.Add(pairToCutDown.Key, pairToCutDown.Value.Key);
            }
            return returnCollection;
        }

        private int SetFirstHourOnRecord(string line)
        {
            int hour = 0, id = 0;

            string[] lineParts = line.Split(',');

            if (lineParts.Length > 2)
            {
                try
                {
                    // Get the meteor counter...
                    int.TryParse(lineParts[1], out id);

                    // See if this count overflows from the previous hour (due to 30 second delay)...
                    DateTime timestamp = DateTime.ParseExact(lineParts[0], "HH:mm:ss", CultureInfo.CurrentCulture);
                    hour = timestamp.Hour;

                    // There is a chance we've overflown, is the count low enough for that?
                    // Assumption: count always begins at 2.                        
                    if (timestamp.Minute == 59 && id < 3)
                    {
                        // Carry this counter as the first one of the next hour,
                        // unless the hour is 23 already...
                        if (hour != 23)
                        {
                            hour = timestamp.AddHours(1).Hour;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("There is a problem determining the first hour on record...", "Contact the dev team");
                }
            }
            return hour;
        }

        private static int TotalUpLastHour(Dictionary<int, KeyValuePair<int, bool>> currentDatesValues, int lastHour, int lastID, int occurencesInHour, DateTime timestamp, int firstHourOnRecord, bool rolledOver, bool rolledOverCounted)
        {
            // We are rounding off the count for the previous hour.
            if ((lastHour != timestamp.Hour && lastHour > -1) || rolledOver)
            {
                #region Comment
                // See if the number of meteor records matches the logged ids.
                // The two should be the same if there was no interruption in logging.
                // OR lastID will be higher if there was a manual line removal 
                // (such as a false positive recognised on a screenshot).
                #endregion
                //if (lastID >= occurencesInHour || ((lastHour == firstHourOnRecord) && (lastID == (occurencesInHour - 1))))
                if (rolledOver && lastHour == -1)
                {
                    // This has been left behind from the previous "clean"..
                }
                else if (lastID >= (occurencesInHour - 1))
                {
                    //int decrement = (lastHour == firstHourOnRecord ? 1 : 0);
                    // This period is NOT corrupt, add it to our data.
                    // Reduce occurrences by 1, since 1 meteor is ALWAYS logged on start up falsely.

                    KeyValuePair<int, bool> valuePair = new KeyValuePair<int, bool>(occurencesInHour - 1, rolledOverCounted);

                    currentDatesValues.Add(lastHour, valuePair); // - decrement);
                    rolledOverCounted = false;
                }
                else
                {
                    KeyValuePair<int, bool> valuePair = new KeyValuePair<int, bool>(-1, rolledOverCounted);

                    currentDatesValues.Add(lastHour, valuePair);
                    rolledOverCounted = false;
                }
                occurencesInHour = 1;
            }
            return occurencesInHour;
        }

        /// <summary>
        /// Removes a line corresponding to a sham meteor from the log file.
        /// </summary>
        private void RemoveLineFromDisplayedLog()
        {
            // Which line in the log file does it correspond to?
            int lineIndex = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart + 2);

            // Remove the corresponding line in the log file...
            string[] lineArray = richTextBox1.Lines;

            // Create a collection so that a line can be removed.
            var lineCollection = new List<string>(lineArray);

            // Remove the desired line.
            lineCollection.RemoveAt(lineIndex);

            // Convert the collection back to an array.
            lineArray = lineCollection.ToArray();

            // Display the new data in the control.
            richTextBox1.Lines = lineArray;
        }

        private void RemoveCorruptHourlyData(Dictionary<int, KeyValuePair<int, bool>> todaysData)
        {
            // Which line in the log file does it correspond to?
            int lineIndex = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);

            // Remove the corresponding line in the log file...
            string[] lineArray = richTextBox1.Lines;

            // Create a collection so that a line can be removed.
            var lineCollection = new List<string>(lineArray);

            // Remove all lines that are just zeros...
            lineCollection.RemoveAll(e => ((String)e).Trim() == "0");
            
            foreach (KeyValuePair<int, KeyValuePair<int, bool>> pair in todaysData)
            {
                if (pair.Value.Key == -1)
                {
                    string hour = string.Format("{0}:", pair.Key);
                    // Remove the corresponding lines.
                    lineCollection.RemoveAll(e => ((String)e).StartsWith(hour));

                    // Remove all corresponding screenshots...
                    RemoveCorrespondingScreenshots(hour);

                    // Check if the last line from the previous hour needs to be counted...
                    if (pair.Value.Value)
                    {
                        string hourBefore = (int.Parse(hour.Substring(0,2)) - 1).ToString();

                        if (lineCollection.Exists(e => ((String)e).StartsWith(hourBefore)))
                        {
                            string lastLineOfHour = lineCollection.Last(e => ((String)e).StartsWith(hourBefore));

                            lineCollection.Remove(lastLineOfHour);

                            // Remove all corresponding screenshots...
                            RemoveCorrespondingScreenshots(lastLineOfHour);
                        }
                    }
                }
            }

            // Convert the collection back to an array.
            lineArray = lineCollection.ToArray();

            // Display the new data in the control.
            richTextBox1.Lines = lineArray;
        }

        private void RemoveCorrespondingScreenshots(string hour)
        {
            // Make up the name of the screenshot...
            GroupCollection groups = Regex.Match(lblLogName.Text, @"(?<timestamp>\d{6}?).txt").Groups;

            if (groups["timestamp"].Value != null)
            {
                string fileNameBits = groups["timestamp"].Value.ToString();

                string screenshotNamePattern = string.Format("event{0}{1}", fileNameBits, hour.Replace(":", ""));

                // Go to screenshots directory...
                string[] screenshots = Directory.GetFiles(config.OriginalScreenshotsDirectory, string.Format("{0}*.jpg", screenshotNamePattern));

                if (screenshots.Length > 0)
                {
                    string directoryToMoveFilesTo = Path.Combine(config.RemovedScreenshotsDirectory, screenshotNamePattern);

                    if (!Directory.Exists(directoryToMoveFilesTo))
                    {
                        Directory.CreateDirectory(directoryToMoveFilesTo);
                    }

                    foreach (string screenshot in screenshots)
                    {
                        File.Move(screenshot, Path.Combine(directoryToMoveFilesTo, Path.GetFileName(screenshot)));
                    }
                }
            }
        }

        private void SaveUpdatedLogFile()
        {
            string updatedLogsDir = config.UpdatedLogsDirectory;

            if (!Directory.Exists(updatedLogsDir))
            {
                Directory.CreateDirectory(updatedLogsDir);
            }
            // Save the updated file in the specified "saved logs" directory.
            if (lblLogName.Text.StartsWith(config.UpdatedLogsDirectory))
            {
                // Use the updated version...
                File.WriteAllLines(lblLogName.Text, richTextBox1.Lines);
            }
            else
            {
                string savedLogFile = Path.Combine(updatedLogsDir, string.Format("updated_{0}", Path.GetFileName(lblLogName.Text)));

                // Show the user which file you are displaying...
                lblLogName.Text = savedLogFile;
                File.WriteAllLines(savedLogFile, richTextBox1.Lines);
            }
        }

        #endregion

        #region Screenshot helpers...

        public void LoadScreenshots()
        {
            bool result = false;

            if (Directory.Exists(config.OriginalScreenshotsDirectory))
            {
                result = LoadFirstScreenshot();
            }

            EnableScreenshotControls(result);
        }

        /// <summary>
        /// Displays the file in rich text box. Updates text of lblScreenshotName.
        /// Returns an empty string if there are no files, or the name of the file loaded.
        /// </summary>
        /// <param name="fileIndex"></param>
        /// <returns>Empty string if there are no files, or the name of the file loaded</returns>  
        private string LoadScreenshot(int fileIndex)
        {
            string[] shotFiles = logsAndShots.Screenshots;
            string shotFileName = string.Empty;
            int totalScreenshots = shotFiles.Length;

            if (totalScreenshots == 0)
            {
                return shotFileName;
            }

            try
            {
                // Reset the counter if overflows...
                currentScreenshotIndex = Helper.ValidateCounter(totalScreenshots, currentScreenshotIndex, out reachedEOF);

                // Display the screenshot file's position...
                lblScreenshotCount.Text = string.Format("[{0} of {1}]", (currentScreenshotIndex + 1), totalScreenshots);

                shotFileName = shotFiles[currentScreenshotIndex];

                // Display its file name...
                lblScreenshotName.Text = shotFileName;
                // Save the name of the dislayed screenshot...
                this.currentScreenshotFile = shotFileName;

                // Get the content of the screenshot file.
                Bitmap srcBitmap = (Bitmap)Image.FromFile(shotFileName);
                Rectangle rec = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);

                // Create the new bitmap and associated graphics object
                Bitmap bmp = new Bitmap(rec.Width, rec.Height);
                Graphics g = Graphics.FromImage(bmp);

                // Draw the specified section of the source bitmap to the new one
                g.DrawImage(srcBitmap, 0, 0, rec, GraphicsUnit.Pixel);
                g.Dispose();
                srcBitmap.Dispose();

                picBoxScreenshot.Image = bmp;
                picBoxScreenshot.Update();
            }
            catch
            {
                // TODO: Add log4net...
            }

            return shotFileName;
        }

        private int SetScreenshotIndex(string shotFileName)
        {
            string[] shotFiles = logsAndShots.Screenshots;
            int totalScreenshots = shotFiles.Length;

            if (totalScreenshots == 0)
            {
                return -1;
            }

            // Reset the counter if overflows...
            for (int i = 0; i < totalScreenshots; i++ )
            {
                if (shotFiles[i] == shotFileName)
                {
                    currentScreenshotIndex = i;
                    break;
                }
            }

            return currentScreenshotIndex;
        }

        private void ClearScreenshots()
        {
            if (logsAndShots == null)
            {
                return;
            }
            logsAndShots = null;

            // Reset the image...
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scatterthon));
            picBoxScreenshot.Image = ((System.Drawing.Image)(resources.GetObject("picBoxScreenshot.Image")));
            picBoxScreenshot.Update();

            // Display the screenshot file's position...
            lblScreenshotCount.Text = "";

            this.currentScreenshotFile = String.Empty;

            EnableScreenshotControls(false);
        }

        private bool LoadFirstScreenshot()
        {
            bool result = false;
            try
            {
                currentScreenshotIndex = 0;

                logsAndShots = new DisplaySync(lblLogName.Text, config.OriginalScreenshotsDirectory);

                if (logsAndShots != null && logsAndShots.Screenshots.Length > 0)
                {
                    result = LoadScreenshotAndHighlightLogFile();
                }
            }
            catch
            {
                // Signal that we are not set up for screenshot deletion...
                logsAndShots = null;
            }
            return result;
        }

        private void MoveToPreviousScreenshot()
        {
            // Move onto the previous file..
            currentScreenshotIndex--;

            LoadScreenshotAndHighlightLogFile();
        }

        private void MoveToNextScreenshot()
        {
            // Move onto the previous file..
            currentScreenshotIndex++;

            LoadScreenshotAndHighlightLogFile();
        }

        private void NoScreenshotsAction()
        {
            MessageBox.Show("There are no screenshots for this date...", "Sorry, no screenshots...");
            btnClean.Enabled = true;
        }

        /// <summary>
        /// Loads the requested screenshot and updates the displayed log file
        /// to highlight a line corresponding to that screenshot's timestamp.
        /// </summary>
        private bool LoadScreenshotAndHighlightLogFile()
        {
            string screenshotLoaded = LoadScreenshot(currentScreenshotIndex);

            if (string.IsNullOrEmpty(screenshotLoaded))
            {
                NoScreenshotsAction();
                return false;
            }
            else
            {
                string startSearch = string.Empty;
                string endSearch = string.Empty;

                // Take note of the current screenshot file...
                currentScreenshotFile = screenshotLoaded;

                richTextBox1.SelectionBackColor = SystemColors.Window;

                // Ensure the log file scrolls to the required location.
                GroupCollection groups = Regex.Match(screenshotLoaded,
                                         @"event\d{6}(?<hour>\d{2}?)(?<minutes>\d{2}?)(?<seconds>\d{2}$?)").Groups;

                string hour = groups["hour"].Value;
                string minutes = groups["minutes"].Value;
                string seconds = groups["seconds"].Value;

                string message = string.Empty;

                // Could this log entry be found in the previous log file?
                if (hour.StartsWith("00") && minutes.StartsWith("00"))
                {
                    message = "The matching log entry may belong to the previous date's log file..";
                }

                // Also save this value for stats graphing...
                SetDecimalTimestamp(hour, minutes);

                DateTime stamp = DateTime.ParseExact(hour + minutes + seconds, "HHmmss", CultureInfo.CurrentCulture);

                int delayIncrements = 0;
                int offsetIndex = 0;
                int endOfHighlight = 0;

                // Quite often the delay is further delayed by an extra second or two - let us not miss those entries.
                do
                {
                    DateTime offset = stamp.AddSeconds(-(config.ScreenshotsDelay + delayIncrements));

                    string searchString = string.Format("{0:D2}:{1:D2}:{2:D2}", offset.Hour, offset.Minute, offset.Second);

                    if (string.IsNullOrEmpty(startSearch))
                    {
                        startSearch = searchString;
                    }

                    offsetIndex = richTextBox1.Text.IndexOf(searchString);

                    if (richTextBox1.TextLength < (offsetIndex + 40))
                    {
                        endOfHighlight = richTextBox1.TextLength;
                        reachedEOF = true;                        
                        break;
                    }
                    else if (offsetIndex > -1)
                    {
                        endOfHighlight = richTextBox1.Text.IndexOf("\n", offsetIndex, 40);                        
                        break;
                    }
                    else
                    {
                        // Increment the number of attempts...
                        delayIncrements++;
                    }
                    endSearch = searchString;
                }
                while (delayIncrements <= 5);

                if (offsetIndex > -1)
                {
                    if (offsetIndex > 1)
                    {
                        richTextBox1.Select(offsetIndex - 1, endOfHighlight - offsetIndex + 1);
                    }
                    else
                    {
                        richTextBox1.Select(offsetIndex, endOfHighlight - offsetIndex);
                    }
                    richTextBox1.SelectionBackColor = Color.LightGray;
                    richTextBox1.Update();
                    richTextBox1.Refresh();

                    if (scrollCaret)
                    {
                        richTextBox1.ScrollToCaret();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        MessageBox.Show(string.Format("No match found with log timestamps between {1} and {0}. Unless this entry was manually removed, please report this as an issue.", startSearch, endSearch), "Log entry not found...");
                    }
                    else
                    {
                        MessageBox.Show(string.Format("No match found with log timestamps between {1} and {0}. {2}", startSearch, endSearch, message), "Log entry not found...");
                    }
                }
            }
            return true;
        }

        private void EnableScreenshotControls(bool result)
        {
            btnNextScreenshot.Enabled = result;
            btnPrevScreenshot.Enabled = result;
            btnDoneScreenshotValidation.Enabled = result;

            if (result && !string.IsNullOrEmpty(lblLogName.Text))
            {
                btnRemoveScreenshot.Enabled = true;
            }
            else
            {
                btnRemoveScreenshot.Enabled = false;
            }

            if (result && Directory.Exists(config.CopiedScreenshotsDirectory))
            {
                btnCopyScreenshot.Enabled = true;
            }
            else
            {
                btnCopyScreenshot.Enabled = false;
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
