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
        public ConfigObject(Scatterthon mainForm, string currentStation)
        {
            this.mainForm = mainForm;
            this.currentStation = currentStation;
        }

        #region Fields
        Scatterthon mainForm = null;

        // Currently selected station...
        private string currentStation;

        // Default observing station name...
        private string defaultStation;
        
        // This is where all log files will be loaded from...
        private string logOriginalsDirectory;
        // This is where all screenshots will be loaded from...
        private string screenshotOriginalsDirectory;
        // This is where high-resolution screenshots will be loaded from...
        private string screenshotHighResolutionDirectory;

        // For storing copies of cleansed log files...
        private string logUpdatesDirectory = "Basic";
        // For when you need to assemble screenshots for double-checking with smb else.
        private string screenshotsCopiedDirectory = "Copies";
        // Where to save Rmob export files...
        private string rmobFilesDirectory = "Rmob";

        // For calculating offset between timestamps in log file and 
        // the timestamps in screenshot file names.
        private int screenshotDelay;        
        private int captureSpan;
        private bool usePagination;
        private int annualTopMeteorCount;
        
        
        #endregion

        #region Properties


        public string CurrentStation
        {
            set { currentStation = value; }
            get { return currentStation; }
        }

        public string DefaultStation
        {
            get { return string.IsNullOrEmpty(defaultStation) ? string.Empty : defaultStation; }
            set 
            {
                defaultStation = value;
                SaveConfigFile("defaultStation", value.ToString());
            }
        }

        public string OriginalLogsDirectory
        {
            get 
            { 
                return string.IsNullOrEmpty(logOriginalsDirectory) ? string.Empty : logOriginalsDirectory; 
            }
            set
            {
                logOriginalsDirectory = value;

                if (!string.IsNullOrEmpty(value) && !Directory.Exists(value))
                {
                    try
                    {
                        Directory.CreateDirectory(value);
                    }
                    catch 
                    {
                        // Keep the original directory for future reference...
                        // logOriginalsDirectory = string.Empty;
                    }
                }
            }
        }

        public string OriginalScreenshotsDirectory
        {
            get
            {
                return string.IsNullOrEmpty(screenshotOriginalsDirectory) ? string.Empty : screenshotOriginalsDirectory;
            }
            set
            {
                screenshotOriginalsDirectory = value;

                if (!string.IsNullOrEmpty(value) && !Directory.Exists(value))
                {
                    try
                    {
                        Directory.CreateDirectory(value);
                    }
                    catch 
                    {
                        // Keep the original directory for future reference...
                        // screenshotOriginalsDirectory = string.Empty;
                    }
                }
            }
        }

        public string HighResolutionScreenshotsDirectory
        {
            get
            {
                return string.IsNullOrEmpty(screenshotHighResolutionDirectory) ? string.Empty : screenshotHighResolutionDirectory;
            }
            set
            {
                screenshotHighResolutionDirectory = value;

                if (!string.IsNullOrEmpty(value) && !Directory.Exists(value))
                {
                    try
                    {
                        Directory.CreateDirectory(value);
                    }
                    catch
                    {
                        // Keep the original directory for future reference...
                        // screenshotHighResolutionDirectory = string.Empty;
                    }
                }
            }
        }

        public string UpdatedLogsDirectory
        {
            get 
            {
                string temp = Path.Combine(this.currentStation, logUpdatesDirectory);

                if (!Directory.Exists(temp))
                {
                    Directory.CreateDirectory(temp);
                }
                return temp; 
            }
        }

        public string CopiedScreenshotsDirectory
        {
            get
            {
                string temp = Path.Combine(this.currentStation, screenshotsCopiedDirectory);

                if (!Directory.Exists(temp))
                {
                    Directory.CreateDirectory(temp);
                }
                return temp;
            }
        }

        public string RmobFilesDirectory
        {
            get
            {
                string temp = Path.Combine(this.currentStation, rmobFilesDirectory);

                if (!Directory.Exists(temp))
                {
                    Directory.CreateDirectory(temp);
                }
                return temp;
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

        public int ScreenshotsDelay
        {
            get { return screenshotDelay; }
            set 
            {   
                screenshotDelay = value;                                
            }
        }

        public bool UsePagination
        {
            get { return usePagination; }
            set
            {
                usePagination = value;
            }
        }

        public int CaptureSpan
        {
            get { return captureSpan; }
            set
            {
                captureSpan = value;
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

        #endregion

        public void UpdateConfigurationSettings(string stationName)
        {
            try
            {
                // Get the current configuration file.
                Configuration systemConfig = GetSystemConfigObject();

                // Read and display the custom section.
                ObservingStationsSection myStations = systemConfig.GetSection("ObservingStations") as ObservingStationsSection;

                List<StationConfigElement> arrayOfStations = new List<StationConfigElement>();

                if (myStations != null)
                {
                    foreach(StationConfigElement stationElement in myStations.Stations)
                    {
                        if (stationElement.Name != stationName)
                        {
                            StationConfigElement station = new StationConfigElement(stationElement.Name,
                                                            stationElement.Logs,
                                                            stationElement.Screenshots,
                                                            stationElement.HighResolutions,
                                                            stationElement.CaptureSpan,
                                                            stationElement.CaptureDelay,
                                                            stationElement.UsePagination);

                            arrayOfStations.Add(station);
                        }
                    }

                    systemConfig.Sections.Remove("ObservingStations");
                }

                // Create a custom configuration section.
                ObservingStationsSection myStationsSection = new ObservingStationsSection();

                foreach(StationConfigElement stationElement in arrayOfStations)
                {
                    myStationsSection.Stations.Add(stationElement);
                }

                // Take care to save the changes...
                StationConfigElement updatedStation = new StationConfigElement(stationName,
                                                        this.OriginalLogsDirectory,
                                                        this.OriginalScreenshotsDirectory,
                                                        this.HighResolutionScreenshotsDirectory,
                                                        this.CaptureSpan,
                                                        this.ScreenshotsDelay,
                                                        this.UsePagination);

                //myStations.Stations.Add(updatedStation);
                myStationsSection.Stations.Add(updatedStation);

                // Add the custom section to the application configuration file. 
                if (systemConfig.Sections["ObservingStations"] == null)
                {
                    systemConfig.Sections.Add("ObservingStations", myStationsSection);
                }

                // Save the application configuration file.
                myStationsSection.SectionInformation.ForceSave = true;
                
                systemConfig.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("ObservingStations");
            }
            catch { }
        }

        private Configuration GetSystemConfigObject()
        {
            string appPath;
            Configuration mappedConfig = null;

            string configFile = GetConfigFileName(out appPath);

            if (File.Exists(configFile))
            {
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();

                configFileMap.ExeConfigFilename = configFile;

                mappedConfig = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            }

            return mappedConfig;
        }

        public void SaveConfigurationSettings(string stationName)
        {
            Configuration config = GetSystemConfigObject();

            if (!string.IsNullOrEmpty(stationName) && config != null)
            {
                try
                {
                    // Read and display the custom section.
                    ObservingStationsSection myStations = config.GetSection("ObservingStations") as ObservingStationsSection;

                    if (myStations == null)
                    {
                        UsingConfigurationCollectionAttribute.CreateCustomSection();
                    }

                    // Update station-specific properties...
                    myStations.Stations.Remove(stationName);

                    StationConfigElement station = new StationConfigElement(stationName, 
                                                                            this.OriginalLogsDirectory, 
                                                                            this.OriginalScreenshotsDirectory, 
                                                                            this.HighResolutionScreenshotsDirectory,
                                                                            this.CaptureSpan,
                                                                            this.ScreenshotsDelay,
                                                                            this.UsePagination);

                    myStations.Stations.Add(station);

                    // Process the common properties...
                    if (config.AppSettings.Settings["defaultStation"] != null)
                    {
                        config.AppSettings.Settings["defaultStation"].Value = this.DefaultStation;
                    }

                    config.AppSettings.Settings["annualTopMeteorCount"].Value = this.AnnualTopMeteorCount.ToString();

                    config.Save(ConfigurationSaveMode.Full);                                        
                }
                catch { }
            }
        }


        public void LoadConfigurationSettings(string stationName)
        {
                Configuration config = GetSystemConfigObject();

                try
                {
                    if (!string.IsNullOrEmpty(stationName))
                    {
                        // Read and display the custom section. 
                        ObservingStationsSection myStations = config.GetSection("ObservingStations") as ObservingStationsSection;                                                

                        StationConfigElement station = null; 

                        if (myStations != null)
                        {
                            for (int i = 0; i < myStations.Stations.Count; i++)
                            {
                                if (myStations.Stations[i].Name == stationName)
                                {
                                    station = myStations.Stations[i];
                                    break;
                                }
                            }

                            if (station == null)
                            {
                                station = new StationConfigElement(stationName, string.Empty, string.Empty, string.Empty, 300, 30, false);

                                myStations.Stations.Add(station);
                            }
                            this.OriginalLogsDirectory = station.Logs;
                            this.OriginalScreenshotsDirectory = station.Screenshots;
                            this.HighResolutionScreenshotsDirectory = station.HighResolutions;
                            this.CaptureSpan = station.CaptureSpan;
                            this.ScreenshotsDelay = station.CaptureDelay;
                            this.UsePagination = station.UsePagination;
                        }
                        else
                        {
                            UsingConfigurationCollectionAttribute.CreateCustomSection();
                        }
                    }

                    int temp = 0;

                    if (config.AppSettings.Settings["defaultStation"] != null)
                    {
                        this.DefaultStation = config.AppSettings.Settings["defaultStation"].Value.ToString();
                    }                    

                    int.TryParse(config.AppSettings.Settings["annualTopMeteorCount"].Value, out temp);
                    this.AnnualTopMeteorCount = temp;
                    
                }
                catch { }            
        }

        private void SaveConfigFile(string key, string newValue)
        {
            try
            {
                string configFile = MakeConfigFileWritable();

                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();

                configFileMap.ExeConfigFilename = configFile;

                System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                config.AppSettings.Settings[key].Value = newValue;

                config.Save();
            }
            catch { }
        }

        private static string MakeConfigFileWritable()
        {
            string appPath;

            string configFile = GetConfigFileName(out appPath);

            try
            {
                var dir = new DirectoryInfo(appPath);

                foreach (var fileInDirectory in dir.GetFiles("*", SearchOption.AllDirectories))
                    fileInDirectory.Attributes &= ~FileAttributes.ReadOnly;
            }
            catch (Exception ex)
            {

            }

            FileInfo file = new FileInfo(configFile);
            file.IsReadOnly = false;
            return configFile;
        }

        private static string GetConfigFileName(out string appPath)
        {
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            appPath = System.IO.Path.GetDirectoryName(assembly.Location);

            string exeName = assembly.ManifestModule.ToString();

            return System.IO.Path.Combine(appPath, string.Format("{0}.config", exeName));
        }
    }
}
