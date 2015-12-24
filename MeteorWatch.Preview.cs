using System;
using System.Collections.Generic;
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
    partial class Scatterthon
    {

        private void AddCleansedDataToPreview()
        {
            if (currentLogDate == null)
            {
                return;
            }       

            List<Color> colorList = PrepareColourRangeForHighestCount(currentLogDate);

            AddColorgramDaysToMonthPreview(currentLogDate, colorList);
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

        private List<Color> PrepareColourRangeForHighestCount(DateTime dt)
        {
            int highestCount = FindHighestCountThisMonth(dt);

            // Set this value in the grid..
            dataGridView1.Rows[23].Cells[33].Value = highestCount;
            dataGridView1.Rows[23].Cells[33].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            highestCount = UpdateHighestCount(highestCount);

            if (dataGridView1.Rows[23].Cells[33].Value.ToString() != highestCount.ToString())
            {
                dataGridView1.Rows[23].Cells[33].Value = highestCount;
            }

            //List<Color> colorList = PrepareListOfThreeColours(topRange, midRange2, botRange, highestCount);
            List<Color> colorList = PrepareListOfMultiColours(highestCount);

            return colorList;
        }

        private int UpdateHighestCount(int highestCount)
        {
            if (highestCount < 24)
            {
                highestCount = 24;
                config.AnnualTopMeteorCount = 24;
            }

            if (!checkIgnoreTopCount.Checked)
            {
                if (highestCount < config.AnnualTopMeteorCount)
                {
                    highestCount = config.AnnualTopMeteorCount;
                }
                else if (highestCount > config.AnnualTopMeteorCount)
                {
                    config.AnnualTopMeteorCount = highestCount;
                }
            }
            return highestCount;
        }

        private void BlackOutDataGrid(DateTime monthAndYear)
        {
            int daysInMonth = DateTime.DaysInMonth(monthAndYear.Year, monthAndYear.Month);

            dataGridView1.GridColor = Color.DarkGray;

            // Prepare the background black...
            for (int i = 0; i < daysInMonth; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    dataGridView1.Rows[j].Cells[i].Value = String.Empty;
                    dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.Black;
                }
            }
        }

        private void DisplayStandardDataGrid()
        {
            List<Color> colorList = PrepareListOfMultiColours(24);

            int squareSide = 17;

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            for (int i = 0; i < 31; i++)
            {
                dataGridView1.Columns[i].Width = squareSide;
            }

            for (int i = 0; i < 24; i++)
            {
                int index = dataGridView1.Rows.Add();

                //dataGridView1.Rows[index].DividerHeight = 10;
                dataGridView1.Rows[index].Height = squareSide;
                dataGridView1.Rows[index].HeaderCell.Value = index.ToString();
                dataGridView1.Rows[index].HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                // Draw the scales...
                // dataGridView1.Rows[hour].Cells[day].Style.BackColor = Color.Red
                dataGridView1.Rows[index].Cells[32].Style.BackColor = colorList[index];
            }

            // Set the zero range cell..
            dataGridView1.Rows[0].Cells[33].Value = "0";
            dataGridView1.Rows[0].Cells[33].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            BlackOutDataGrid(DateTime.Now);
        }

        private void btnImportColorgram_Click(object sender, EventArgs e)
        {
            DisplayRmobDataFromFile();
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
                    // returns DateTime and the data for that month...
                    // as a KeyValuePair<DateTime, Dictionary<int, int>> of dates, hours and counts
                    DateTime monthAndYear;
                    Dictionary<DateTime, Dictionary<int, int>> hoursAndCounts;

                    GetRmobData(tempFile, out monthAndYear, out hoursAndCounts);

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
        }

        private void RedrawPreview(DateTime monthAndYear)
        {
            currentRmobDate = monthAndYear;

            List<Color> colorList = PrepareColourRangeForHighestCount(monthAndYear);

            //DrawGridColorScales();
            DrawGridMulticolorScales();

            AddColorgramDaysToMonthPreview(monthAndYear, colorList);
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

        private void DrawGridColorScales()
        {
            //List<Color> colorList = PrepareListOfThreeColours(24);
            List<Color> colorList = PrepareListOfMultiColours(24);

            // Paint the scales...
            for (int i = 0; i < 24; i++)
            {
                dataGridView1.Rows[i].Cells[32].Style.BackColor = colorList[i];
            }
        }

        private void GetRmobData(string tempFile, out DateTime monthAndYear, out Dictionary<DateTime, Dictionary<int, int>> hoursAndCounts)
        {
            string fileName = Path.GetFileName(tempFile);

            GroupCollection groups = Regex.Match(fileName, @"rmob_(?<month>.{3}?)_(?<year>\d{4}?).txt").Groups;

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

        private List<Color> PrepareListOfMultiColours(int topCount)
        {
            if (topCount < 24)
            {
                topCount = 24;
            }

            // B(0) --- T(1) --- G(2) --- Y(3) --- . --- R(4)

            int length = standardRange.Length;
            int loopLimit = length - 1;
            int allocatedSquares = topCount / length;
            int lastRange = topCount - allocatedSquares * 3;

            List<Color> colors = new List<Color>();


            for(int i = 0; i < loopLimit - 1; i++)
            {
                List<Color> range1 = PrepareListOfColours(standardRange[i], standardRange[i + 1], allocatedSquares);
                colors.AddRange(range1);
            }

            List<Color> range2 = PrepareListOfColours(standardRange[loopLimit - 1], standardRange[loopLimit], lastRange);
            colors.AddRange(range2);

            return colors;
        }

        private List<Color> PrepareListOfThreeColours(Color topRange, Color midRange, Color botRange, int topCount)
        {
            if (topCount == 0)
            {
                topCount = 24;
            }

            int halfWayCount = topCount/2;

            List<Color> firstRange = PrepareListOfColours(topRange, midRange, halfWayCount);
            List<Color> secondRange = PrepareListOfColours(midRange, botRange, halfWayCount + 1);

            firstRange.AddRange(secondRange);

            return firstRange;
        }

        private List<Color> PrepareListOfColours(Color botRange, Color topRange, int topCount)
        {
            if (topCount == 0)
            {
                topCount = 1;
            }

            int rMax = topRange.R;
            int rMin = botRange.R;
            int rDiff = rMax - rMin;

            int gMax = topRange.G;
            int gMin = botRange.G;
            int gDiff = gMax - gMin;

            int bMax = topRange.B;
            int bMin = botRange.B;
            int bDiff = bMax - bMin;

            var colorList = new List<Color>();
            for (int i = 0; i < topCount; i++)
            {
                var rAverage = rMin + (int)(rDiff * i / topCount);
                var gAverage = gMin + (int)(gDiff * i / topCount);
                var bAverage = bMin + (int)(bDiff * i / topCount);

                //colorList.Add(Color.FromArgb(rAverage, gAverage, bDiff));
                colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }

            return colorList;
        }
    }
}
