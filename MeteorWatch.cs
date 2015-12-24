using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MeteorWatch
{
    public partial class Scatterthon : Form
    {
        /// Each line is similar to: 14:16:18,12,-93.4,-112.9,2335.8,2
        /// File names: event_log20140628 (log) and event140629035833 (image).

        #region Fields

        ConfigObject config = null;

        // Sync the views of screenshots with the lines in the displayed log file.
        private static DisplaySync logsAndShots;
        private static int currentLogIndex;
        private static int currentScreenshotIndex;
        private string currentScreenshotFile;
        private bool scrollCaret = true;
        
        // Fields used in stats and graphs...
        private decimal timestampDecimal = 0;
        private bool reachedEOF = false;
        private bool createNewSeries = false;

        // Fields used in colourgram...
        Dictionary<DateTime, Dictionary<int, int>> colorgram = new Dictionary<DateTime, Dictionary<int, int>>();
        
        DateTime currentLogDate = new DateTime();
        DateTime currentRmobDate = new DateTime();

        // Maps month and year (as a string) to the RMOB file...
        Dictionary<string, RmobFile> rmobFiles = new Dictionary<string, RmobFile>();

        Color[] standardRange = new Color[5];

        #endregion

        #region Constructor

        public Scatterthon()
        {
            InitializeComponent();

            // Hide statistics tab for now...
            tabMain.TabPages.Remove(tabStatistics);
            tabConfig.Select();
            // Show the tab page (insert it to the correct position)
            // tabMain.TabPages.Insert(0, tabStatistics);

            config = new ConfigObject(this);

            // Populate Config tab in the lower pane.
            config.LoadConfigurationSettings();
            // ...and make it's selected by default.
            //tabMain.SelectedIndex = 1;

            // Populate Analysis tab in the lower pane.
            LoadChartAnalysis();  
          
            // Initialise calendar...
            AddBoldDatesForExistingCleansedFiles();
            monthCalendar2.MaxDate = DateTime.Now;

            //standardRange[0] = Color.FromArgb(1, 1, 203);
            //standardRange[1] = Color.FromArgb(1, 203, 198);
            //standardRange[2] = Color.FromArgb(15, 203, 1);
            //standardRange[3] = Color.FromArgb(250, 255, 41);
            //standardRange[4] = Color.Red;

            standardRange[0] = Color.FromArgb(0, 64, 255);
            standardRange[1] = Color.FromArgb(2, 193, 252);
            standardRange[2] = Color.FromArgb(124, 255, 149);
            standardRange[3] = Color.FromArgb(255, 238, 18);
            standardRange[4] = Color.FromArgb(250, 2, 0);
            
            DisplayStandardDataGrid();
                                
            int topMonthCount = FindHighestCountThisMonth(DateTime.Now);

            LoadDataGrid(topMonthCount);
        }

        #endregion

        private void LoadDataGrid(int maxThisMonth)
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

        private void aboutScatterthonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox1 box = new AboutBox1())
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
    }
}
