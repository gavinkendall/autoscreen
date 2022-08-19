//-----------------------------------------------------------------------
// <copyright file="ScheduleCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A collection of schedules.</summary>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.
//-----------------------------------------------------------------------
using System;
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
        private const string SCHEDULE_ENABLE = "enable";
        private const string SCHEDULE_MODE_ONETIME = "mode_onetime";
        private const string SCHEDULE_MODE_PERIOD = "mode_period";
        private const string SCHEDULE_CAPTUREAT = "captureat";
        private const string SCHEDULE_STARTAT = "startat";
        private const string SCHEDULE_STOPAT = "stopat";
        private const string SCHEDULE_SCREEN_CAPTURE_INTERVAL = "screen_capture_interval";
        private const string SCHEDULE_MONDAY = "monday";
        private const string SCHEDULE_TUESDAY = "tuesday";
        private const string SCHEDULE_WEDNESDAY = "wednesday";
        private const string SCHEDULE_THURSDAY = "thursday";
        private const string SCHEDULE_FRIDAY = "friday";
        private const string SCHEDULE_SATURDAY = "saturday";
        private const string SCHEDULE_SUNDAY = "sunday";
        private const string SCHEDULE_NOTES = "notes";
        private const string SCHEDULE_LOGIC = "logic";
        private const string SCHEDULE_SCOPE = "scope";

        private readonly string SCHEDULE_XPATH;

        private string AppCodename { get; set; }
        private string AppVersion { get; set; }

        // Special Schedule (which was previously an actual Schedule but now it's internal)
        // These are used for the -startat, -stopat, and -captureat command line options.

        /// <summary>
        /// Determines if the Special Schedule is activated.
        /// </summary>
        public bool SpecialScheduleActivated { get; set; }

        /// <summary>
        /// Determines if we're doing a single capture.
        /// </summary>
        public bool SpecialScheduleModeOneTime { get; set; }

        /// <summary>
        /// Determines if we're doing a capture session during a period of time.
        /// </summary>
        public bool SpecialScheduleModePeriod { get; set; }

        /// <summary>
        /// The time when the Special Schedule should start taking screenshots as defined by the -startat command line option.
        /// </summary>
        public DateTime SpecialScheduleStartAt { get; set; }

        /// <summary>
        /// The time when the Special Schedule should stop taking screenshots as defined by the -stopat command line option.
        /// </summary>
        public DateTime SpecialScheduleStopAt { get; set; }

        /// <summary>
        /// The time when screenshots should be taken (only once) as defined by the -captureat command line option.
        /// </summary>
        public DateTime SpecialScheduleCaptureAt { get; set; }

        /// <summary>
        /// The interval at which the Special Schedule will run at when taking screenshots between the start time and the stop time.
        /// </summary>
        public int SpecialScheduleScreenCaptureInterval { get; set; }


        /// <summary>
        /// The empty constructor for the schedule collection.
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
        public bool LoadXmlFileAndAddSchedules(Config config, FileSystem fileSystem, Log log)
        {
            try
            {
                if (fileSystem.FileExists(fileSystem.SchedulesFile))
                {
                    log.WriteDebugMessage("Schedules file \"" + fileSystem.SchedulesFile + "\" found. Attempting to load XML document");

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(fileSystem.SchedulesFile);

                    log.WriteDebugMessage("XML document loaded");

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xSchedules = xDoc.SelectNodes(SCHEDULE_XPATH);

                    foreach (XmlNode xSchedule in xSchedules)
                    {
                        Schedule schedule = new Schedule();
                        XmlNodeReader xReader = new XmlNodeReader(xSchedule);

                        while (xReader.Read())
                        {
                            if (xReader.IsStartElement() && !xReader.IsEmptyElement)
                            {
                                switch (xReader.Name)
                                {
                                    case SCHEDULE_NAME:
                                        xReader.Read();
                                        schedule.Name = xReader.Value;
                                        break;

                                    case SCHEDULE_ENABLE:
                                    case "active": // Any version older than 2.4.0.0 used "active" instead of "enable".
                                        xReader.Read();
                                        schedule.Enable = Convert.ToBoolean(xReader.Value);
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

                                    case SCHEDULE_SCREEN_CAPTURE_INTERVAL:
                                        xReader.Read();
                                        schedule.ScreenCaptureInterval = Convert.ToInt32(xReader.Value);
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

                                    case SCHEDULE_NOTES:
                                        xReader.Read();
                                        schedule.Notes = xReader.Value;
                                        break;

                                    case SCHEDULE_LOGIC:
                                        xReader.Read();
                                        schedule.Logic = Convert.ToInt32(xReader.Value);
                                        break;

                                    case SCHEDULE_SCOPE:
                                        xReader.Read();
                                        schedule.Scope = xReader.Value;

                                        // Set the scope for the schedule to be "All Screens and Regions" if the value is empty.
                                        if (string.IsNullOrEmpty(schedule.Scope))
                                        {
                                            schedule.Scope = "All Screens and Regions";
                                        }
                                        break;
                                }
                            }
                        }

                        xReader.Close();

                        // Change the data for each Schedule that's being loaded if we've detected that
                        // the XML document is from an older version of the application.
                        if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                        {
                            log.WriteDebugMessage("An old version of the schedules.xml file was detected. Attempting upgrade to new schema.");

                            Version v2300 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, Settings.CODEVERSION_BOOMBAYAH);
                            Version v2319 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, "2.3.1.9");
                            Version configVersion = config.Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                            {
                                log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                // This is a new property for Schedule that was introduced in 2.3.0.0
                                // so any version before 2.3.0.0 needs to have it during an upgrade.
                                schedule.Enable = true;
                            }

                            if (v2319 != null && configVersion != null && configVersion.VersionNumber < v2319.VersionNumber)
                            {
                                log.WriteDebugMessage("Boombayah 2.3.1.8 or older detected");

                                // A new property for Schedule introduced in 2.3.1.9
                                schedule.ScreenCaptureInterval = Convert.ToInt32(config.Settings.User.GetByKey("ScreenCaptureInterval").Value);
                            }

                            // 2.4 removes "Special Schedule" that was used in previous versions because 2.4 uses an internal special schedule going forward.
                            if (schedule.Name.Equals("Special Schedule"))
                            {
                                schedule.Name = string.Empty;
                            }
                        }

                        if (!string.IsNullOrEmpty(schedule.Name))
                        {
                            Add(schedule);
                        }
                    }

                    if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                    {
                        log.WriteDebugMessage("Schedules file detected as an old version");
                        SaveToXmlFile(config.Settings, fileSystem, log);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                if (fileSystem.FileExists(fileSystem.SchedulesFile))
                {
                    fileSystem.DeleteFile(fileSystem.SchedulesFile);

                    log.WriteErrorMessage("The file \"" + fileSystem.SchedulesFile + "\" had to be deleted because an error was encountered. You may need to force quit the application and run it again.");
                }

                log.WriteExceptionMessage("ScheduleCollection::LoadXmlFileAndAddSchedules", ex);

                return false;
            }
        }

        /// <summary>
        /// Saves the image schedules in the collection to the schedules.xml file.
        /// </summary>
        public bool SaveToXmlFile(Settings settings, FileSystem fileSystem, Log log)
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

                if (fileSystem.FileExists(fileSystem.SchedulesFile))
                {
                    fileSystem.DeleteFile(fileSystem.SchedulesFile);
                }

                using (XmlWriter xWriter = XmlWriter.Create(fileSystem.SchedulesFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_SCHEDULES_NODE);

                    foreach (Schedule schedule in base.Collection)
                    {
                        xWriter.WriteStartElement(XML_FILE_SCHEDULE_NODE);

                        xWriter.WriteElementString(SCHEDULE_ENABLE, schedule.Enable.ToString());
                        xWriter.WriteElementString(SCHEDULE_NAME, schedule.Name);
                        xWriter.WriteElementString(SCHEDULE_MODE_ONETIME, schedule.ModeOneTime.ToString());
                        xWriter.WriteElementString(SCHEDULE_MODE_PERIOD, schedule.ModePeriod.ToString());
                        xWriter.WriteElementString(SCHEDULE_CAPTUREAT, schedule.CaptureAt.ToString());
                        xWriter.WriteElementString(SCHEDULE_STARTAT, schedule.StartAt.ToString());
                        xWriter.WriteElementString(SCHEDULE_STOPAT, schedule.StopAt.ToString());
                        xWriter.WriteElementString(SCHEDULE_SCREEN_CAPTURE_INTERVAL, schedule.ScreenCaptureInterval.ToString());
                        xWriter.WriteElementString(SCHEDULE_MONDAY, schedule.Monday.ToString());
                        xWriter.WriteElementString(SCHEDULE_TUESDAY, schedule.Tuesday.ToString());
                        xWriter.WriteElementString(SCHEDULE_WEDNESDAY, schedule.Wednesday.ToString());
                        xWriter.WriteElementString(SCHEDULE_THURSDAY, schedule.Thursday.ToString());
                        xWriter.WriteElementString(SCHEDULE_FRIDAY, schedule.Friday.ToString());
                        xWriter.WriteElementString(SCHEDULE_SATURDAY, schedule.Saturday.ToString());
                        xWriter.WriteElementString(SCHEDULE_SUNDAY, schedule.Sunday.ToString());
                        xWriter.WriteElementString(SCHEDULE_NOTES, schedule.Notes);
                        xWriter.WriteElementString(SCHEDULE_LOGIC, schedule.Logic.ToString());

                        // Default to "All Screens and Regions" if scope is empty.
                        if (string.IsNullOrEmpty(schedule.Scope))
                        {
                            schedule.Scope = "All Screens and Regions";
                        }

                        xWriter.WriteElementString(SCHEDULE_SCOPE, schedule.Scope);

                        xWriter.WriteEndElement();
                    }

                    xWriter.WriteEndElement();
                    xWriter.WriteEndElement();
                    xWriter.WriteEndDocument();

                    xWriter.Flush();
                    xWriter.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                log.WriteExceptionMessage("ScheduleCollection::SaveToXmlFile", ex);

                return false;
            }
        }
    }
}