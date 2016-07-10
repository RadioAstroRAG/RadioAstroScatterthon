using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LogComponent
{
    public partial class LogFileViewer : UserControl
    {
        public event RowWithImageSelectedHandler ScreenshotRowSelected;
        public event EventHandler ScreenshotDeselected;               

        // Adapt to a different log file structure by changing the numbers.
        enum Index
        {
            Time = 0,       // Timestamp
            Event = 1,      // EventNumber
            Signal = 2,     // SignalStrength
            Noise = 3,      // Noise
            Freq = 4,       // Frequency
            Duration = 5,   // Duration
            Class = 6,      // Classification
            Image = 7       // Screenshot reference
        }

        Stack<KeyValuePair<DataGridViewRow[], int[]>> undoHistory = new Stack<KeyValuePair<DataGridViewRow[], int[]>>();
      
        private string[] LogFile;
        private bool sortedByTime = true;
        private bool controlKeyDown = false;
        private bool contextMenuRequired = false;

        private string firstSelectedImage = string.Empty;

        // Categories of phenomenon...
        List<string> categories = new List<string>();

        public string[] LogFileContent
        {
          get 
          {
              List<string> lines = new List<string>();

              // Get all the data from our data grid view into a csv file...
              foreach (DataGridViewRow row in dgv.Rows)
              {
                  var cells = row.Cells.Cast<DataGridViewCell>();
                  lines.Add(string.Join(",", cells.Select(cell => cell.Value).ToArray()));
              }

              return lines.ToArray();
          }

          set 
          { 
              LogFile = value; 
          }
        }

        public string[] LogFileContentSortedByTime
        {
          get 
          {
              List<string> lines = new List<string>();
              try
              {
                  DataGridViewRow[] rows = CopyOfDataGridRows(dgv);

                  using (DataGridView temp = new DataGridView())
                  {
                      // We need 8 columns...
                      for (int i = 0; i < 8; i++)
                      {
                          temp.Columns.Add(i.ToString(), i.ToString());
                      }

                      temp.Rows.AddRange(rows);
                      temp.Sort(temp.Columns[0], ListSortDirection.Ascending);

                      // Get all the data from our data grid view into a csv file...
                      foreach (DataGridViewRow row in temp.Rows)
                      {
                          var cells = row.Cells.Cast<DataGridViewCell>();
                          lines.Add(string.Join(",", cells.Select(cell => cell.Value).ToArray()));
                      }
                  }
              }
              catch(ArgumentNullException ex)
              {
                  // Log the offending log-file name, as it's probabaly in the wrong format.
              }
              return lines.ToArray();
          }
        }

        #region Constructor
        public LogFileViewer()
        {
            InitializeComponent();
            dgv.SortCompare += CustomSortCompare;

            categories.Add("Aircraft");
            categories.Add("Head echo");
            categories.Add("Interference");
            categories.Add("Meteor trail");
            categories.Add("Moon bounce");
            categories.Add("Satellite");
            categories.Add("Query");
        }
        #endregion

        public void ClearUndoHistory()
        {
            undoHistory.Clear();
            undoHistory = null;
            undoHistory = new Stack<KeyValuePair<DataGridViewRow[], int[]>>();
        }
        public void SelectFirstRow()
        {
            if (dgv != null && dgv.Rows.Count > 0)
            {
                dgv.Rows[0].Selected = true;
            }
        }
        public void LoadLogFileContent(string fileName, string pathToScreenshots, int timeOffset, bool showSignal, bool showNoise, bool showFreq)
        {
            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length > 0)
            {
                // int numberOfExtraColumns = lines[0].Split(new char[] { ',' }).Length;
                int numberOfExtraColumns = 8;

                bool imagesPopulated = PartiallyResetGridsRowsAndColumns(dgv, numberOfExtraColumns, showSignal, showNoise, showFreq);
                
                PopulateGridViewWithData(dgv, lines, numberOfExtraColumns);

                if (!imagesPopulated)
                {
                    PopulateGridWithImagePaths(dgv, fileName, pathToScreenshots, timeOffset);
                }               
            }
        }

        public DateTime? GetCurrentLogDate(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string date = Regex.Match(fileName,
                        string.Format(@"event_log(?<dateStamp>\d+$?)"))
                              .Groups["dateStamp"].Value;

                DateTime currentLogDate = new DateTime();

                DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.None, out currentLogDate);

                return currentLogDate;
            }
            return null;
        }

        private void PopulateGridWithImagePaths(DataGridView dgv, string fileName, string pathToScreenshots, int timeOffset)
        {
            if (!Directory.Exists(pathToScreenshots))
            {
                return;
            }

            // Get a selection of images from the specified directory for the current date...
            string[] screenshots = null;

            DateTime? currentDate = GetCurrentLogDate(fileName);

            if (currentDate != null)
            {
                GroupCollection groups = Regex.Match(fileName, @"(?<datestamp>\d{6}?).txt").Groups;

                string fileNameBits = null;

                if (groups["datestamp"].Value != null)
                {
                    fileNameBits = groups["datestamp"].Value.ToString();
                }

                if (string.IsNullOrEmpty(fileNameBits))
                {
                    // Let's try the "saved" version...
                    GroupCollection groups1 = Regex.Match(fileName, @"(?<datestamp>\d{6}?)_saved.txt").Groups;

                    if (groups1["datestamp"].Value != null)
                    {
                        fileNameBits = groups1["datestamp"].Value.ToString();
                    }
                }

                // Get all the files related to this date...
                string screenshotNamePattern = string.Format("event{0}", fileNameBits);

                // Go to screenshots directory...
                screenshots = Directory.GetFiles(pathToScreenshots, string.Format("{0}*.jpg", screenshotNamePattern));
            }

            // Go through each image and see what period it is supposed to cover...
            int rowIndexToStartAt = 0;

            foreach(string screenshot in screenshots)
            {
                // Get the timestamp for the image, subtract the "delay" period...
                GroupCollection groups = Regex.Match(screenshot, @"(?<timestamp>\d{6}?).jpg").Groups;

                if (groups["timestamp"].Value != null)
                {
                    string timeParts = groups["timestamp"].Value.ToString();

                    // tos = Time Of Screenshot
                    DateTime tos = DateTime.ParseExact(timeParts, "HHmmss", CultureInfo.CurrentCulture);

                    TimeSpan end = new TimeSpan(tos.Hour, tos.Minute, tos.Second);

                    TimeSpan start = end.Subtract(TimeSpan.FromSeconds(timeOffset));

                    bool startPeriodMatched = false;
                    int time = (int)Index.Time;
                    int screen = (int)Index.Image;
                    int sortResult = 0;

                    for (int index = rowIndexToStartAt; index < dgv.Rows.Count; index++ )
                    {
                        DataGridViewRow row = dgv.Rows[index];

                        // Make sure there's some data in this row...
                        string timeCellValue = row.Cells[time].Value as string;

                        if (!string.IsNullOrEmpty(timeCellValue))
                        {
                            DateTime a = DateTime.ParseExact(timeCellValue, "HH:mm:ss", CultureInfo.CurrentCulture);
                            TimeSpan ts = new TimeSpan(a.Hour, a.Minute, a.Second);

                            // Is this time stamp falling within the "start" and "end" period?...
                            if (!startPeriodMatched)
                            {
                                sortResult = ts.CompareTo(start);

                                if (sortResult >= 0)
                                {
                                    startPeriodMatched = true;

                                    // Add this screenshot to the end of the row for reference...
                                    row.Cells[screen].Value = screenshot;
                                    row.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                                }
                            }
                            else
                            {
                                // We are gone past the beginning of the period...
                                // But are we still inside the period?
                                sortResult = ts.CompareTo(end);

                                if (sortResult <= 0)
                                {
                                    // Yes, we are still inside the period...
                                    row.Cells[screen].Value = screenshot;
                                    row.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                                }
                                else
                                {
                                    // No, we are outside the period. Time to get out of the loop.
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        
        public void btnMerge_Click(object sender, EventArgs e)
        {
            if (!sortedByTime)
            {
                MessageBox.Show("Please sort by Time (in ascending order) before applying the Merge function.");
            }
            else if (dgv.SelectedRows.Count <= 1)
            {
                MessageBox.Show("Please ensure several rows are selected before applying the Merge function.");
            }
            else
            {
                PrepareBackup(dgv);

                DataGridViewSelectedRowCollection selected = dgv.SelectedRows;
                int selectedRowsNumber = selected.Count;

                IQueryable<DataGridViewRow> qRows = selected.OfType<DataGridViewRow>().AsQueryable();

                // We will be merging into the top-most record, let us find its index first...
                var minIndex = qRows.Min(i => i.Index);
                var maxIndex = qRows.Max(i => i.Index);

                //decimal totalMergedDuration = 0;
                //double maxSigStrength = 0;
                //var meanNoise = 0.0;
                //var meanFreq = 0.0;
                
                try
                {   
                    // // See if any of these rows have been programmatically split before...
                    //var artificial = qRows.Where(i => i.Cells[(int)Index.Event].Value.ToString().Contains("."));

                    //if (artificial.ToList().Count == 0)
                    //{
                    //    // Calculate the required max and means.
                    //    // maxSigStrength = (double)decimal.Parse(qRows.Max(i => i.Cells[(int)Index.Signal].Value).ToString());

                    //    foreach (DataGridViewRow row in dgv.SelectedRows)
                    //    {
                    //        double tempStrength = 0;
                    //        double.TryParse(row.Cells[(int)Index.Signal].Value.ToString(), out tempStrength);
                    //        maxSigStrength += tempStrength;
                    //    }

                    //    //meanNoise = qRows.Average(i => (double)decimal.Parse(i.Cells[(int)Index.Noise].Value.ToString()));
                    //    //meanFreq = qRows.Average(i => (double)decimal.Parse(i.Cells[(int)Index.Freq].Value.ToString()));

                    //    // int totalMergedDuration = GetDescreteDuration(minIndex, maxIndex);
                    //    // int totalMergedDuration = (int)qRows.Sum(i => (decimal)i.Cells[(int)Index.Duration].Value);
                    //    // int totalMergedDuration = qRows.Sum(i => (int.Parse(i.Cells[(int)Index.Duration].Value.ToString())));

                    //    // This is a problematic calculation, do it "manually"...                        

                    //    foreach (DataGridViewRow row in dgv.SelectedRows)
                    //    {
                    //        decimal tempDuration = 0;
                    //        decimal.TryParse(row.Cells[(int)Index.Duration].Value.ToString(), out tempDuration);
                    //        totalMergedDuration += tempDuration;
                    //    }
                    //}

                    // Remove the remaining selected records...
                    for (int i = 0; i < selectedRowsNumber; i++)
                    {
                        DataGridViewRow row = selected[i];

                        if (row.Index != minIndex)
                        {
                            dgv.Rows.RemoveAt(row.Index);
                        }
                    }

                    DataGridViewRow rowToMergeInto = dgv.Rows[minIndex];

                    // Set the new total duration in the record we are keeping.
                    //rowToMergeInto.Cells[(int)Index.Duration].Value = totalMergedDuration;
                    //rowToMergeInto.Cells[(int)Index.Signal].Value = maxSigStrength;
                    //rowToMergeInto.Cells[(int)Index.Noise].Value = Math.Round((decimal)meanNoise, 1, MidpointRounding.AwayFromZero);
                    //rowToMergeInto.Cells[(int)Index.Freq].Value = Math.Round((decimal)meanFreq, 1, MidpointRounding.AwayFromZero);

                    rowToMergeInto.Cells[(int)Index.Duration].Value = "";
                    rowToMergeInto.Cells[(int)Index.Signal].Value = "";
                    rowToMergeInto.Cells[(int)Index.Noise].Value = "";
                    rowToMergeInto.Cells[(int)Index.Freq].Value = "";

                    rowToMergeInto.Selected = true;
                    dgv.CurrentCell = rowToMergeInto.Cells[0];
                    dgv.Focus();
                }
                catch(Exception ex)
                {
                    // Inspect the exception and decide what to do.
                    MessageBox.Show("Please ensure the format of selected rows is consistent with other log entries.");
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private int GetDescreteDuration(int minIndex, int maxIndex)
        {
            // Now calculate the total merged event duration. Start with finding the time span...
            DateTime startTime = DateTime.ParseExact(dgv.Rows[minIndex].Cells[(int)Index.Time].Value.ToString(), "HH:mm:ss", CultureInfo.CurrentCulture);
            DateTime endTime = DateTime.ParseExact(dgv.Rows[maxIndex].Cells[(int)Index.Time].Value.ToString(), "HH:mm:ss", CultureInfo.CurrentCulture);

            TimeSpan span = endTime - startTime;
            int totalMergedDuration = span.Seconds + int.Parse(dgv.Rows[maxIndex].Cells[5].Value.ToString());
            return totalMergedDuration;
        }

        private int GetContinuousDuration(int minIndex, int maxIndex)
        {
            // Now calculate the total merged event duration. Start with finding the time span...
            DateTime startTime = DateTime.ParseExact(dgv.Rows[minIndex].Cells[(int)Index.Time].Value.ToString(), "HH:mm:ss", CultureInfo.CurrentCulture);
            DateTime endTime = DateTime.ParseExact(dgv.Rows[maxIndex].Cells[(int)Index.Time].Value.ToString(), "HH:mm:ss", CultureInfo.CurrentCulture);

            TimeSpan span = endTime - startTime;
            int totalMergedDuration = span.Seconds + int.Parse(dgv.Rows[maxIndex].Cells[5].Value.ToString());
            return totalMergedDuration;
        }

        public void btnSplit_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please ensure only one row is selected before applying the Split function.");
            }
            else if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please ensure a full row is selected before applying the Split function.");
            }
            else
            {
                int numberOfNewRows = (int)numericUpDown1.Value - 1;

                if (numberOfNewRows >= 1)
                {
                    PrepareBackup(dgv);

                    // Split the selected row into more than one...
                    DataGridViewRow selectedRow = dgv.SelectedRows[0];
                    DataGridViewCellCollection srcCells = selectedRow.Cells;

                    int indexOfSelectedRow = selectedRow.Index;
                    
                    DataGridViewRow[] range = new DataGridViewRow[numberOfNewRows];

                    for (int i = 0; i < numberOfNewRows; i++)
                    {
                        DataGridViewRow newRow = selectedRow.Clone() as DataGridViewRow; // new DataGridViewRow();

                        newRow.Cells[(int)Index.Time].Value = srcCells[(int)Index.Time].Value;

                        newRow.Cells[(int)Index.Event].Value = string.Format("{0}.{1}", srcCells[(int)Index.Event].Value, i+1);
                        newRow.Cells[(int)Index.Signal].Value = string.Empty;
                        newRow.Cells[(int)Index.Noise].Value = string.Empty;
                        newRow.Cells[(int)Index.Freq].Value = string.Empty;
                        newRow.Cells[(int)Index.Duration].Value = string.Empty;
                        newRow.Cells[(int)Index.Image].Value = srcCells[(int)Index.Image].Value;

                        DataGridViewCellCollection destCells = newRow.Cells;

                        // Copy the cell values of the original row...
                        //for (int j = 0; j < destCells.Count; j++ )
                        //{
                        //    destCells[j].Value = srcCells[j].Value;
                        //}                            
                                                                       
                        range[i] = newRow; 
                    } 

                    // And insert them after the selected row.
                    dgv.Rows.InsertRange(indexOfSelectedRow + 1, range);

                    // Highlight all the newly split rows...

                    for (int i = indexOfSelectedRow; i <= (indexOfSelectedRow + range.Length); i++)
                    {
                        dgv.Rows[i].Selected = false;
                    }                    

                    dgv.Rows[indexOfSelectedRow + range.Length].Selected = true;
                    dgv.CurrentCell = dgv.Rows[indexOfSelectedRow + range.Length].Cells[0];
                }
            }
        }

        private void btnApplyClassification_Click(object sender, EventArgs e)
        {
            if (comboClasses.SelectedItem != null && !string.IsNullOrEmpty(comboClasses.SelectedItem.ToString()))
            {
                // See how many rows have been selected..
                DataGridViewSelectedRowCollection rows = dgv.SelectedRows;

                if (rows.Count > 0)
                {
                    PrepareBackup(dgv);

                    string original = comboClasses.SelectedItem.ToString();

                    if (original.Length > 4)
                    {
                        string classification = original.Substring(4);

                        if (categories.Contains(classification) || classification == "Remove")
                        {
                            int indexToUpdateAt = dgv.Columns["Class"].Index;

                            foreach (DataGridViewRow row in rows)
                            {
                                // Set the value of the cell in the "classification" column
                                // to the value selected in the drop-down with event types.
                                row.Cells[indexToUpdateAt].Value = classification;
                            }
                        }
                    }
                }
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            UndoLastAction(dgv);
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            btnUndo.Enabled = false;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dgv.SelectedRows != null && dgv.SelectedRows.Count > 0)
            {
                btnApplyClassification.Enabled = true;
                btnApplyClassification.Focus();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // We should only be able to split into 2 or more rows.
            if (dgv.SelectedRows != null && dgv.SelectedRows.Count == 1)
            {
                if (!btnSplit.Enabled)
                {
                    if (numericUpDown1.Value > 1)
                    {
                        btnSplit.Enabled = true;
                    }
                }
                else
                {
                    if (numericUpDown1.Value < 2)
                    {
                        btnSplit.Enabled = false;
                    }
                }
            }
            else
            {
                btnSplit.Enabled = false;
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            int numberSelectedRows = dgv.SelectedRows.Count;
            bool displayScreenshot = true;

            // We should only be able to merge rows if more than 1 are selected.
            if (numberSelectedRows >= 2)
            {
                IQueryable<DataGridViewRow> qRows = dgv.SelectedRows.OfType<DataGridViewRow>().AsQueryable();

                // We will be merging into the top-most record, let us find its index first...
                var minIndex = qRows.Min(i => i.Index);
                var maxIndex = qRows.Max(i => i.Index);                                          

                // See if row selection is contiguous...
                if (maxIndex - minIndex + 1 == numberSelectedRows)
                {
                    btnMerge.Enabled = true;
                }
                else
                {
                    btnMerge.Enabled = false;
                }

                btnSplit.Enabled = false;
                numericUpDown1.Enabled = false;

                // See if we need to stop dispaying an image (if there was one in the first selected row,
                // and the row currently selected refers to a different image)...
                if (firstSelectedImage != null)
                {
                    object selectedImage = dgv.CurrentRow.Cells[(int)Index.Image].Value;

                    if (selectedImage != null && selectedImage.ToString() != firstSelectedImage)
                    {
                        displayScreenshot = false;
                    }
                }

            }
            else if (numberSelectedRows == 1)
            {
                // See if we need to ask the caller to display a screenshot for this row...
                object selectedImage = dgv.SelectedRows[0].Cells[(int)Index.Image].Value;

                if (selectedImage != null)
                {
                    string screenshot = selectedImage.ToString();

                    // Take note of the selected screenshot to decide when to stop displaying it
                    // during multiple row selection...
                    firstSelectedImage = screenshot;

                    if (!String.IsNullOrEmpty(screenshot) && ScreenshotRowSelected != null)
                    {
                        ScreenshotRowSelected(this, new ScreenshotPathArgs { ScreenshotPath = screenshot });
                        displayScreenshot = true;
                    }                    
                }                

                // Do the buttons...
                btnMerge.Enabled = false;
                numericUpDown1.Enabled = true;                

                if (numericUpDown1.Value > 1)
                {
                    btnSplit.Enabled = true;
                }
                else
                {
                    btnSplit.Enabled = false;
                }
            }
            else if (numberSelectedRows == 0)
            {
                // Nothing selected...
                btnMerge.Enabled = false;
                comboClasses.Enabled = false;
                btnSplit.Enabled = false;
                btnApplyClassification.Enabled = false;
            }

            if (!displayScreenshot && ScreenshotDeselected != null)
            {
                ScreenshotDeselected(this, null);
            }

            if (numberSelectedRows >= 1)
            {
                comboClasses.Enabled = true;
            }

            lblSelectedRowCount.Text = dgv.SelectedRows.Count.ToString();
        }

        public void LogFileViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                return; 
            }
            else if (dgv.SelectedRows.Count >= 2 && e.Shift && e.KeyCode != Keys.Down && e.KeyCode != Keys.Up)
            {
                btnMerge.Focus();
                e.Handled = true;
                return;
            }

            if (e.Alt)
            {
                comboClasses.Enabled = true;
                comboClasses.Focus();
                e.Handled = true;
                return;
            }

            if (e.Control)
            {
                controlKeyDown = true;
                return;
            }

            if (controlKeyDown)
            {
                bool numberPressed = true;

                switch(e.KeyCode)
                {
                    case Keys.D2: numericUpDown1.Value = 2; break;
                    case Keys.D3: numericUpDown1.Value = 3; break;
                    case Keys.D4: numericUpDown1.Value = 4; break;
                    case Keys.D5: numericUpDown1.Value = 5; break;
                    case Keys.D6: numericUpDown1.Value = 6; break;
                    case Keys.D7: numericUpDown1.Value = 7; break;
                    case Keys.D8: numericUpDown1.Value = 8; break;
                    case Keys.D9: numericUpDown1.Value = 9; break;

                    default:
                        numberPressed = false;
                        break;
                }

                if (numberPressed)
                {
                    btnSplit.Focus();                    
                }

                controlKeyDown = false;
                e.Handled = true;            
            }
        }

        private void dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex == 7)
            {
                contextMenuRequired = true;
            }
        }

        private void contextMenuGridStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            contextMenuGridStrip.Hide();

            if (dgv.SelectedRows.Count > 0)
            {
                folderBrowserDialogPopup.SelectedPath = Environment.CurrentDirectory;

                if (DialogResult.OK == folderBrowserDialogPopup.ShowDialog() &&
                    Directory.Exists(folderBrowserDialogPopup.SelectedPath))
                {
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        object cellValue = row.Cells[7].Value;

                        if (cellValue != null)
                        {
                            string imagePath = cellValue.ToString();

                            if (File.Exists(imagePath))
                            {
                                File.Copy(imagePath, Path.Combine(folderBrowserDialogPopup.SelectedPath, new FileInfo(imagePath).Name));
                            }
                        }
                    }
                }
            }
        }

        private void contextMenuGridStrip_Opening(object sender, CancelEventArgs e)
        {
            if (!contextMenuRequired)
            {
                e.Cancel = true;
            }
            else
            {
                // Reset the flag...
                contextMenuRequired = false;
            }
        } 
    }

    public class ScreenshotPathArgs : EventArgs
    {
        public string ScreenshotPath { get; set; }
    }
    public delegate void RowWithImageSelectedHandler(object sender, ScreenshotPathArgs args);
}
