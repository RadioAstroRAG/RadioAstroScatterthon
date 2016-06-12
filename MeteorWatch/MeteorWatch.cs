using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MeteorWatch
{
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

    public partial class Scatterthon : Form
    {
        /// Each line is similar to: 14:16:18,12,-93.4,-112.9,2335.8,2
        /// File names: event_log20140628 (log) and event140629035833 (image).
        ///               

        #region Fields

        ConfigObject config = null;
        
        private static int currentLogIndex;
        private static bool startup = true;

        private string currentScreenshotFile;
        private string currentLogFileName;
        private int totalLogFiles = 0;
        
        // Fields used in stats and graphs...
        private decimal timestampDecimal = 0;

        // High-resolution screenshots...
        private int numberOfCaptures = 0;
        // Screenshots within Current Interval...
        private List<string> capturesWithinInterval = new List<string>();
        // Marks the first image to be displayed in the 3-image window...
        private int hrStartOfDisplayedArray = 0;

        // Categories of phenomenon...
        List<string> categories = new List<string>();

        // Fields used in colourgram...
        Dictionary<DateTime, Dictionary<int, int>> colorgram = new Dictionary<DateTime, Dictionary<int, int>>();
        
        DateTime currentLogDate = new DateTime();
        DateTime currentRmobDate = new DateTime();

        // Maps month and year (as a string) to the RMOB file...
        Dictionary<string, RmobFile> rmobFiles = new Dictionary<string, RmobFile>();
        string invalidTopCountMessage = "Please enter values between the bounds of the actual value (or 24 as a minimum) and 500.";

        Color[] standardRange = new Color[5];

        #endregion

        #region Constructor

        public Scatterthon()
        {
            startup = true;

            InitializeComponent();

            InitializeConfiguration();

            // Hide 'Annual' tab for now...
            tabMain.TabPages.Remove(tabAnnual);

            tabConfig.Select();
            // Show the tab page (insert it to the correct position)
            // tabMain.TabPages.Insert(0, tabAnnual);
            // ...and make it selected by default.
            //tabMain.SelectedIndex = 1;
                       
          
            // Initialise calendar...
            AddBoldDatesForExistingCleansedFiles();
            monthCalendar2.MaxDate = DateTime.Now;

            standardRange[0] = Color.FromArgb(0, 64, 255);
            standardRange[1] = Color.FromArgb(2, 193, 252);
            standardRange[2] = Color.FromArgb(124, 255, 149);
            standardRange[3] = Color.FromArgb(255, 238, 18);
            standardRange[4] = Color.FromArgb(250, 2, 0);
            
            DisplayStandardDataGrid();
                                
            int topMonthCount = FindHighestCountThisMonth(DateTime.Now);

            LoadRmobDataGrid(topMonthCount);

            // Make sure we display a screenshot when a corresponding row is selected in log file...
            logFileComponent.ScreenshotRowSelected += logFileViewer2_ScreenshotRowSelected;
            // And remove it when no screenshot should be shown...
            logFileComponent.ScreenshotDeselected += logFileViewer2_ScreenshotDeselected;
            
            categories.Add("Aircraft");
            categories.Add("Head echo");
            categories.Add("Interference");
            categories.Add("Meteor trail");
            categories.Add("Moon bounce");
            categories.Add("Satellite");
            categories.Add("Query");

            //*****************************

            SetupTwentyFourHourMargin();
        }

        public void SetCurrentLogIndex(bool? increment)
        {
            // Do another check on how many files we have...
            string[] files = Directory.GetFiles(config.OriginalLogsDirectory, "event_log20*.txt");

            if (files.Length == 0 || files.Length == 1)
            {
                btnPrevLog.Enabled = false;
                btnNextLog.Enabled = false;

                if (increment == null)
                {
                    MessageBox.Show("You cannot navigate away from the current log file...", "Not enough files");
                }
                return;
            }

            if (increment == null)
            {
                int inputFileNumber = int.Parse(txtLogIndex.Text);

                if (inputFileNumber == 0 || inputFileNumber > files.Length)
                {
                    MessageBox.Show(string.Format("Please enter a number between 1 and {0}", files.Length), "Incorrect file number");
                    return;
                }
                else
                {
                    currentLogIndex = inputFileNumber - 1;
                }
            }
            else if (increment == true)
            {
                if (files.Length > currentLogIndex)
                {
                    currentLogIndex++;
                }
            }
            else
            {
                if (currentLogIndex > 0)
                {
                    currentLogIndex--;
                }
            }

            SaveData();

            if (checkDropHistory.Checked)
            {
                logFileComponent.ClearUndoHistory();
            }        

            // Take care of the "Previous Log" button...
            if (currentLogIndex == 0)
            {
                btnPrevLog.Enabled = false;
            }
            else if (currentLogIndex >= 1)
            {
                btnPrevLog.Enabled = true;
            }

            // Take care of the "Next Log" button...
            if (currentLogIndex < (files.Length - 1))
            {
                btnNextLog.Enabled = true;
            }
            else if (currentLogIndex == (files.Length - 1))
            {
                btnNextLog.Enabled = false;
            }

            LoadLogFile();
        }

        private void InitializeConfiguration()
        {
            List<string> configuredStations = UsingConfigurationCollectionAttribute.GetStationNames();
            cmbStationNames.Items.AddRange(configuredStations.ToArray());

            config = new ConfigObject(this, string.Empty);

            // Populate Config tab...
            config.LoadConfigurationSettings("Default");

            chkDefaultStation.Checked = !string.IsNullOrEmpty(config.DefaultStation);

            if (cmbStationNames.Items.Contains(config.DefaultStation))
            {
                cmbStationNames.SelectedItem = config.DefaultStation;
                config.CurrentStation = config.DefaultStation;
            }
        }

        /// <summary>
        /// Display the settings in the config tab...
        /// </summary>
        /// <param name="stationName"></param>
        private void LoadConfiguration(string stationName)
        {
            config.LoadConfigurationSettings(stationName);

            txtOriginalLogsDirectory.Text = config.OriginalLogsDirectory;
            txtOriginalScreenshotsDirectory.Text = config.OriginalScreenshotsDirectory;
            txtHighResolutionScreenshots.Text = config.HighResolutionScreenshotsDirectory;
            txtContinuousCaptureSpan.Text = config.CaptureSpan.ToString();
            txtScreenshotsDelay.Text = config.ScreenshotsDelay.ToString();
            txtAnnualTopMeteorCount.Text = config.AnnualTopMeteorCount.ToString();
        }

        private void SetupTwentyFourHourMargin()
        {
            for (int i = 0; i < 24; i++)
            {
                int index = TwentyFourHourGrid.Rows.Add();

                TwentyFourHourGrid.Rows[index].Height = 20;
                TwentyFourHourGrid.Rows[index].HeaderCell.Value = index.ToString();
                TwentyFourHourGrid.Rows[index].HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }

        void logFileViewer2_ScreenshotDeselected(object sender, EventArgs e)
        {
            if (!startup)
            {
                ClearScreenshots();
            }
        }

        void logFileViewer2_ScreenshotRowSelected(object sender, LogComponent.ScreenshotPathArgs args)
        {
            LoadScreenshotInPictureBox(args.ScreenshotPath, picBoxScreenshot);
        }

        #endregion

        private void LoadRmobDataGrid(int maxThisMonth)
        {
            // Set the maximum range cell...
            dataGridView1.Rows[23].Cells[33].Value = maxThisMonth.ToString();
            dataGridView1.Rows[23].Cells[33].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            List<Color> colorList = PrepareListOfMultiColours(maxThisMonth);
                        
            // dataGridView1.Rows[hour].Cells[day].Style.BackColor = Color.Red

            // Prepare the background black...
            for (int i = 0; i < DateTime.Now.Month; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.Black;
                }
            }
        }

        private Dictionary<DateTime, int> SumUpInstancesPerPeriod(List<DateTime> timestamps, int daysInMonth, out int numberOfRows, out int maxEventsThisMonth)
        {
            int selectedPeriod = comboPeriod.SelectedIndex;
            numberOfRows = 24;
            maxEventsThisMonth = 24;

            switch (selectedPeriod)
            {
                case 0:
                    numberOfRows = 24;
                    break;
                case 1:
                    numberOfRows = 24 * 2;
                    break;
                case 2:
                    numberOfRows = 24 * 4;
                    break;
            }

            // How many minutes is each offset?
            int offsetInMinutes = (24 * 60) / (numberOfRows);

            IQueryable<DateTime> qTimestamps = timestamps.OfType<DateTime>().AsQueryable().OrderByDescending(dt => dt);

            // Spanning a month...
            DateTime startPoint = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1, 0, 0, 0, 0);
            DateTime endPoint = startPoint.Add(new TimeSpan(daysInMonth, 0, 0, 0, 0));

            TimeSpan period = new TimeSpan(0, offsetInMinutes, 0);

            Dictionary<DateTime, int> instancesPerPeriod = new Dictionary<DateTime, int>();

            // Go through each day of the month...
            while (startPoint < endPoint)
            {
                // Select all timestamps falling within this period...
                int timestampCount = qTimestamps.Count(t => t >= startPoint && t < (startPoint + period));

                instancesPerPeriod.Add(startPoint, timestampCount);

                // Increment the period
                startPoint = startPoint + period;
            }

            maxEventsThisMonth = instancesPerPeriod.OfType<KeyValuePair<DateTime, int>>().AsQueryable().Max(kvp => kvp.Value);

            return instancesPerPeriod;
        }

        private void aboutScatterthonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutScatterthon box = new AboutScatterthon())
            {
                box.ShowDialog(this);
            }
        }

        private void showTraceWindowMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Closing the trace window that will open next will result in closing down the application. Are you sure you wish to proceed?", "Warning...", MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                AllocConsole();
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void btnDoneScreenshotValidation_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            string[] fileContent = logFileComponent.LogFileContentSortedByTime;

            if (fileContent.Length > 0)
            {
                string savedFileName = SaveBasicLogFile(fileContent);

                SaveTimeStamps(fileContent);

                fileContent = logFileComponent.LogFileContentSortedByTime;

                if (checkGenerateRMOB.Checked)
                {
                    ProcessRmobFileData(fileContent);
                    SaveRmobFile();
                }

                // Update labels if necessary...
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(savedFileName);
                if (!currentLogFileName.Contains(fileNameWithoutExtension))
                {
                    lblLogName.Text = savedFileName;
                    currentLogFileName = fileNameWithoutExtension;
                }
            }
            else
            {
                MessageBox.Show("Unless you are trying to save an empty file - please remove the currently loaded log file, as it seems to have incorrect format...", "Warning");
            }
        }

        private void SaveTimeStamps(string[] fileContent)
        {
            string fileName = Path.GetFileNameWithoutExtension(lblLogName.Text).Substring(0, "event_log20001201".Length);            

            int classIndex = (int)Index.Class;
            int timeIndex = (int)Index.Time;

            #region List of classifications...

            //"Aircraft",
            //"Head echo",
            //"Interference",
            //"Meteor trail",
            //"Moon bounce",
            //"Remove",
            //"Satellite",
            //"Query"

            #endregion

            foreach (string category in categories)
            {
                SaveSpecificTimeStamps(category.Replace(" ", ""), category, fileName, fileContent, classIndex, timeIndex);
            }
            SaveUnclassifiedTimeStamps(fileName, fileContent, classIndex, timeIndex);
        }

        private void SaveSpecificTimeStamps(string directoryName, string category, string logFileName, string[] fileContent, int classIndex, int timeIndex)
        {
            List<string> timesForCategory = new List<string>();

            directoryName = Path.Combine(new string[] { config.CurrentStation, directoryName });

            if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);

            foreach (string line in fileContent)
            {
                string[] parts = line.Split(',');

                // Add the timestamp for this event if it matches specified category...
                if (parts.Length > 6 && parts[classIndex] == category)
                {
                    string timestamp = parts[timeIndex];

                    timesForCategory.Add(timestamp);                    
                }
            }

            // Save our new files there...
            File.WriteAllLines(Path.Combine(directoryName, string.Format("{0}_{1}.txt", logFileName, category)), timesForCategory);
        }

        private void SaveUnclassifiedTimeStamps(string logFileName, string[] fileContent, int classIndex, int timeIndex)
        {
            string directoryName = Path.Combine(new string[] { config.CurrentStation, "Unclassified" });

            List<string> timesForCategory = new List<string>();

            if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);

            foreach (string line in fileContent)
            {
                string[] parts = line.Split(',');

                // Add the timestamp for this event if it is not classified...
                if (parts.Length > 6 && string.IsNullOrEmpty(parts[classIndex]))
                {
                    string timestamp = parts[timeIndex];

                    timesForCategory.Add(timestamp);
                }
            }

            // Save our new files there...
            File.WriteAllLines(Path.Combine(directoryName, string.Format("{0}_noclass.txt", logFileName)), timesForCategory);
        }

        /// <summary>
        /// Exports a csv version of the Data grid view as a file..
        /// </summary>
        /// <param name="fileContent">Content of Data grid view</param>
        private string SaveBasicLogFile(string[] fileContent)
        {
            // For example, C:\logfiles\event_log20140805.txt...
            string fileName = Path.GetFileNameWithoutExtension(lblLogName.Text).Substring(0, "event_log20140804".Length);
            string directory = config.UpdatedLogsDirectory;

            // Save our new file there...
            string savedFileName = Path.Combine(directory, string.Format("{0}_saved.txt", fileName));
            File.WriteAllLines(savedFileName, fileContent);

            return savedFileName;
        }

        private void btnNextLog_Click(object sender, EventArgs e)
        {
            ClearScreenshots();
            SetCurrentLogIndex(true);
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
            ClearScreenshots();
            SetCurrentLogIndex(false);
        }

        private void Scatterthon_Load(object sender, EventArgs e)
        {
            btnApplySettings_Click(null, null);

            // We are now fully initialized on startup...
            startup = false;
        }

        private void btnView_Click(object sender, EventArgs e)
        {          
            LoadPreviewDataGrid();
        }

        private void cmbStationNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = cmbStationNames.SelectedItem.ToString();

            if (chkDefaultStation.Checked)
            {
                config.DefaultStation = selectedItem;
            }
            // Load the corresponding settings...           
            config.CurrentStation = selectedItem;

            // Check that this station name exists in the config section...
            LoadConfiguration(selectedItem);

        }

        private void btnNewStation_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter your Observing Station name, keeping in mind that it will be used as a folder name...", "Observing Station Name", "Default");

            if (!string.IsNullOrEmpty(input))
            {
                cmbStationNames.Items.Add(input);
                cmbStationNames.SelectedItem = input;
            }
        }

        private void checkedListClasses_SelectedValueChanged(object sender, EventArgs e)
        {
            btnView.Enabled = (checkedListClasses.CheckedItems.Count > 0);
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((sender as TabControl).SelectedTab.Text)
            {
                case "Preview":
                    if (!string.IsNullOrEmpty(currentLogFileName))
                    {
                        GetCurrentLogDate(currentLogFileName, out currentLogDate);
                        dateTimePicker1.Value = currentLogDate;
                    }
                    break;

                case "RMOB":
                    if (!string.IsNullOrEmpty(currentLogFileName))
                    {
                        GetCurrentLogDate(currentLogFileName, out currentLogDate);
                        RedrawPreview(currentLogDate);
                    }
                    break;

                default:
                    break;
            }
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void Scatterthon_KeyDown(object sender, KeyEventArgs e)
        {
            logFileComponent.LogFileViewer_KeyDown(sender, e);
        }

        private void txtLogIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            ClearScreenshots();
            SetCurrentLogIndex(null);
        }

        private void Scatterthon_FormClosing(object sender, FormClosingEventArgs e)
        {
            string messageBoxMessage = "If you want to save your work, please click 'Cancel', then 'Save'. \nIf your work has been saved, please click 'OK' to continue exiting Scatterthon.";

            DialogResult result = MessageBox.Show(messageBoxMessage, 
                "Save your work...", 
                MessageBoxButtons.OKCancel, 
                MessageBoxIcon.None, 
                MessageBoxDefaultButton.Button2);

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

    }
}
