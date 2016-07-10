using LogComponent;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MeteorWatch
{
    partial class Scatterthon
    {
        private void btnCoalesce_Click(object sender, EventArgs e)
        {
            DirectoryInfo dirWithProcessedFiles = new DirectoryInfo(config.UpdatedLogsDirectory);
            string coalescedFile = String.Empty;

            FileInfo[] files = GetFilesToCoalesce(dirWithProcessedFiles, out coalescedFile);

            List<string> selectedItems = new List<string>();

            foreach (object itemChecked in checkedListClassesFilter.CheckedItems)
            {
                selectedItems.Add(itemChecked.ToString().Trim());
            }

            #region Pull the file content together...

            string coalescedFilePath = Path.Combine(config.CurrentStation, "Coalesced");

            if (!Directory.Exists(coalescedFilePath)) { Directory.CreateDirectory(coalescedFilePath); }

            string fullPathToFile = Path.Combine(coalescedFilePath, coalescedFile);
            List<string> linesToAppend = new List<string>();

            foreach(FileInfo file in files)
            {
                string[] lines = File.ReadAllLines(file.FullName);

                // Only include selected classifications...
                foreach (string line in lines)
                {
                    foreach(string classification in selectedItems)
                    {
                        if (line.Contains("," + classification + ","))
                        {
                            linesToAppend.Add(line);                            
                            break;
                        }
                    }                    
                }                
            }

            File.AppendAllLines(fullPathToFile, linesToAppend);

            #endregion

            // Pass fake screenshots directory to prevent the attempt to allocate images to log file records...
            logFileViewerFilter.LoadLogFileContent(fullPathToFile, "X:", config.ScreenshotsDelay, true, true, true);
            logFileViewerFilter.SelectFirstRow();

        }

        private FileInfo[] GetFilesToCoalesce(DirectoryInfo dirWithProcessedFiles, out string coalescedFileName)
        {
            FileInfo[] filesToCoalesce = new FileInfo[]{};
            coalescedFileName = "Empty.txt";

            if (radioCoalesceAll.Checked)
            {
                filesToCoalesce = dirWithProcessedFiles.GetFiles();
                coalescedFileName = string.Format("All_On_{0}.txt", DateTime.Now.ToString("yyyyMMddHHmm"));
            }
            else if (radioCoalesceMonth.Checked)
            {
                // Select all files matching the selected month and year...
                string month = dtpFilter.Value.ToString("MM");
                string year = dtpFilter.Value.ToString("yyyy");

                filesToCoalesce = dirWithProcessedFiles.GetFiles(string.Format("event_log{0}{1}*_saved.txt", year, month));
                coalescedFileName = string.Format("Month_{0}_On_{1}.txt", month, DateTime.Now.ToString("yyyyMMddHHmm"));
            }
            else if (radioCoalesceYear.Checked)
            {
                // Select all files matching selected year...
                string year = dtpFilter.Value.Year.ToString();

                filesToCoalesce = dirWithProcessedFiles.GetFiles(string.Format("event_log{0}*_saved.txt", year));
                coalescedFileName = string.Format("Year_{0}_On_{1}.txt", year, DateTime.Now.ToString("yyyyMMddHHmm"));
            }

            return filesToCoalesce;
        }
    }
}
