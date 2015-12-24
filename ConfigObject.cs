using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MeteorWatch
{
    public class ConfigObject
    {
        public ConfigObject(Scatterthon mainForm)
        {
            this.mainForm = mainForm;
        }

        #region Fields
        Scatterthon mainForm = null;

        // This is where all log files will be loaded from...
        private string logOriginalsDirectory;
        // For storing copies of cleansed log files...
        private string logUpdatesDirectory;
        // This is where all screenshots will be loaded from...
        private string screenshotOriginalsDirectory;
        // To hold your rejected screenshots.
        private string screenshotsRemovedDirectory;

        // For when you need to assemble screenshots for double-checking with smb else.
        private string screenshotsCopiedDirectory;
        // For calculating offset between timestamps in log file and 
        // the timestamps in screenshot file names.
        private int screenshotDelay;
        private int annualTopMeteorCount;

        private string excelFileName = String.Empty;

        private string rmobFilesDirectory = String.Empty;
        #endregion

        #region Properties
        public string OriginalLogsDirectory
        {
            get { return string.IsNullOrEmpty(logOriginalsDirectory) ? string.Empty : logOriginalsDirectory; }
            set 
            {
                string path = AppDataReplacedInPath(value);

                if (Directory.Exists(path))
                {
                    logOriginalsDirectory = path;
                    mainForm.txtOriginalLogsDirectory.Text = path;
                    SaveConfigFile("logOriginalPath", path);
                }
            }
        }

        public string UpdatedLogsDirectory
        {
            get { return string.IsNullOrEmpty(logUpdatesDirectory) ? string.Empty : logUpdatesDirectory; }
            set 
            {
                string path = AppDataReplacedInPath(value);

                if (Directory.Exists(path))
                {
                    logUpdatesDirectory = path;
                    mainForm.txtUpdatedLogsDirectory.Text = path;
                    SaveConfigFile("logUpdatesPath", path);
                }
            }
        }

        private string AppDataReplacedInPath(string initialPath)
        {
            if (initialPath.StartsWith("%appdata%"))
            {
                return initialPath.Replace("%appdata%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
            return initialPath;
        }

        public string OriginalScreenshotsDirectory
        {
            get { return string.IsNullOrEmpty(screenshotOriginalsDirectory) ? string.Empty : screenshotOriginalsDirectory; }
            set 
            {
                string path = AppDataReplacedInPath(value);

                if (Directory.Exists(path))
                {
                    screenshotOriginalsDirectory = path;
                    mainForm.txtOriginalScreenshotsDirectory.Text = path;
                    SaveConfigFile("screenshotOriginalPath", path);

                }
            }
        }

        public string RemovedScreenshotsDirectory
        {
            get { return string.IsNullOrEmpty(screenshotsRemovedDirectory) ? string.Empty : screenshotsRemovedDirectory; }
            set 
            {
                string path = AppDataReplacedInPath(value);

                if (Directory.Exists(path))
                {
                    screenshotsRemovedDirectory = path;
                    mainForm.txtRemovedScreenshotsDirectory.Text = path;
                    SaveConfigFile("screenshotRemovePath", path);
                }
            }
        }

        public string CopiedScreenshotsDirectory
        {
            get { return string.IsNullOrEmpty(screenshotsCopiedDirectory) ? string.Empty : screenshotsCopiedDirectory; }
            set
            {
                string path = AppDataReplacedInPath(value);

                if (Directory.Exists(path))
                { 
                    screenshotsCopiedDirectory = path;
                    mainForm.txtCopiedScreenshotsDirectory.Text = path;
                    SaveConfigFile("screenshotCopyPath", path);
                }
            }
        }

        public int ScreenshotsDelay
        {
            get { return screenshotDelay; }
            set 
            {   
                screenshotDelay = value;
                mainForm.txtCaptureDelay.Text = value.ToString();
                SaveConfigFile("screenshotDelay", value.ToString());
            }
        }

        public int AnnualTopMeteorCount
        {
            get { return annualTopMeteorCount; }
            set
            {
                annualTopMeteorCount = value;
                mainForm.txtAnnualTopMeteorCount.Text = value.ToString();
                SaveConfigFile("annualTopMeteorCount", value.ToString());
            }
        }

        public string RmobFilesDirectory
        {
            get { return string.IsNullOrEmpty(rmobFilesDirectory) ? string.Empty : rmobFilesDirectory; }
            set
            {
                string path = AppDataReplacedInPath(value);

                if (Directory.Exists(path))
                { 
                    rmobFilesDirectory = path;
                    mainForm.txtRmobFilesDirectory.Text = path;
                    SaveConfigFile("rmobOutputDirectory", path);
                }
            }
        }

        public string ExcelFileName
        {
            get { return excelFileName; }
            set {
                if (File.Exists(value))
                {
                    excelFileName = value;

                    // Save file name to configuration file...
                    SaveConfigFile("spreadsheet", excelFileName);
                }
            }
        }

        #endregion

        public void LoadConfigurationSettings()
        {
            try
            {
                int temp = 0;
                int.TryParse(ConfigurationManager.AppSettings["screenshotDelay"], out temp);
                this.ScreenshotsDelay = temp;
                
                int.TryParse(ConfigurationManager.AppSettings["annualTopMeteorCount"], out temp);
                this.AnnualTopMeteorCount = temp;

                this.OriginalScreenshotsDirectory = ConfigurationManager.AppSettings["screenshotOriginalPath"].ToString();

                this.OriginalLogsDirectory = ConfigurationManager.AppSettings["logOriginalPath"].ToString();

                this.UpdatedLogsDirectory = ConfigurationManager.AppSettings["logUpdatesPath"].ToString();

                this.RemovedScreenshotsDirectory = ConfigurationManager.AppSettings["screenshotRemovePath"].ToString();

                this.CopiedScreenshotsDirectory = ConfigurationManager.AppSettings["screenshotCopyPath"].ToString();

                this.RmobFilesDirectory = ConfigurationManager.AppSettings["rmobOutputDirectory"].ToString();
            }
            catch { }
        }

        private void SaveConfigFile(string key, string newValue)
        {
            try
            {
                Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

                string appPath = System.IO.Path.GetDirectoryName(assembly.Location);

                string exeName = assembly.ManifestModule.ToString();

                string configFile = System.IO.Path.Combine(appPath, string.Format("{0}.config", exeName));

                try
                {
                    var dir = new DirectoryInfo(appPath);

                    foreach (var fileInDirectory in dir.GetFiles("*", SearchOption.AllDirectories))
                        fileInDirectory.Attributes &= ~FileAttributes.ReadOnly;
                }
                catch(Exception ex)
                {

                }

                FileInfo file = new FileInfo(configFile);
                file.IsReadOnly = false;

                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();

                configFileMap.ExeConfigFilename = configFile;

                System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                config.AppSettings.Settings[key].Value = newValue;

                config.Save();
            }
            catch { }
        }
    }
}
