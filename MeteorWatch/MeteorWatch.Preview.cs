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
        private void LoadPreviewDataGrid()
        {
            List<DateTime> timestamps = GetTimestampsToView();

            // How many columns?
            int daysInMonth = DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);

            int maxEventsThisMonth = 0;
            int numberOfRows = 0;

            Dictionary<DateTime, int> sums = SumUpInstancesPerPeriod(timestamps, daysInMonth, out numberOfRows, out maxEventsThisMonth);

            PreparePreviewGridForData(maxEventsThisMonth, numberOfRows, daysInMonth);

            List<Color> colorList = PrepareListOfMultiColours(maxEventsThisMonth);

            if (dataGridView2.Rows.Count > 0)
            {
                // Prepare the background black...
                for (int i = 0; i < daysInMonth; i++)
                {
                    for (int j = 0; j < numberOfRows; j++)
                    {
                        dataGridView2.Rows[j].Cells[i].Style.BackColor = Color.Black;
                    }
                }

                PopulatePreviewGridWithData(sums, colorList, numberOfRows, daysInMonth);

                // Get rid of the "current selection" blue square...
                dataGridView2.CurrentCell = null;
            }
            else
            {
                MessageBox.Show("Either there is no data or no 'unit of time' was selected...", "Not enough data");
            }
        }

        private void PopulatePreviewGridWithData(Dictionary<DateTime, int> totals, List<Color> colorList, int numberOfRows, int daysInMonth)
        {
            int index = 0;

            for (int i = 0; i < daysInMonth; i++)
            {
                for (int j = 0; j < numberOfRows; j++)
                {
                    int count = totals.Values.ElementAt(index);

                        if (count == 0)
                        {
                            dataGridView2.Rows[j].Cells[i].Style.BackColor = Color.Black;
                        }
                        else
                        {
                            dataGridView2.Rows[j].Cells[i].ToolTipText = count.ToString();

                            dataGridView2.Rows[j].Cells[i].Style.BackColor = colorList[count - 1];
                        }

                    index++;
                }
            }
        }

        private List<DateTime> GetTimestampsToView()
        {
            // Read in the user selections... event_log20140628
            List<DateTime> timestampList = new List<DateTime>();

            // Prepare file names...
            string fileName = string.Format("event_log{0}*.txt", dateTimePicker1.Value.ToString("yyyyMM"));

            List<string> selectedItems = new List<string>();

            foreach (object itemChecked in checkedListClasses.CheckedItems)
            {
                string selectedCategory = itemChecked.ToString();
                selectedItems.Add(selectedCategory.Trim().Replace(" ", ""));
            }

            // Go through each category and pull in the corresponding timestamps...
            foreach (string folderName in selectedItems)
            {
                string directory = Path.Combine(new string[] { config.CurrentStation, folderName });

                if (Directory.Exists(directory))
                {
                    GetClassifiedFileTimestamps(timestampList, fileName, directory);
                }
            }

            if (selectedItems.Contains("Headecho") && selectedItems.Contains("Meteortrail"))
            {
                // Get our unclassified meteor data...
                if (Directory.Exists("Unclassified"))
                {
                    GetClassifiedFileTimestamps(timestampList, fileName, "Unclassified");
                }
            }
            return timestampList;
        }

        private void GetClassifiedFileTimestamps(List<DateTime> timestampList, string fileName, string folderName)
        {
            string[] timestampsFiles = Directory.GetFiles(folderName, fileName);

            // Could be several files (per category per day)...
            foreach (string timestampFile in timestampsFiles)
            {
                DateTime dateStamp = new DateTime();

                GetCurrentLogDate(timestampFile, out dateStamp);

                string[] lines = File.ReadAllLines(timestampFile);

                foreach (string line in lines)
                {
                    // Get it into full timestamp format...
                    TimeSpan ts = TimeSpan.Parse(line, CultureInfo.InvariantCulture);

                    dateStamp = dateStamp.Date + ts;

                    timestampList.Add(dateStamp);
                }
            }
        }

        private void PreparePreviewGridForData(int maxEventsThisMonth, int numberOfRows, int daysInMonth)
        {
            if (comboPeriod.SelectedItem == null)
            {
                // No item selected for preview...
                return;
            }

            dataGridView2.CurrentCell = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // How many columns?
            for (int i = 1; i <= daysInMonth; i++)
            {
                string dayOfMonth = i.ToString();

                DataGridViewTextBoxColumn column = CreateNewTextBoxColumn(dayOfMonth);

                dataGridView2.Columns.Add(column);
            }

            // Blank "divider" column...
            dataGridView2.Columns.Add(CreateNewTextBoxColumn(""));
            // Colorgramme color range column...
            dataGridView2.Columns.Add(CreateNewTextBoxColumn(""));
            // "Top count" column...
            dataGridView2.Columns.Add(CreateNewTextBoxColumn("", 30));

            // Get the height of the row...
            int rowHeight = (int)Math.Ceiling(20 * decimal.Parse(comboPeriod.SelectedItem.ToString()));

            for(int i = 0; i < numberOfRows; i++)
            {
                int indexAdded = dataGridView2.Rows.Add();
                dataGridView2.Rows[indexAdded].Height = rowHeight;
            }

            // Set the maximum range cell...
            dataGridView2.Rows[numberOfRows - 1].Cells[daysInMonth + 2].Value = maxEventsThisMonth.ToString();
            dataGridView2.Rows[numberOfRows - 1].Cells[daysInMonth + 2].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            // Now take care of the "color reference" column...
            List<Color> referenceColorList = PrepareListOfMultiColours(numberOfRows);

            int coloursRow = dataGridView2.Columns.Count - 2;

            for (int hour = 0; hour < numberOfRows; hour++)
            {
                dataGridView2.Rows[hour].Cells[coloursRow].Style.BackColor = referenceColorList[hour];
            }
        }

        private static DataGridViewTextBoxColumn CreateNewTextBoxColumn(string dayOfMonth, int colWidth = 20)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

            column.HeaderText = dayOfMonth;
            column.Name = dayOfMonth;
            column.ReadOnly = true;
            column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            column.Width = colWidth;
            return column;
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

            if (!checkIgnoreTopCount.Checked || whatIfPreview)
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

            // We don't want a "random" blue square on our colorgram...
            dataGridView1.CurrentCell = null;
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
