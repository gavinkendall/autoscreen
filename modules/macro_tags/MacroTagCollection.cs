//-----------------------------------------------------------------------
// <copyright file="MacroTagCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A collection of macro tags.</summary>
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
    /// A collection class to store and manage Tag objects.
    /// </summary>
    public class MacroTagCollection : CollectionTemplate<MacroTag>
    {
        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_MACRO_TAG_NODE = "macrotag";
        private const string XML_FILE_MACRO_TAGS_NODE = "macrotags";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string MACRO_TAG_NAME = "name";
        private const string MACRO_TAG_DESCRIPTION = "description";
        private const string MACRO_TAG_NOTES = "notes";
        private const string MACRO_TAG_TYPE = "type";

        private const string MACRO_TAG_DATETIME_FORMAT_VALUE = "datetime_format_value";

        private const string MACRO_TAG_TIMERANGE_MACRO1_START = "timerange_macro1_start";
        private const string MACRO_TAG_TIMERANGE_MACRO1_END = "timerange_macro1_end";

        private const string MACRO_TAG_TIMERANGE_MACRO2_START = "timerange_macro2_start";
        private const string MACRO_TAG_TIMERANGE_MACRO2_END = "timerange_macro2_end";

        private const string MACRO_TAG_TIMERANGE_MACRO3_START = "timerange_macro3_start";
        private const string MACRO_TAG_TIMERANGE_MACRO3_END = "timerange_macro3_end";

        private const string MACRO_TAG_TIMERANGE_MACRO4_START = "timerange_macro4_start";
        private const string MACRO_TAG_TIMERANGE_MACRO4_END = "timerange_macro4_end";

        private const string MACRO_TAG_TIMERANGE_MACRO1_MACRO = "timerange_macro1_macro";
        private const string MACRO_TAG_TIMERANGE_MACRO2_MACRO = "timerange_macro2_macro";
        private const string MACRO_TAG_TIMERANGE_MACRO3_MACRO = "timerange_macro3_macro";
        private const string MACRO_TAG_TIMERANGE_MACRO4_MACRO = "timerange_macro4_macro";

        // These are old "Time of Day" fields from versions older than 2.3.2.6
        private const string MACRO_TAG_TIME_OF_DAY_MORNING_START = "time_of_day_morning_start";
        private const string MACRO_TAG_TIME_OF_DAY_MORNING_END = "time_of_day_morning_end";
        private const string MACRO_TAG_TIME_OF_DAY_AFTERNOON_START = "time_of_day_afternoon_start";
        private const string MACRO_TAG_TIME_OF_DAY_AFTERNOON_END = "time_of_day_afternoon_end";
        private const string MACRO_TAG_TIME_OF_DAY_EVENING_START = "time_of_day_evening_start";
        private const string MACRO_TAG_TIME_OF_DAY_EVENING_END = "time_of_day_evening_end";
        private const string MACRO_TAG_TIME_OF_DAY_MORNING_VALUE = "time_of_day_morning_value";
        private const string MACRO_TAG_TIME_OF_DAY_AFTERNOON_VALUE = "time_of_day_afternoon_value";
        private const string MACRO_TAG_TIME_OF_DAY_EVENING_VALUE = "time_of_day_evening_value";
        private const string MACRO_TAG_TIME_OF_DAY_EVENING_EXTENDS_TO_NEXT_MORNING = "evening_extends_to_next_morning";

        private const string MACRO_TAG_ENABLE = "enable";

        private readonly string MACRO_TAG_XPATH;

        private string AppCodename { get; set; }
        private string AppVersion { get; set; }

        /// <summary>
        /// Empty constructor for the tag collection.
        /// </summary>
        public MacroTagCollection()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/");
            sb.Append(XML_FILE_ROOT_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_MACRO_TAGS_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_MACRO_TAG_NODE);

            MACRO_TAG_XPATH = sb.ToString();
        }

        /// <summary>
        /// Adds a tag to the tag collection.
        /// </summary>
        /// <param name="tag">The tag to add to the tag collection.</param>
        public override void Add(MacroTag tag)
        {
            if (string.IsNullOrEmpty(tag.Name)) return;

            if (!tag.Name.StartsWith("%"))
                tag.Name = "%" + tag.Name;

            if (!tag.Name.EndsWith("%"))
                tag.Name += "%";

            base.Add(tag);
        }

        /// <summary>
        /// Loads the macro tags.
        /// </summary>
        public bool LoadXmlFileAndAddTags(Config config, MacroParser macroParser, FileSystem fileSystem, Log log)
        {
            try
            {
                if (fileSystem.FileExists(fileSystem.MacroTagsFile))
                {
                    log.WriteDebugMessage("Macro Tags file \"" + fileSystem.MacroTagsFile + "\" found. Attempting to load XML document");

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(fileSystem.MacroTagsFile);

                    log.WriteDebugMessage("XML document loaded");

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xTags = xDoc.SelectNodes(MACRO_TAG_XPATH);

                    // This is to maintain backwards compatibility with versions that are older than 2.4
                    if (xTags.Count == 0)
                    {
                        xTags = xDoc.SelectNodes(@"/autoscreen/tags/tag");
                    }

                    bool eveningExtendsToNextMorning = false;

                    foreach (XmlNode xTag in xTags)
                    {
                        MacroTag tag = new MacroTag(macroParser);
                        XmlNodeReader xReader = new XmlNodeReader(xTag);

                        while (xReader.Read())
                        {
                            if (xReader.IsStartElement() && !xReader.IsEmptyElement)
                            {
                                switch (xReader.Name)
                                {
                                    case MACRO_TAG_NAME:
                                        xReader.Read();
                                        tag.Name = xReader.Value;

                                        if (!tag.Name.StartsWith("%"))
                                            tag.Name = "%" + tag.Name;

                                        if (!tag.Name.EndsWith("%"))
                                            tag.Name += "%";

                                        break;

                                    case MACRO_TAG_DESCRIPTION:
                                        xReader.Read();
                                        tag.Description = xReader.Value;
                                        break;

                                    case MACRO_TAG_NOTES:
                                        xReader.Read();
                                        tag.Notes = xReader.Value;
                                        break;

                                    case MACRO_TAG_TYPE:
                                        xReader.Read();

                                        string value = xReader.Value;

                                        // Change the data for each Macro Tag that's being loaded if we've detected that
                                        // the XML document is from an older version of the application.
                                        if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                                        {
                                            log.WriteDebugMessage("An old version of the macrotags.xml file was detected. Attempting upgrade to new schema.");

                                            Version v2300 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, Settings.CODEVERSION_BOOMBAYAH);
                                            Version v2326 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, "2.3.2.6");
                                            Version configVersion = config.Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                                            {
                                                log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                                // Starting with 2.3.0.0 the DateTimeFormatFunction type became the DateTimeFormatExpression type.
                                                value = value.Replace("DateTimeFormatFunction", "DateTimeFormatExpression");
                                            }

                                            if (v2326 != null && configVersion != null && configVersion.VersionNumber < v2326.VersionNumber)
                                            {
                                                log.WriteDebugMessage("Boombayah 2.3.2.5 or older detected");

                                                // Starting with 2.3.2.6 the TimeOfDay type became the TimeRange type.
                                                value = value.Replace("TimeOfDay", "TimeRange");
                                            }
                                        }

                                        tag.Type = (MacroTagType)Enum.Parse(typeof(MacroTagType), value);
                                        break;

                                    case MACRO_TAG_DATETIME_FORMAT_VALUE:
                                        xReader.Read();
                                        tag.DateTimeFormatValue = xReader.Value;
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO1_START:
                                    case MACRO_TAG_TIME_OF_DAY_MORNING_START:
                                        xReader.Read();
                                        tag.TimeRangeMacro1Start = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO1_END:
                                    case MACRO_TAG_TIME_OF_DAY_MORNING_END:
                                        xReader.Read();
                                        tag.TimeRangeMacro1End = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO2_START:
                                    case MACRO_TAG_TIME_OF_DAY_AFTERNOON_START:
                                        xReader.Read();
                                        tag.TimeRangeMacro2Start = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO2_END:
                                    case MACRO_TAG_TIME_OF_DAY_AFTERNOON_END:
                                        xReader.Read();
                                        tag.TimeRangeMacro2End = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO3_START:
                                    case MACRO_TAG_TIME_OF_DAY_EVENING_START:
                                        xReader.Read();
                                        tag.TimeRangeMacro3Start = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO3_END:
                                    case MACRO_TAG_TIME_OF_DAY_EVENING_END:
                                        xReader.Read();
                                        tag.TimeRangeMacro3End = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO4_START:
                                        xReader.Read();
                                        tag.TimeRangeMacro4Start = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO4_END:
                                        xReader.Read();
                                        tag.TimeRangeMacro4End = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO1_MACRO:
                                    case MACRO_TAG_TIME_OF_DAY_MORNING_VALUE:
                                        xReader.Read();
                                        tag.TimeRangeMacro1Macro = xReader.Value;
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO2_MACRO:
                                    case MACRO_TAG_TIME_OF_DAY_AFTERNOON_VALUE:
                                        xReader.Read();
                                        tag.TimeRangeMacro2Macro = xReader.Value;
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO3_MACRO:
                                    case MACRO_TAG_TIME_OF_DAY_EVENING_VALUE:
                                        xReader.Read();
                                        tag.TimeRangeMacro3Macro = xReader.Value;
                                        break;

                                    case MACRO_TAG_TIMERANGE_MACRO4_MACRO:
                                        xReader.Read();
                                        tag.TimeRangeMacro4Macro = xReader.Value;
                                        break;

                                    case MACRO_TAG_TIME_OF_DAY_EVENING_EXTENDS_TO_NEXT_MORNING:
                                        xReader.Read();
                                        eveningExtendsToNextMorning = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case MACRO_TAG_ENABLE:
                                    case "active": // Any version older than 2.4.0.0 used "active" instead of "enable".
                                        xReader.Read();
                                        tag.Enable = Convert.ToBoolean(xReader.Value);
                                        break;
                                }
                            }
                        }

                        xReader.Close();

                        // Change the data for each Macro Tag that's being loaded if we've detected that
                        // the XML document is from an older version of the application.
                        if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                        {
                            log.WriteDebugMessage("An old version of the macrotags.xml file was detected. Attempting upgrade to new schema.");

                            Version v2300 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, Settings.CODEVERSION_BOOMBAYAH);
                            Version v2326 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, "2.3.2.6");
                            Version configVersion = config.Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                            {
                                log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                // This is a new property for Macro Tag that was introduced in 2.3.0.0
                                // so any version before 2.3.0.0 needs to have it during an upgrade.
                                tag.Enable = true;

                                // "Description" is a new property for Macro Tag that was introduced in 2.3.0.0
                                switch (tag.Type)
                                {
                                    case MacroTagType.ActiveWindowTitle:
                                        tag.Description = "The title of the active window";
                                        break;

                                    case MacroTagType.DateTimeFormat:
                                        tag.Description = "A value representing either a date, a time, or a combination of the date and time";
                                        break;

                                    case MacroTagType.ImageFormat:
                                        tag.Description = "The image format of the screenshot (such as jpeg or png)";
                                        break;

                                    case MacroTagType.ScreenCaptureCycleCount:
                                        tag.Description = "The number of capture cycles during a screen capture session";
                                        break;

                                    case MacroTagType.ScreenName:
                                        tag.Description = "The name of the screen or region";
                                        break;

                                    case MacroTagType.ScreenNumber:
                                        tag.Description = "The screen number. For example, the first display is screen number 1";
                                        break;

                                    case MacroTagType.User:
                                        tag.Description = "The name of the user";
                                        break;

                                    case MacroTagType.Machine:
                                        tag.Description = "The name of the computer";
                                        break;

                                    case MacroTagType.TimeRange:
                                        tag.Description = "The macro to use for a specific time range";
                                        break;

                                    case MacroTagType.DateTimeFormatExpression:
                                        tag.Description = "An expression which represents a time that is either ahead or behind the current time";
                                        break;
                                }
                            }

                            if (v2326 != null && configVersion != null && configVersion.VersionNumber < v2326.VersionNumber)
                            {
                                tag.TimeRangeMacro4Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                                tag.TimeRangeMacro4End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                                tag.TimeRangeMacro4Macro = string.Empty;

                                // This is an old property from before 2.3.2.6 when TimeOfDay macro tags were used.
                                // Since 2.3.2.6 we now use TimeRange tags so we need to split up the "evening" start and end times
                                // into their own "Macro 4" start and end times when this property is set to true.
                                if (eveningExtendsToNextMorning)
                                {
                                    tag.TimeRangeMacro4Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                                    tag.TimeRangeMacro4End = tag.TimeRangeMacro3End;

                                    tag.TimeRangeMacro3End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                                    tag.TimeRangeMacro4Macro = tag.TimeRangeMacro3Macro;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(tag.Name))
                        {
                            Add(tag);
                        }
                    }

                    if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                    {
                        log.WriteDebugMessage("Macro Tags file detected as an old version");

                        // Go through the macro tags we want to add. If we can't find them then add them to the collection.

                        if (GetByName("%timerange%") == null)
                        {
                            Add(new MacroTag(macroParser, "timerange", "The macro to use during a specific time range. At the moment it is %timerange%", MacroTagType.TimeRange, enable: true));
                        }

                        if (GetByName("%quarteryear%") == null)
                        {
                            Add(new MacroTag(macroParser, "quarteryear", "A number representing the current quarter of the current year (%quarteryear%)", MacroTagType.QuarterYear, enable: true));
                        }

                        if (GetByName("%x%") == null)
                        {
                            Add(new MacroTag(macroParser, "x", "The X value of the screen or region", MacroTagType.X, enable: true));
                        }

                        if (GetByName("%y%") == null)
                        {
                            Add(new MacroTag(macroParser, "y", "The Y value of the screen or region", MacroTagType.Y, enable: true));
                        }

                        if (GetByName("%width%") == null)
                        {
                            Add(new MacroTag(macroParser, "width", "The Width value of the screen or region", MacroTagType.Width, enable: true));
                        }

                        if (GetByName("%height%") == null)
                        {
                            Add(new MacroTag(macroParser, "height", "The Height value of the screen or region", MacroTagType.Height, enable: true));
                        }

                        if (GetByName("%process%") == null)
                        {
                            Add(new MacroTag(macroParser, "process", "The name of the active process", MacroTagType.Process, enable: true));
                        }

                        if (GetByName("%label%") == null)
                        {
                            Add(new MacroTag(macroParser, "label", "The label being applied to the saved screenshot", MacroTagType.Label, enable: true));
                        }

                        if (GetByName("%capturenowcount%") == null)
                        {
                            Add(new MacroTag(macroParser, "capturenowcount", "The number of times Capture Now has been used", MacroTagType.CountNow, enable: true));
                        }

                        SaveToXmlFile(config, fileSystem, log);
                    }
                }
                else
                {
                    log.WriteDebugMessage("WARNING: Unable to load macro tags");

                    // Setup a few "built in" tags by default.
                    Add(new MacroTag(macroParser, "name", "The name of the screen or region", MacroTagType.ScreenName, enable: true));
                    Add(new MacroTag(macroParser, "screen", "The screen number. For example, the first display is screen 1 and the second display is screen 2", MacroTagType.ScreenNumber, enable: true));
                    Add(new MacroTag(macroParser, "format", "The image format such as jpeg or png", MacroTagType.ImageFormat, enable: true));
                    Add(new MacroTag(macroParser, "date", "The current date", MacroTagType.DateTimeFormat, macroParser.DateFormat, active: true));
                    Add(new MacroTag(macroParser, "time", "The current time", MacroTagType.DateTimeFormat, macroParser.TimeFormatForWindows, active: true));
                    Add(new MacroTag(macroParser, "year", "The current year", MacroTagType.DateTimeFormat, macroParser.YearFormat, active: true));
                    Add(new MacroTag(macroParser, "month", "The current month", MacroTagType.DateTimeFormat, macroParser.MonthFormat, active: true));
                    Add(new MacroTag(macroParser, "day", "The current day", MacroTagType.DateTimeFormat, macroParser.DayFormat, active: true));
                    Add(new MacroTag(macroParser, "hour", "The current hour", MacroTagType.DateTimeFormat, macroParser.HourFormat, active: true));
                    Add(new MacroTag(macroParser, "minute", "The current minute", MacroTagType.DateTimeFormat, macroParser.MinuteFormat, active: true));
                    Add(new MacroTag(macroParser, "second", "The current second", MacroTagType.DateTimeFormat, macroParser.SecondFormat, active: true));
                    Add(new MacroTag(macroParser, "millisecond", "The current millisecond", MacroTagType.DateTimeFormat, macroParser.MillisecondFormat, active: true));
                    Add(new MacroTag(macroParser, "lastyear", "The previous year", MacroTagType.DateTimeFormatExpression, "{year-1}", active: true));
                    Add(new MacroTag(macroParser, "lastmonth", "The previous month", MacroTagType.DateTimeFormatExpression, "{month-1}", active: true));
                    Add(new MacroTag(macroParser, "yesterday", "The previous day", MacroTagType.DateTimeFormatExpression, "{day-1}[yyyy-MM-dd]", active: true));
                    Add(new MacroTag(macroParser, "tomorrow", "The next day", MacroTagType.DateTimeFormatExpression, "{day+1}[yyyy-MM-dd]", active: true));
                    Add(new MacroTag(macroParser, "6hoursbehind", "Six hours behind the current hour", MacroTagType.DateTimeFormatExpression, "{hour-6}[yyyy-MM-dd_HH-mm-ss.fff]", active: true));
                    Add(new MacroTag(macroParser, "6hoursahead", "Six hours ahead the current hour", MacroTagType.DateTimeFormatExpression, "{hour+6}[yyyy-MM-dd_HH-mm-ss.fff]", active: true));
                    Add(new MacroTag(macroParser, "count", "The number of capture cycles during a running screen capture session.", MacroTagType.ScreenCaptureCycleCount, enable: true));
                    Add(new MacroTag(macroParser, "user", "The user using this computer", MacroTagType.User, enable: true));
                    Add(new MacroTag(macroParser, "machine", "The name of the computer", MacroTagType.Machine, enable: true));
                    Add(new MacroTag(macroParser, "title", "The title of the active window", MacroTagType.ActiveWindowTitle, enable: true));
                    Add(new MacroTag(macroParser, "timerange", "The macro to use during a specific time range", MacroTagType.TimeRange, enable: true));
                    Add(new MacroTag(macroParser, "quarteryear", "A number representing the current quarter of the current year", MacroTagType.QuarterYear, enable: true));
                    Add(new MacroTag(macroParser, "x", "The X value of the screen or region", MacroTagType.X, enable: true));
                    Add(new MacroTag(macroParser, "y", "The Y value of the screen or region", MacroTagType.Y, enable: true));
                    Add(new MacroTag(macroParser, "width", "The Width value of the screen or region", MacroTagType.Width, enable: true));
                    Add(new MacroTag(macroParser, "height", "The Height value of the screen or region", MacroTagType.Height, enable: true));
                    Add(new MacroTag(macroParser, "process", "The name of the active process", MacroTagType.Process, enable: true));
                    Add(new MacroTag(macroParser, "label", "The label being applied to the saved screenshot", MacroTagType.Label, enable: true));
                    Add(new MacroTag(macroParser, "capturenowcount", "The number of times Capture Now has been used", MacroTagType.CountNow, enable: true));

                    SaveToXmlFile(config, fileSystem, log);
                }

                return true;
            }
            catch (Exception ex)
            {
                if (fileSystem.FileExists(fileSystem.MacroTagsFile))
                {
                    fileSystem.DeleteFile(fileSystem.MacroTagsFile);

                    log.WriteErrorMessage("The file \"" + fileSystem.MacroTagsFile + "\" had to be deleted because an error was encountered. You may need to force quit the application and run it again.");
                }

                log.WriteExceptionMessage("MacroTagCollection::LoadXmlFileAndAddTags", ex);

                return false;
            }
        }

        /// <summary>
        /// Saves the tags.
        /// </summary>
        public bool SaveToXmlFile(Config config, FileSystem fileSystem, Log log)
        {
            try
            {
                XmlWriterSettings xSettings = new XmlWriterSettings();
                xSettings.Indent = true;
                xSettings.CloseOutput = true;
                xSettings.CheckCharacters = true;
                xSettings.Encoding = Encoding.UTF8;
                xSettings.NewLineChars = Environment.NewLine;
                xSettings.IndentChars = XML_FILE_INDENT_CHARS;
                xSettings.NewLineHandling = NewLineHandling.Entitize;
                xSettings.ConformanceLevel = ConformanceLevel.Document;

                if (string.IsNullOrEmpty(fileSystem.MacroTagsFile))
                {
                    fileSystem.MacroTagsFile = fileSystem.DefaultMacroTagsFile;

                    if (fileSystem.FileExists(fileSystem.ConfigFile))
                    {
                        fileSystem.AppendToFile(fileSystem.ConfigFile, "\nMacroTagsFile=" + fileSystem.MacroTagsFile);
                    }
                }

                if (fileSystem.FileExists(fileSystem.MacroTagsFile))
                {
                    fileSystem.DeleteFile(fileSystem.MacroTagsFile);
                }

                using (XmlWriter xWriter = XmlWriter.Create(fileSystem.MacroTagsFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, config.Settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, config.Settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_MACRO_TAGS_NODE);

                    foreach (MacroTag tag in base.Collection)
                    {
                        xWriter.WriteStartElement(XML_FILE_MACRO_TAG_NODE);

                        xWriter.WriteElementString(MACRO_TAG_ENABLE, tag.Enable.ToString());
                        xWriter.WriteElementString(MACRO_TAG_NAME, tag.Name);
                        xWriter.WriteElementString(MACRO_TAG_DESCRIPTION, tag.Description);
                        xWriter.WriteElementString(MACRO_TAG_NOTES, tag.Notes);
                        xWriter.WriteElementString(MACRO_TAG_TYPE, tag.Type.ToString());
                        xWriter.WriteElementString(MACRO_TAG_DATETIME_FORMAT_VALUE, tag.DateTimeFormatValue);
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO1_START, tag.TimeRangeMacro1Start.ToString());
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO1_END, tag.TimeRangeMacro1End.ToString());
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO2_START, tag.TimeRangeMacro2Start.ToString());
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO2_END, tag.TimeRangeMacro2End.ToString());
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO3_START, tag.TimeRangeMacro3Start.ToString());
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO3_END, tag.TimeRangeMacro3End.ToString());
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO4_START, tag.TimeRangeMacro4Start.ToString());
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO4_END, tag.TimeRangeMacro4End.ToString());
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO1_MACRO, tag.TimeRangeMacro1Macro);
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO2_MACRO, tag.TimeRangeMacro2Macro);
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO3_MACRO, tag.TimeRangeMacro3Macro);
                        xWriter.WriteElementString(MACRO_TAG_TIMERANGE_MACRO4_MACRO, tag.TimeRangeMacro4Macro);

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
                log.WriteExceptionMessage("MacroTagCollection::SaveToXmlFile", ex);

                return false;
            }
        }
    }
}