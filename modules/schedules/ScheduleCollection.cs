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

        private readonly string SCHEDULE_XPATH;

        private string AppCodename { get; set; }
        private string AppVersion { get; set; }

        // Special Schedule (which was previously an actual Schedule but now it's internal)
        // These are used for the -startat, -stopat, and -captureat command line options.

        /// <summary>
        /// Determines if the Special Schedule is enabled.
        /// </summary>
        public bool SpecialScheduleEnabled { get; set; }

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
                                schedule.ScreenCaptureInterval = Convert.ToInt32(config.Settings.User.GetByKey("ScreenCaptureInterval", config.Settings.DefaultSettings.ScreenCaptureInterval).Value);
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
                else
                {
                    log.WriteDebugMessage("WARNING: Unable to load schedules");

                    DateTime dtNow = DateTime.Now;

                    if (config.Settings.VersionManager != null && config.Settings.VersionManager.OldUserSettings != null)
                    {
                        string oldAppCodename = config.Settings.VersionManager.OldUserSettings.AppCodename;
                        string oldAppVersion = config.Settings.VersionManager.OldUserSettings.AppVersion;

                        if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, oldAppCodename, oldAppVersion))
                        {
                            Version configVersion = config.Settings.VersionManager.Versions.Get(oldAppCodename, oldAppVersion);
                            Version v2300 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, Settings.CODEVERSION_BOOMBAYAH);

                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                            {
                                log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                // If we're importing the schedule settings from a previous version of Auto Screen Capture we'll need to create a "Schedule 1" and enable it.
                                SettingCollection oldUserSettings = config.Settings.VersionManager.OldUserSettings;

                                Schedule schedule1 = new Schedule()
                                {
                                    Name = "Schedule 1",
                                    Enable = false,
                                    ModeOneTime = true,
                                    ModePeriod = false,
                                    CaptureAt = dtNow,
                                    StartAt = dtNow,
                                    StopAt = dtNow,
                                    ScreenCaptureInterval = Convert.ToInt32(oldUserSettings.GetByKey("IntScreenCaptureInterval", config.Settings.DefaultSettings.ScreenCaptureInterval).Value),
                                    Notes = "This schedule was imported from an old version of Auto Screen Capture."
                                };

                                bool captureStartAt = Convert.ToBoolean(oldUserSettings.GetByKey("BoolCaptureStartAt", config.Settings.DefaultSettings.BoolCaptureStartAt).Value);
                                bool captureStopAt = Convert.ToBoolean(oldUserSettings.GetByKey("BoolCaptureStopAt", config.Settings.DefaultSettings.BoolCaptureStopAt).Value);

                                DateTime dtStartAt = Convert.ToDateTime(oldUserSettings.GetByKey("DateTimeCaptureStartAt", config.Settings.DefaultSettings.DateTimeCaptureStartAt).Value);
                                DateTime dtStopAt = Convert.ToDateTime(oldUserSettings.GetByKey("DateTimeCaptureStopAt", config.Settings.DefaultSettings.DateTimeCaptureStopAt).Value);

                                // Days
                                bool captureOnTheseDays = Convert.ToBoolean(oldUserSettings.GetByKey("BoolCaptureOnTheseDays", config.Settings.DefaultSettings.BoolCaptureOnTheseDays).Value);
                                bool sunday = Convert.ToBoolean(oldUserSettings.GetByKey("BoolCaptureOnSunday", config.Settings.DefaultSettings.BoolCaptureOnSunday).Value);
                                bool monday = Convert.ToBoolean(oldUserSettings.GetByKey("BoolCaptureOnMonday", config.Settings.DefaultSettings.BoolCaptureOnMonday).Value);
                                bool tuesday = Convert.ToBoolean(oldUserSettings.GetByKey("BoolCaptureOnTuesday", config.Settings.DefaultSettings.BoolCaptureOnTuesday).Value);
                                bool wednesday = Convert.ToBoolean(oldUserSettings.GetByKey("BoolCaptureOnWednesday", config.Settings.DefaultSettings.BoolCaptureOnWednesday).Value);
                                bool thursday = Convert.ToBoolean(oldUserSettings.GetByKey("BoolCaptureOnThursday", config.Settings.DefaultSettings.BoolCaptureOnThursday).Value);
                                bool friday = Convert.ToBoolean(oldUserSettings.GetByKey("BoolCaptureOnFriday", config.Settings.DefaultSettings.BoolCaptureOnFriday).Value);
                                bool saturday = Convert.ToBoolean(oldUserSettings.GetByKey("BoolCaptureOnSaturday", config.Settings.DefaultSettings.BoolCaptureOnSaturday).Value);

                                schedule1.ModeOneTime = false;
                                schedule1.ModePeriod = true;

                                if (captureStartAt)
                                {
                                    schedule1.Enable = true;
                                    schedule1.StartAt = dtStartAt;
                                }

                                if (captureStopAt)
                                {
                                    schedule1.Enable = true;
                                    schedule1.StopAt = dtStopAt;
                                }

                                if (captureOnTheseDays)
                                {
                                    schedule1.Sunday = sunday;
                                    schedule1.Monday = monday;
                                    schedule1.Tuesday = tuesday;
                                    schedule1.Wednesday = wednesday;
                                    schedule1.Thursday = thursday;
                                    schedule1.Friday = friday;
                                    schedule1.Saturday = saturday;
                                }

                                Add(schedule1);
                            }
                        }
                    }

                    Schedule workScheduleMorning = new Schedule()
                    {
                        Name = "Work Schedule - Morning",
                        Enable = false,
                        ModeOneTime = false,
                        ModePeriod = true,
                        CaptureAt = dtNow,
                        StartAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0),
                        StopAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0),
                        ScreenCaptureInterval = config.Settings.DefaultSettings.ScreenCaptureInterval,
                        Notes = "This schedule was created by Auto Screen Capture and is disabled by default. It takes screenshots every weekday between 9am and 12pm using a one minute interval.",
                        Monday = true,
                        Tuesday = true,
                        Wednesday = true,
                        Thursday = true,
                        Friday = true
                    };

                    Schedule workScheduleAfternoon = new Schedule()
                    {
                        Name = "Work Schedule - Afternoon",
                        Enable = false,
                        ModeOneTime = false,
                        ModePeriod = true,
                        CaptureAt = dtNow,
                        StartAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0),
                        StopAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0),
                        ScreenCaptureInterval = config.Settings.DefaultSettings.ScreenCaptureInterval,
                        Notes = "This schedule was created by Auto Screen Capture and is disabled by default. It takes screenshots every weekday between 1pm and 5pm using a one minute interval.",
                        Monday = true,
                        Tuesday = true,
                        Wednesday = true,
                        Thursday = true,
                        Friday = true
                    };

                    Add(workScheduleMorning);
                    Add(workScheduleAfternoon);

                    SaveToXmlFile(config.Settings, fileSystem, log);
                }

                return true;
            }
            catch (Exception ex)
            {
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

                if (string.IsNullOrEmpty(fileSystem.SchedulesFile))
                {
                    fileSystem.SchedulesFile = fileSystem.DefaultSchedulesFile;

                    fileSystem.AppendToFile(fileSystem.ConfigFile, "\nSchedulesFile=" + fileSystem.SchedulesFile);
                }

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