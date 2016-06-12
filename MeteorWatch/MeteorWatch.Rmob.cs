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
        private bool whatIfPreview = false;

        #region Button click events... 

        private void btnAddToCalendar_Click(object sender, EventArgs e)
        {
            AddCleansedDataToRmobFile();

        }

        private string MakeRmobFileName(out string fileName)
        {
            string dateStamp = string.Empty;

            if (currentLogDate == DateTime.MinValue)
            {
                dateStamp = string.Format("{0:MMM}_{1}", currentRmobDate, currentRmobDate.Year);
            }
            else 
            {
                if (currentLogDate == DateTime.MinValue)
                {
                    GetCurrentLogDate(currentLogFileName, out currentLogDate);
                }
                dateStamp = string.Format("{0:MMM}_{1}", currentLogDate, currentLogDate.Year);
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
            string newFileName = SaveRmobFile();

            MessageBox.Show(string.Format("RMOB file saved as {0}...", newFileName), "File Saved as...");
        }

        private string SaveRmobFile()
        {
            string dateStamp, newFileName;

            dateStamp = MakeRmobFileName(out newFileName);

            string allText = this.rmobFiles[dateStamp].MakeFile();

            File.WriteAllText(newFileName, allText);
            return newFileName;
        }
        #endregion

        #region Helpers...
        private void BoldenCleansedLogDate()
        {
            DateTime[] datesSoFar = monthCalendar2.BoldedDates;

            int requiredLength = datesSoFar.Length + 1;
            Array.Resize(ref datesSoFar, requiredLength);
            datesSoFar[requiredLength - 1] = currentLogDate;

            monthCalendar2.BoldedDates = datesSoFar;
        }
        
        private void AddCleansedDataToRmobFile()
        {
            string monthStamp = string.Format("{0:MMM}_{1}", currentLogDate, currentLogDate.Year);

            if (!rmobFiles.ContainsKey(monthStamp))
            {
                rmobFiles.Add(monthStamp, new RmobFile(currentLogDate));
            }

            rmobFiles[monthStamp].SetDataForDay(currentLogDate, colorgram[currentLogDate]);

            BoldenCleansedLogDate();
        }

        private void AddBoldDatesForExistingCleansedFiles()
        {
            try
            {
                // Check for files in the specified directory.
                string cleansedDirectory = config.UpdatedLogsDirectory;

                if (Directory.Exists(cleansedDirectory))
                {
                    string[] files = Directory.GetFiles(cleansedDirectory, "updated_event_log20*.txt");

                    DateTime[] cleansedDatesArray = monthCalendar2.BoldedDates;
                    Array.Resize(ref cleansedDatesArray, files.Length);

                    for (int fileIndex = 0; fileIndex < files.Length; fileIndex++)
                    {
                        string fileName = Path.GetFileName(files[fileIndex]);

                        GroupCollection groups = Regex.Match(fileName, @"(?<timestamp>\d{8}?).txt").Groups;

                        DateTime timePart = DateTime.ParseExact(groups["timestamp"].Value.ToString(),
                            "yyyyMMdd", CultureInfo.CurrentCulture);

                        cleansedDatesArray[fileIndex] = timePart;

                        //string dateString = fileName.Substring(17, 8);

                        // Parse it to extract the date..
                        //DateTime cleansedDate = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.CurrentCulture);
                        //cleansedDatesArray[fileIndex] = cleansedDate;
                    }

                    monthCalendar2.BoldedDates = cleansedDatesArray;
                }
            }
            catch { }
        }
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
                        else if (count == -1) //|| hourlyCount.Key
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
            // Only respond to numeric input or Enter key...
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                radioColourByRandom_CheckedChanged(radioColourByRandom, null);
            }
        }


    }
}
