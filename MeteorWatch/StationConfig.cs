using MeteorWatch;
using System;
using System.Collections.Generic;
using System.Configuration;


// Define a custom section that contains a custom 
// UrlsCollection collection of custom UrlConfigElement elements. 
// This class shows how to use the ConfigurationCollectionAttribute. 
public class ObservingStationsSection : ConfigurationSection
{
    // Declare the Urls collection property using the 
    // ConfigurationCollectionAttribute. 
    // This allows to build a nested section that contains 
    // a collection of elements.
    [ConfigurationProperty("stations", IsDefaultCollection = false)]
    [ConfigurationCollection(typeof(StationsCollection),
        AddItemName = "add",
        ClearItemsName = "clear",
        RemoveItemName = "remove")]
    public StationsCollection Stations
    {
        get
        {
            // We are returning a collection of URLs...
            return (StationsCollection)base["stations"];
        }
    }
}

// Define the custom UrlsCollection that contains the  
// custom UrlsConfigElement elements. 
public class StationsCollection : ConfigurationElementCollection
{
    public override bool IsReadOnly()
    {
        return false;
    }

    public StationsCollection()
    {
        StationConfigElement station = (StationConfigElement)CreateNewElement();
        Add(station);
    }

    public override ConfigurationElementCollectionType CollectionType
    {
        get
        {
            return ConfigurationElementCollectionType.AddRemoveClearMap;
        }
    }

    protected override ConfigurationElement CreateNewElement()
    {
        return new StationConfigElement();
    }

    protected override Object GetElementKey(ConfigurationElement element)
    {
        return ((StationConfigElement)element).Name;
    }

    public StationConfigElement this[int index]
    {
        get
        {
            return (StationConfigElement)BaseGet(index);
        }
        set
        {
            if (BaseGet(index) != null)
            {
                BaseRemoveAt(index);
            }
            BaseAdd(index, value);
        }
    }

    new public StationConfigElement this[string Name]
    {
        get
        {
            return (StationConfigElement)BaseGet(Name);
        }
    }

    public int IndexOf(StationConfigElement url)
    {
        return BaseIndexOf(url);
    }

    public void Add(StationConfigElement url)
    {
        BaseAdd(url);
    }
    protected override void BaseAdd(ConfigurationElement element)
    {
        BaseAdd(element, false);
    }

    public void Remove(StationConfigElement url)
    {
        if (BaseIndexOf(url) >= 0)
            BaseRemove(url.Name);
    }

    public void RemoveAt(int index)
    {
        BaseRemoveAt(index);
    }

    public void Remove(string name)
    {
        BaseRemove(name);
    }

    public void Clear()
    {
        BaseClear();
    }
}

// Define the custom UrlsConfigElement elements that are contained  
// by the custom UrlsCollection. 
// Notice that you can change the default values to create new default elements. 
public class StationConfigElement : ConfigurationElement
{
    public StationConfigElement(String name, String logs, String screenshots, String highResolutions, int captureSpan, int captureDelay, bool usePagination)
    {
        this.Name = name;
        this.Logs = logs;
        this.Screenshots = screenshots;
        this.HighResolutions = highResolutions;
        this.CaptureSpan = captureSpan;
        this.CaptureDelay = captureDelay;
        this.UsePagination = usePagination;
    }

    public StationConfigElement()
    {
        this.Name = "Default";
        this.Logs = "C:\\";
        this.Screenshots = "C:\\";
        this.HighResolutions = "C:\\";
        this.CaptureSpan = 300;
        this.CaptureDelay = 30;
        this.UsePagination = false;
    }

    [ConfigurationProperty("name", DefaultValue = "Default",
        IsRequired = true, IsKey = true)]
    public string Name
    {
        get
        {
            return (string)this["name"];
        }
        set
        {
            this["name"] = value;
        }
    }

    [ConfigurationProperty("logs", DefaultValue = "C:\\", IsRequired = true)]
    public string Logs
    {
        get
        {
            return (string)this["logs"];
        }
        set
        {
            this["logs"] = value;
        }
    }

    [ConfigurationProperty("screenshots", DefaultValue = "C:\\", IsRequired = true)]
    public string Screenshots
    {
        get
        {
            return (string)this["screenshots"];
        }
        set
        {
            this["screenshots"] = value;
        }
    }

    [ConfigurationProperty("highResolutions", DefaultValue = "C:\\", IsRequired = true)]
    public string HighResolutions
    {
        get
        {
            return (string)this["highResolutions"];
        }
        set
        {
            this["highResolutions"] = value;
        }
    }

    [ConfigurationProperty("captureDelay", DefaultValue = (int)30, IsRequired = false)]
    [IntegerValidator(MinValue = 0, MaxValue = 360, ExcludeRange = false)]
    public int CaptureDelay
    {
        get
        {
            return (int)this["captureDelay"];
        }
        set
        {
            this["captureDelay"] = value;
        }
    }

    [ConfigurationProperty("captureSpan", DefaultValue = (int)300, IsRequired = false)]
    [IntegerValidator(MinValue = 0, MaxValue = 600, ExcludeRange = false)]
    public int CaptureSpan
    {
        get
        {
            return (int)this["captureSpan"];
        }
        set
        {
            this["captureSpan"] = value;
        }
    }

    [ConfigurationProperty("usePagination", DefaultValue = (bool)false, IsRequired = false)]
    public bool UsePagination
    {
        get
        {
            return (bool)this["usePagination"];
        }
        set
        {
            this["usePagination"] = value;
        }
    }

    public override bool IsReadOnly()
    {
        return false;
    }
}

public class UsingConfigurationCollectionAttribute
{

    // Create a custom section and save it in the  
    // application configuration file. 
    public static void CreateCustomSection()
    {
        try
        {
            // Create a custom configuration section.
            ObservingStationsSection myStationsSection = new ObservingStationsSection();
            myStationsSection.SectionInformation.RestartOnExternalChanges = false;

            // Get the current configuration file.
            System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);

            // Add the custom section to the application configuration file. 
            if (config.Sections["ObservingStations"] == null)
            {
                config.Sections.Add("ObservingStations", myStationsSection);
                config.Sections["ObservingStations"].SectionInformation.RestartOnExternalChanges = false;
            }

            // Save the application configuration file.
            myStationsSection.SectionInformation.ForceSave = true;
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("ObservingStations");

            Console.WriteLine("Created custom section in the application configuration file: {0}",
                config.FilePath);
            Console.WriteLine();

        }
        catch (ConfigurationErrorsException err)
        {
            Console.WriteLine("CreateCustomSection: {0}", err.ToString());
        }

    }

    public static List<string> GetStationNames()
    {
        List<string> stationNames = new List<string>();

        try
        {
            // Get the application configuration file.
            System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None) as Configuration;

            // Read and display the custom section.
            ObservingStationsSection myStations =
               config.GetSection("ObservingStations") as ObservingStationsSection;

            if (myStations == null)
                Console.WriteLine("Failed to load ObservingStationsSection.");
            else
            {
                Console.WriteLine("Stations defined in the configuration file:");
                for (int i = 0; i < myStations.Stations.Count; i++)
                {
                    stationNames.Add(myStations.Stations[i].Name);
                }
            }
            return stationNames;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public static List<string> ReadCustomSection(string stationName, ConfigObject localConfig)
    {
        List<string> stationNames = new List<string>();
        
        try
        {
            // Get the application configuration file.
            System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None) as Configuration;

            // Read and display the custom section.
            ObservingStationsSection myStations =
               config.GetSection("ObservingStations") as ObservingStationsSection;

            if (myStations == null)
                Console.WriteLine("Failed to load ObservingStationsSection.");
            else
            {
                Console.WriteLine("Stations defined in the configuration file:");
                for (int i = 0; i < myStations.Stations.Count; i++)
                {
                    stationNames.Add(myStations.Stations[i].Name);

                    Console.WriteLine("  Name={0} InputLogs={1} Screenshots={2} CaptureDelay={3}",
                        myStations.Stations[i].Name,
                        myStations.Stations[i].Logs,
                        myStations.Stations[i].Screenshots,
                        myStations.Stations[i].HighResolutions,
                        myStations.Stations[i].CaptureSpan,
                        myStations.Stations[i].CaptureDelay,
                        myStations.Stations[i].UsePagination);
                }
            }

        }
        catch (ConfigurationErrorsException err)
        {
            Console.WriteLine("ReadCustomSection(string): {0}", err.ToString());
        }

        return stationNames;
    }

}