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
            HighResolutionScreenshotsDirectory,
            RemovedScreenshotDirectory,
            CopiedScreentshotDirectory,
            RmobOutputDirectory,
            ScreenshotCaptureDelay,
        }

        private void btnSaveProgress_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnUpdateTopMeteorCount_Click(object sender, EventArgs e)
        {
            // Save integer settings...
            try
            {
                config.AnnualTopMeteorCount = int.Parse(txtAnnualTopMeteorCount.Text.Trim());

                // See if we can and should redraw the colorgramme based on the new annual count.
                if (currentRmobDate != null && radioColourByYear.Checked)
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
            SetDirectoryInConfiguration(txtOriginalLogsDirectory.Text, ConfigParam.OriginalLogDirectory, true);

            txtOriginalLogsDirectory.Text = config.OriginalLogsDirectory;
        }

        private void txtOriginalScreenshotsDirectory_DoubleClick(object sender, EventArgs e)
        {
            SetDirectoryInConfiguration(txtOriginalScreenshotsDirectory.Text, ConfigParam.OriginalScreenshotDirectory, true);

            txtOriginalScreenshotsDirectory.Text = config.OriginalScreenshotsDirectory;
        }

        private void txtHighResolutionScreenshots_DoubleClick(object sender, EventArgs e)
        {
            SetDirectoryInConfiguration(txtHighResolutionScreenshots.Text, ConfigParam.HighResolutionScreenshotsDirectory, true);

            txtHighResolutionScreenshots.Text = config.HighResolutionScreenshotsDirectory;
        }

        private void checkPagination_CheckedChanged(object sender, EventArgs e)
        {
            bool usePages = ((sender as CheckBox).CheckState == CheckState.Checked);

            config.UsePagination = usePages;

            txtContinuousCaptureSpan.Enabled = usePages;
        }


        /// <summary>
        /// Applies configuration settings to the App.config file and updates the config object.
        /// </summary>
        /// <param name="initialPath"></param>
        /// <param name="configParam"></param>
        private void SetDirectoryInConfiguration(string initialPath, ConfigParam configParam, bool usePopup)
        {
            if (initialPath.StartsWith("%appdata%"))
            {
                initialPath = initialPath.Replace("%appdata%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
            if (usePopup)
            {            
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

                        case ConfigParam.HighResolutionScreenshotsDirectory:
                            config.HighResolutionScreenshotsDirectory = dlgOpenDirectory.SelectedPath;
                            break;

                        default:
                            break;
                    }
                }
            }
            else
            {
                switch (configParam)
                {
                    case ConfigParam.OriginalLogDirectory:
                        config.OriginalLogsDirectory = txtOriginalLogsDirectory.Text;
                        break;

                    case ConfigParam.OriginalScreenshotDirectory:
                        config.OriginalScreenshotsDirectory = txtOriginalScreenshotsDirectory.Text;
                        break;

                    case ConfigParam.HighResolutionScreenshotsDirectory:
                        config.HighResolutionScreenshotsDirectory = txtHighResolutionScreenshots.Text;
                        break;

                    default:
                        break;
                }
            }
        }

        private void btnApplySettings_Click(object sender, EventArgs e)
        {
            if (!startup)
            {
                // Save all configuration settings...
                SaveSettings();
                ClearScreenshots();
            }

            LoadLogFiles();

            DisplayHighResolutionThumbnails();

            // Save integer settings...
            try
            {                
                config.AnnualTopMeteorCount = int.Parse(txtAnnualTopMeteorCount.Text.Trim());

                if (currentRmobDate != null)
                {
                    string rmobFileToLoad;

                    MakeRmobFileName(false, out rmobFileToLoad);

                    LoadRmobFile(rmobFileToLoad);
                }
            }
            catch
            {
                MessageBox.Show("Please check numeric configuration values.", "Settings in the wrong format...");
            }

            // Make sure you can navigate from the keyboard from the start...
            logFileComponent.Focus();
        }

        public void SaveSettings()
        {
            SetDirectoryInConfiguration(txtOriginalLogsDirectory.Text, ConfigParam.OriginalLogDirectory, false);
            SetDirectoryInConfiguration(txtOriginalScreenshotsDirectory.Text, ConfigParam.OriginalScreenshotDirectory, false);
            SetDirectoryInConfiguration(txtHighResolutionScreenshots.Text, ConfigParam.HighResolutionScreenshotsDirectory, false);

            SetOffsetsInConfiguration();
            
            //this.txtOffsetDuration//ScreenshotsDelay
            if (cmbStationNames.SelectedItem != null)
            {            
                string selectedStation = cmbStationNames.SelectedItem.ToString();

                if (chkDefaultStation.Checked && !string.IsNullOrEmpty(selectedStation))
                {
                    // Save the default station name...
                    config.DefaultStation = selectedStation;
                }
                else
                {
                    config.DefaultStation = string.Empty;
                }

                config.UpdateConfigurationSettings(selectedStation);
            }
        }

        private void SetOffsetsInConfiguration()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtScreenshotsDelay.Text))
                {
                    config.ScreenshotsDelay = int.Parse(txtScreenshotsDelay.Text);
                }
                if (!string.IsNullOrEmpty(txtContinuousCaptureSpan.Text))
                {
                    config.CaptureSpan = int.Parse(txtContinuousCaptureSpan.Text);
                }
            }
            catch
            {
                MessageBox.Show("Please ensure all 'duration' and 'span' periods are specified as integer numbers...");
            }
        }
    }
}
