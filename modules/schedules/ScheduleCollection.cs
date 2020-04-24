//-----------------------------------------------------------------------
// <copyright file="ScheduleCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace AutoScreenCapture
{
    /// <summary>
    /// A collection class to store and manage Schedule objects.
    /// </summary>
    public class ScheduleCollection : CollectionTemplate<Schedule>
    {
        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_SCHEDULE_NODE = "schedule";
        private const string XML_FILE_SCHEDULES_NODE = "schedules";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string SCHEDULE_NAME = "name";
        private const string SCHEDULE_ENABLED = "enabled";
        private const string SCHEDULE_MODE_ONETIME = "mode_onetime";
        private const string SCHEDULE_MODE_PERIOD = "mode_period";
        private const string SCHEDULE_CAPTUREAT = "captureat";
        private const string SCHEDULE_STARTAT = "startat";
        private const string SCHEDULE_STOPAT = "stopat";
        private const string SCHEDULE_MONDAY = "monday";
        private const string SCHEDULE_TUESDAY = "tuesday";
        private const string SCHEDULE_WEDNESDAY = "wednesday";
        private const string SCHEDULE_THURSDAY = "thursday";
        private const string SCHEDULE_FRIDAY = "friday";
        private const string SCHEDULE_SATURDAY = "saturday";
        private const string SCHEDULE_SUNDAY = "sunday";
        private readonly string SCHEDULE_XPATH;

        private static string AppCodename { get; set; }
        private static string AppVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ScheduleCollection()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/");
            sb.Append(XML_FILE_ROOT_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_SCHEDULES_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_SCHEDULE_NODE);

            SCHEDULE_XPATH = sb.ToString();
        }

        /// <summary>
        /// Loads the image schedules from the schedules.xml file.
        /// </summary>
        public void LoadXmlFileAndAddSchedules()
        {
            try
            {
                if (File.Exists(FileSystem.SchedulesFile))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(FileSystem.SchedulesFile);

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xSchedules = xDoc.SelectNodes(SCHEDULE_XPATH);

                    foreach (XmlNode xSchedule in xSchedules)
                    {
                        Schedule schedule = new Schedule();
                        XmlNodeReader xReader = new XmlNodeReader(xSchedule);

                        while (xReader.Read())
                        {
                            if (xReader.IsStartElement())
                            {
                                switch (xReader.Name)
                                {
                                    case SCHEDULE_NAME:
                                        xReader.Read();
                                        schedule.Name = xReader.Value;
                                        break;

                                    case SCHEDULE_ENABLED:
                                        xReader.Read();
                                        schedule.Enabled = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCHEDULE_MODE_ONETIME:
                                        xReader.Read();
                                        schedule.ModeOneTime = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCHEDULE_MODE_PERIOD:
                                        xReader.Read();
                                        schedule.ModePeriod = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCHEDULE_CAPTUREAT:
                                        xReader.Read();
                                        schedule.CaptureAt = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case SCHEDULE_STARTAT:
                                        xReader.Read();
                                        schedule.StartAt = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case SCHEDULE_STOPAT:
                                        xReader.Read();
                                        schedule.StopAt = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case SCHEDULE_MONDAY:
                                        xReader.Read();
                                        schedule.Monday = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCHEDULE_TUESDAY:
                                        xReader.Read();
                                        schedule.Tuesday = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCHEDULE_WEDNESDAY:
                                        xReader.Read();
                                        schedule.Wednesday = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCHEDULE_THURSDAY:
                                        xReader.Read();
                                        schedule.Thursday = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCHEDULE_FRIDAY:
                                        xReader.Read();
                                        schedule.Friday = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCHEDULE_SATURDAY:
                                        xReader.Read();
                                        schedule.Saturday = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCHEDULE_SUNDAY:
                                        xReader.Read();
                                        schedule.Sunday = Convert.ToBoolean(xReader.Value);
                                        break;
                                }
                            }
                        }

                        xReader.Close();

                        // Change the data for each Schedule that's being loaded if we've detected that
                        // the XML file is from an older version of the application.
                        if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                        {
                            Log.WriteDebugMessage("An old version of the schedules.xml file was detected. Attempting upgrade to new schema.");

                            Version v2250 = Settings.VersionManager.Versions.Get("Dalek", "2.2.5.0");
                            Version configVersion = Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                            if (v2250 != null && configVersion != null && configVersion.VersionNumber < v2250.VersionNumber)
                            {
                                Log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                // This is a new property for Schedule that was introduced in 2.2.5.0
                                // so any version before 2.2.5.0 needs to have it during an upgrade.
                                schedule.Enabled = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(schedule.Name))
                        {
                            Add(schedule);
                        }
                    }

                    if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                    {
                        SaveToXmlFile();
                    }
                }
                else
                {
                    Log.WriteDebugMessage($"WARNING: {FileSystem.SchedulesFile} not found. Unable to load schedules");

                    Log.WriteDebugMessage("Creating default Command Line Schedule for use with command line arguments such as -captureat, -startat, and -stopat");

                    Schedule schedule = new Schedule()
                    {
                        Name = "Command Line Schedule",
                        Enabled = false,
                        ModeOneTime = true,
                        ModePeriod = false,
                        CaptureAt = DateTime.Now,
                        StartAt = DateTime.Now,
                        StopAt = DateTime.Now
                    };

                    Add(schedule);

                    SaveToXmlFile();
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("ScheduleCollection::LoadXmlFileAndAddSchedules", ex);
            }
        }

        /// <summary>
        /// Saves the image schedules in the collection to the schedules.xml file.
        /// </summary>
        public void SaveToXmlFile()
        {
            try
            {
                XmlWriterSettings xSettings = new XmlWriterSettings
                {
                    Indent = true,
                    CloseOutput = true,
                    CheckCharacters = true,
                    Encoding = Encoding.UTF8,
                    NewLineChars = Environment.NewLine,
                    IndentChars = XML_FILE_INDENT_CHARS,
                    NewLineHandling = NewLineHandling.Entitize,
                    ConformanceLevel = ConformanceLevel.Document
                };

                if (string.IsNullOrEmpty(FileSystem.SchedulesFile))
                {
                    FileSystem.SchedulesFile = FileSystem.DefaultSchedulesFile;

                    if (File.Exists(FileSystem.ConfigFile))
                    {
                        using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                        {
                            sw.WriteLine("SchedulesFile=" + FileSystem.SchedulesFile);
                        }
                    }
                }

                if (File.Exists(FileSystem.SchedulesFile))
                {
                    File.Delete(FileSystem.SchedulesFile);
                }

                using (XmlWriter xWriter =
                    XmlWriter.Create(FileSystem.SchedulesFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_SCHEDULES_NODE);

                    foreach (Schedule schedule in base.Collection)
                    {
                        xWriter.WriteStartElement(XML_FILE_SCHEDULE_NODE);

                        xWriter.WriteElementString(SCHEDULE_ENABLED, schedule.Enabled.ToString());
                        xWriter.WriteElementString(SCHEDULE_NAME, schedule.Name);
                        xWriter.WriteElementString(SCHEDULE_MODE_ONETIME, schedule.ModeOneTime.ToString());
                        xWriter.WriteElementString(SCHEDULE_MODE_PERIOD, schedule.ModePeriod.ToString());
                        xWriter.WriteElementString(SCHEDULE_CAPTUREAT, schedule.CaptureAt.ToString());
                        xWriter.WriteElementString(SCHEDULE_STARTAT, schedule.StartAt.ToString());
                        xWriter.WriteElementString(SCHEDULE_STOPAT, schedule.StopAt.ToString());
                        xWriter.WriteElementString(SCHEDULE_MONDAY, schedule.Monday.ToString());
                        xWriter.WriteElementString(SCHEDULE_TUESDAY, schedule.Tuesday.ToString());
                        xWriter.WriteElementString(SCHEDULE_WEDNESDAY, schedule.Wednesday.ToString());
                        xWriter.WriteElementString(SCHEDULE_THURSDAY, schedule.Thursday.ToString());
                        xWriter.WriteElementString(SCHEDULE_FRIDAY, schedule.Friday.ToString());
                        xWriter.WriteElementString(SCHEDULE_SATURDAY, schedule.Saturday.ToString());
                        xWriter.WriteElementString(SCHEDULE_SUNDAY, schedule.Sunday.ToString());

                        xWriter.WriteEndElement();
                    }

                    xWriter.WriteEndElement();
                    xWriter.WriteEndElement();
                    xWriter.WriteEndDocument();

                    xWriter.Flush();
                    xWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("ScheduleCollection::SaveToXmlFile", ex);
            }
        }
    }
}