using LogComponent;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MeteorWatch
{
    partial class Scatterthon
    {
        #region Button click events... 
        
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string rmobFileName = "";
            currentVirtualRmobDate = dateTimePicker2.Value;
            MakeRmobFileName(true, out rmobFileName);
            LoadRmobFile(rmobFileName);
            RedrawPreview(currentVirtualRmobDate);
        }

        private string MakeRmobFileName(bool useVirtual, out string fileName)
        {
            string dateStamp = string.Empty;

            if (!useVirtual && currentLogDate == DateTime.MinValue)
            {
                dateStamp = string.Format("{0:MMM}_{1}", currentRmobDate, currentRmobDate.Year);
            }
            else 
            {
                // If we are regenerating rmob files automatically right now...
                if (useVirtual)
                {
                    dateStamp = string.Format("{0:MMM}_{1}", currentVirtualRmobDate, currentVirtualRmobDate.Year);
                }
                else // We are just stepping along through log files manually, one at a time...
                {
                    if (currentLogDate == DateTime.MinValue)
                    {
                        GetCurrentLogDate(currentLogFileName, out currentLogDate);
                    }
                    dateStamp = string.Format("{0:MMM}_{1}", currentLogDate, currentLogDate.Year);
                }
            }
            
            fileName = Path.Combine(config.RmobFilesDirectory, string.Format("rmob_{0}.txt", dateStamp));

            return dateStamp;
        }

        private void btnExportRmobFile_Click(object sender, EventArgs e)
        {
            if (this.rmobFiles.Count == 0)
            {
                MessageBox.Show("There is no content to export..", "Not enough data");
                return;
            }
            string newFileName = SaveRmobFile(false);

            MessageBox.Show(string.Format("RMOB file saved as {0}...", newFileName), "File Saved as...");
        }

        private string SaveRmobFile(bool automaticMode)
        {
            string dateStamp, newFileName;

            dateStamp = MakeRmobFileName(automaticMode, out newFileName);

            string allText = this.rmobFiles[dateStamp].MakeFile();

            File.WriteAllText(newFileName, allText);
            return newFileName;
        }
        #endregion

        #region Helpers...
        //private void BoldenCleansedLogDate()
        //{
        //    DateTime[] datesSoFar = monthCalendar2.BoldedDates;

        //    int requiredLength = datesSoFar.Length + 1;
        //    Array.Resize(ref datesSoFar, requiredLength);
        //    datesSoFar[requiredLength - 1] = currentLogDate;

        //    monthCalendar2.BoldedDates = datesSoFar;
        //}
        
        private void AddCleansedDataToRmobFile(DateTime rmobDate)
        {
            string monthStamp = string.Format("{0:MMM}_{1}", rmobDate, rmobDate.Year);

            if (!rmobFiles.ContainsKey(monthStamp))
            {
                rmobFiles.Add(monthStamp, new RmobFile(rmobDate));
            }

            rmobFiles[monthStamp].SetDataForDay(rmobDate, colorgram[rmobDate]);

            // BoldenCleansedLogDate();
        }

        //private void AddBoldDatesForExistingCleansedFiles()
        //{
        //    try
        //    {
        //        // Check for files in the specified directory.
        //        string cleansedDirectory = config.UpdatedLogsDirectory;

        //        if (Directory.Exists(cleansedDirectory))
        //        {
        //            string[] files = Directory.GetFiles(cleansedDirectory, "updated_event_log20*.txt");

        //            DateTime[] cleansedDatesArray = monthCalendar2.BoldedDates;
        //            Array.Resize(ref cleansedDatesArray, files.Length);

        //            for (int fileIndex = 0; fileIndex < files.Length; fileIndex++)
        //            {
        //                string fileName = Path.GetFileName(files[fileIndex]);

        //                GroupCollection groups = Regex.Match(fileName, @"(?<timestamp>\d{8}?).txt").Groups;

        //                DateTime timePart = DateTime.ParseExact(groups["timestamp"].Value.ToString(),
        //                    "yyyyMMdd", CultureInfo.CurrentCulture);

        //                cleansedDatesArray[fileIndex] = timePart;

        //                //string dateString = fileName.Substring(17, 8);

        //                // Parse it to extract the date..
        //                //DateTime cleansedDate = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.CurrentCulture);
        //                //cleansedDatesArray[fileIndex] = cleansedDate;
        //            }

        //            monthCalendar2.BoldedDates = cleansedDatesArray;
        //        }
        //    }
        //    catch { }
        //}
        #endregion

        private bool GetRmobData(string tempFile, out DateTime monthAndYear, out Dictionary<DateTime, Dictionary<int, int>> hoursAndCounts)
        {
            string fileName = Path.GetFileName(tempFile);

            monthAndYear = new DateTime();
            hoursAndCounts = new Dictionary<DateTime, Dictionary<int, int>>();

            if (File.Exists(tempFile))
            {
                GroupCollection groups = Regex.Match(fileName, @"rmob_(?<month>.{3}?)_(?<year>\d{4}?).txt").Groups;

                if (groups != null)
                {
                    string month = groups["month"].Value;
                    string year = groups["year"].Value;

                    string monthStamp = string.Format("{0}_{1}", month, year);

                    monthAndYear = DateTime.ParseExact(month + year, "MMMyyyy", CultureInfo.CurrentCulture);

                    RmobFile monthsData = new RmobFile(monthAndYear);

                    monthsData.LoadFile(tempFile);

                    hoursAndCounts = monthsData.GetMonthsData();

                    if (rmobFiles.ContainsKey(monthStamp))
                    {
                        // We'll over-write the old one...
                        rmobFiles.Remove(monthStamp);
                    }
                    rmobFiles.Add(monthStamp, monthsData);
                }
                return true;
            }
            return false;
        }

        private void DisplayRmobDataFromFile()
        {
            string directory = config.RmobFilesDirectory;

            if (Directory.Exists(directory))
            {
                openFileDialog1.InitialDirectory = directory;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.AutoUpgradeEnabled = false;
            }

            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                string tempFile = openFileDialog1.FileName;

                if (File.Exists(tempFile))
                {
                    LoadRmobFile(tempFile);
                }
            }
        }

        private void LoadRmobFile(string tempFile)
        {
            // returns DateTime and the data for that month...
            // as a KeyValuePair<DateTime, Dictionary<int, int>> of dates, hours and counts
            DateTime monthAndYear;
            Dictionary<DateTime, Dictionary<int, int>> hoursAndCounts;

            if (GetRmobData(tempFile, out monthAndYear, out hoursAndCounts))
            {
                foreach (KeyValuePair<DateTime, Dictionary<int, int>> pair in hoursAndCounts)
                {
                    if (colorgram.ContainsKey(pair.Key))
                    {
                        colorgram.Remove(pair.Key);
                    }
                    colorgram.Add(pair.Key, pair.Value);
                }
                RedrawPreview(monthAndYear);
            }
        }

        private void RedrawPreview(DateTime monthAndYear)
        {
            currentRmobDate = monthAndYear;

            List<Color> colorList = ProcessHighestCountValue(monthAndYear);

            if (colorList != null)
            {              
                DrawGridMulticolorScales();

                AddColorgramDaysToMonthPreview(monthAndYear, colorList);
            }
        }

        private void DrawGridMulticolorScales()
        {
            List<Color> colorList = PrepareListOfMultiColours(24);

            // Paint the scales...
            for (int i = 0; i < 24; i++)
            {
                dataGridView1.Rows[i].Cells[32].Style.BackColor = colorList[i];
            }
        }

        private void AddCleansedDataToRmobPreview()
        {
            if (currentLogDate == null)
            {
                return;
            }

            List<Color> colorList = ProcessHighestCountValue(currentLogDate);

            if (colorList != null)
            {
                AddColorgramDaysToMonthPreview(currentLogDate, colorList);
            }
        }

        private void AddColorgramDaysToMonthPreview(DateTime dt, List<Color> colorList)
        {
            BlackOutDataGrid(dt);

            // Get all days for this month...
            foreach (var datesAndValues in colorgram.Where(e => (e.Key.Month == dt.Month && e.Key.Year == dt.Year)))
            {
                // Check the month...
                int date = datesAndValues.Key.Day;
                int daysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);

                for (int day = 0; day < daysInMonth; day++)
                {
                    int adjustedDate = date - 1;

                    foreach (KeyValuePair<int, int> hourlyCount in datesAndValues.Value)
                    {
                        int count = hourlyCount.Value;

                        if (count == 0)
                        {
                            dataGridView1.Rows[hourlyCount.Key].Cells[adjustedDate].Style.BackColor = colorList[0];
                        }
                        else if (count == -1)
                        {
                            dataGridView1.Rows[hourlyCount.Key].Cells[adjustedDate].Style.BackColor = Color.Black;
                        }
                        else
                        {
                            dataGridView1.Rows[hourlyCount.Key].Cells[adjustedDate].ToolTipText = hourlyCount.Value.ToString();

                            dataGridView1.Rows[hourlyCount.Key].Cells[adjustedDate].Style.BackColor = colorList[hourlyCount.Value - 1];
                        }
                    }
                }
            }

            lblPreviewedMonth.Text = dt.ToString("MMMM yyyy");
        }

        private void txtWhatIf_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only respond to numeric input...
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        
        private void btnNormalise_Click(object sender, EventArgs e)
        {
            if (radioColourByRandom.Checked)
            {
                if (string.IsNullOrEmpty(txtWhatIf.Text))
                {
                    MessageBox.Show(invalidTopCountMessage);
                    return;
                }
            }
            RedrawPreview(GetRmobDateFromCurrentLogFile());
        }

        private void btnRecreateRmob_Click(object sender, EventArgs e)
        {
            DirectoryInfo rmobFolder = new DirectoryInfo(config.RmobFilesDirectory);
            bool newFolder = false;

            if (!rmobFolder.Exists)
            {
                rmobFolder.Create();
                newFolder = true;
            }

            if (!newFolder)
            {
                IssueWarningsAndDeleteRmob(rmobFolder);
            }

            DirectoryInfo basicFiles = new DirectoryInfo(config.UpdatedLogsDirectory);

            string fileNameFilter = GetFileNameFilter();

            FileInfo[] fi = basicFiles.GetFiles(fileNameFilter);

            foreach(FileInfo file in fi)
            {
                string[] fileContent = File.ReadAllLines(file.FullName);

                ProcessRmobFileData(fileContent, file.Name, true);

                SaveRmobFile(true);
            }
            RedrawPreview(dateTimePicker2.Value);
        }

        private string GetFileNameFilter()
        {
            string fileNameFilter = string.Empty;

            if (radioRmobAll.Checked)
            {
                fileNameFilter = "event_log*_saved.txt";
            }
            else
            {
                string month = dateTimePicker2.Value.ToString("MM");
                string year = dateTimePicker2.Value.ToString("yyyy");

                if (radioRmobYear.Checked)
                {
                    fileNameFilter = string.Format("event_log{0}*_saved.txt", year);
                }
                else if (radioRmobMonth.Checked)
                {
                    fileNameFilter = string.Format("event_log{0}{1}*_saved.txt", year, month);
                }
            }
            return fileNameFilter;
        }

        private void IssueWarningsAndDeleteRmob(DirectoryInfo rmobFolder)
        {
            if (radioRmobAll.Checked)
            {
                DialogResult res = MessageBox.Show("All RMOB files for this station will be deleted. Do you wish to continue?", "Warning", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    // Delete all RMOB files...
                    foreach (FileInfo fi in rmobFolder.GetFiles())
                    {
                        fi.Delete();
                    }
                }
            }
            else if (radioRmobMonth.Checked)
            {
                // Select all files matching the selected month and year...
                string month = dateTimePicker2.Value.ToString("MMM");
                string year = dateTimePicker2.Value.ToString("yyyy");

                DialogResult res = MessageBox.Show(string.Format("RMOB files for {0} {1} for this station will be deleted. Do you wish to continue?", month, year), "Warning", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    // Delete the RMOB file for this month...
                    foreach (FileInfo fi in rmobFolder.GetFiles(string.Format("rmob_{0}_{1}.txt", month, year)))
                    {
                        fi.Delete();
                    }
                }
            }
            else if (radioRmobYear.Checked)
            {
                // Select all files matching selected year...
                string year = dateTimePicker2.Value.Year.ToString();

                DialogResult res = MessageBox.Show(string.Format("{0} RMOB file for this station will be deleted. Do you wish to continue?", year), "Warning", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    // Delete the RMOB file for this year...
                    foreach (FileInfo fi in rmobFolder.GetFiles(string.Format("rmob_*_{0}.txt", year)))
                    {
                        fi.Delete();
                    }
                }
            }
        }
    }
}
