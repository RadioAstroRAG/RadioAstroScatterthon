using System;
using System.IO;
using System.Windows.Forms;

namespace MeteorWatch
{
    partial class Scatterthon
    {
        private enum ConfigParam
        {
            OriginalLogDirectory,
            UpdatedLogDirectory,
            OriginalScreenshotDirectory,
            RemovedScreenshotDirectory,
            CopiedScreentshotDirectory,
            RmobOutputDirectory,
            ScreenshotCaptureDelay,
        }

        private void btnUpdateCaptureDelay_Click(object sender, EventArgs e)
        {
            // Save integer settings...
            try
            {
                config.ScreenshotsDelay = int.Parse(txtCaptureDelay.Text.Trim());
            }
            catch
            {
                MessageBox.Show("Please check numeric configuration values.", "Settings in the wrong format...");
            }
        }

        private void btnUpdateTopMeteorCount_Click(object sender, EventArgs e)
        {
            // Save integer settings...
            try
            {
                config.AnnualTopMeteorCount = int.Parse(txtAnnualTopMeteorCount.Text.Trim());

                if (currentRmobDate != null)
                {
                    RedrawPreview(currentRmobDate);
                }
            }
            catch
            {
                MessageBox.Show("Please check numeric configuration values.", "Settings in the wrong format...");
            }
        }

        private void txtOriginalLogsDirectory_DoubleClick(object sender, EventArgs e)
        {
            SetDirectoryInConfiguration(txtOriginalLogsDirectory.Text, ConfigParam.OriginalLogDirectory);
        }

        private void txtUpdatedLogsDirectory_DoubleClick(object sender, EventArgs e)
        {
            SetDirectoryInConfiguration(txtUpdatedLogsDirectory.Text, ConfigParam.UpdatedLogDirectory);
        }

        private void txtOriginalScreenshotsDirectory_DoubleClick(object sender, EventArgs e)
        {
            SetDirectoryInConfiguration(txtOriginalScreenshotsDirectory.Text, ConfigParam.OriginalScreenshotDirectory);
        }

        private void txtRemovedScreenshotsDirectory_DoubleClick(object sender, EventArgs e)
        {
            SetDirectoryInConfiguration(txtRemovedScreenshotsDirectory.Text, ConfigParam.RemovedScreenshotDirectory);
        }

        private void txtCopiedScreenshotsDirectory_DoubleClick(object sender, EventArgs e)
        {
            SetDirectoryInConfiguration(txtCopiedScreenshotsDirectory.Text, ConfigParam.CopiedScreentshotDirectory);
        }

        private void txtRmobFilesDirectory_DoubleClick(object sender, EventArgs e)
        {
            SetDirectoryInConfiguration(txtRmobFilesDirectory.Text, ConfigParam.RmobOutputDirectory);
        }

        /// <summary>
        /// Applies configuration settings to the App.config file and updates the config object.
        /// </summary>
        /// <param name="initialPath"></param>
        /// <param name="configParam"></param>
        private void SetDirectoryInConfiguration(string initialPath, ConfigParam configParam)
        {
            if (initialPath.StartsWith("%appdata%"))
            {
                initialPath = initialPath.Replace("%appdata%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
            if (Directory.Exists(initialPath))
            {
                dlgOpenDirectory.SelectedPath = initialPath;
            }
            if (DialogResult.OK == dlgOpenDirectory.ShowDialog() &&
                Directory.Exists(dlgOpenDirectory.SelectedPath))
            {
                switch (configParam)
                {
                    case ConfigParam.OriginalLogDirectory:
                        config.OriginalLogsDirectory = dlgOpenDirectory.SelectedPath;
                        break;

                    case ConfigParam.OriginalScreenshotDirectory:
                        config.OriginalScreenshotsDirectory = dlgOpenDirectory.SelectedPath;
                        break;

                    case ConfigParam.CopiedScreentshotDirectory:
                        config.CopiedScreenshotsDirectory = dlgOpenDirectory.SelectedPath;
                        break;

                    case ConfigParam.RemovedScreenshotDirectory:
                        config.RemovedScreenshotsDirectory = dlgOpenDirectory.SelectedPath;
                        break;

                    case ConfigParam.RmobOutputDirectory:
                        config.RmobFilesDirectory = dlgOpenDirectory.SelectedPath;
                        break;

                    case ConfigParam.UpdatedLogDirectory:
                        config.UpdatedLogsDirectory = dlgOpenDirectory.SelectedPath;
                        break;

                    default:
                        break;
                }
            }
        }

        private void btnApplySettings_Click(object sender, EventArgs e)
        {
            // Save integer settings...
            try
            {
                config.ScreenshotsDelay = int.Parse(txtCaptureDelay.Text.Trim());
                config.AnnualTopMeteorCount = int.Parse(txtAnnualTopMeteorCount.Text.Trim());

                if (currentRmobDate != null)
                {
                    RedrawPreview(currentRmobDate);
                }
            }
            catch
            {
                MessageBox.Show("Please check numeric configuration values.", "Settings in the wrong format...");
            }

            ClearScreenshots();

            LoadLogFiles();
        }


    }
}
