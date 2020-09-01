//-----------------------------------------------------------------------
// <copyright file="TagCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
    public class TagCollection : CollectionTemplate<Tag>
    {
        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_TAG_NODE = "tag";
        private const string XML_FILE_TAGS_NODE = "tags";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string TAG_NAME = "name";
        private const string TAG_DESCRIPTION = "description";
        private const string TAG_NOTES = "notes";
        private const string TAG_TYPE = "type";

        private const string TAG_DATETIME_FORMAT_VALUE = "datetime_format_value";

        private const string TAG_TIME_OF_DAY_MORNING_START = "time_of_day_morning_start";
        private const string TAG_TIME_OF_DAY_MORNING_END = "time_of_day_morning_end";

        private const string TAG_TIME_OF_DAY_AFTERNOON_START = "time_of_day_afternoon_start";
        private const string TAG_TIME_OF_DAY_AFTERNOON_END = "time_of_day_afternoon_end";

        private const string TAG_TIME_OF_DAY_EVENING_START = "time_of_day_evening_start";
        private const string TAG_TIME_OF_DAY_EVENING_END = "time_of_day_evening_end";

        private const string TAG_TIME_OF_DAY_MORNING_VALUE = "time_of_day_morning_value";
        private const string TAG_TIME_OF_DAY_AFTERNOON_VALUE = "time_of_day_afternoon_value";
        private const string TAG_TIME_OF_DAY_EVENING_VALUE = "time_of_day_evening_value";

        private const string TAG_TIME_OF_DAY_EVENING_EXTENDS_TO_NEXT_MORNING = "evening_extends_to_next_morning";

        private const string TAG_ACTIVE = "active";

        private readonly string TAG_XPATH;

        private static string AppCodename { get; set; }
        private static string AppVersion { get; set; }

        /// <summary>
        /// Empty constructor for the tag collection.
        /// </summary>
        public TagCollection()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/");
            sb.Append(XML_FILE_ROOT_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_TAGS_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_TAG_NODE);

            TAG_XPATH = sb.ToString();
        }

        /// <summary>
        /// Adds a tag to the tag collection.
        /// </summary>
        /// <param name="tag">The tag to add to the tag collection.</param>
        public override void Add(Tag tag)
        {
            if (string.IsNullOrEmpty(tag.Name)) return;

            if (!tag.Name.StartsWith("%"))
                tag.Name = "%" + tag.Name;

            if (!tag.Name.EndsWith("%"))
                tag.Name += "%";

            base.Add(tag);
        }

        /// <summary>
        /// Loads the tags.
        /// </summary>
        public bool LoadXmlFileAndAddTags()
        {
            try
            {
                if (FileSystem.FileExists(FileSystem.TagsFile))
                {
                    Log.WriteDebugMessage("Tags file \"" + FileSystem.TagsFile + "\" found. Attempting to load XML document");

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(FileSystem.TagsFile);

                    Log.WriteDebugMessage("XML document loaded");

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xTags = xDoc.SelectNodes(TAG_XPATH);

                    foreach (XmlNode xTag in xTags)
                    {
                        Tag tag = new Tag();
                        XmlNodeReader xReader = new XmlNodeReader(xTag);

                        while (xReader.Read())
                        {
                            if (xReader.IsStartElement() && !xReader.IsEmptyElement)
                            {
                                switch (xReader.Name)
                                {
                                    case TAG_NAME:
                                        xReader.Read();
                                        tag.Name = xReader.Value;

                                        if (!tag.Name.StartsWith("%"))
                                            tag.Name = "%" + tag.Name;

                                        if (!tag.Name.EndsWith("%"))
                                            tag.Name += "%";

                                        break;

                                    case TAG_DESCRIPTION:
                                        xReader.Read();
                                        tag.Description = xReader.Value;
                                        break;

                                    case TAG_NOTES:
                                        xReader.Read();
                                        tag.Notes = xReader.Value;
                                        break;

                                    case TAG_TYPE:
                                        xReader.Read();

                                        string value = xReader.Value;

                                        // Change the data for each Tag that's being loaded if we've detected that
                                        // the XML document is from an older version of the application.
                                        if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                                        {
                                            Log.WriteDebugMessage("An old version of the tags.xml file was detected. Attempting upgrade to new schema.");

                                            Version v2300 = Settings.VersionManager.Versions.Get("Boombayah", "2.3.0.0");
                                            Version configVersion = Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                                            {
                                                Log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                                // Starting with 2.3.0.0 the DateTimeFormatFunction type became the DateTimeFormatExpression type.
                                                value = value.Replace("DateTimeFormatFunction", "DateTimeFormatExpression");
                                            }
                                        }

                                        tag.Type = (TagType)Enum.Parse(typeof(TagType), value);
                                        break;

                                    case TAG_DATETIME_FORMAT_VALUE:
                                        xReader.Read();
                                        tag.DateTimeFormatValue = xReader.Value;
                                        break;

                                    case TAG_TIME_OF_DAY_MORNING_START:
                                        xReader.Read();
                                        tag.TimeOfDayMorningStart = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case TAG_TIME_OF_DAY_MORNING_END:
                                        xReader.Read();
                                        tag.TimeOfDayMorningEnd = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case TAG_TIME_OF_DAY_AFTERNOON_START:
                                        xReader.Read();
                                        tag.TimeOfDayAfternoonStart = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case TAG_TIME_OF_DAY_AFTERNOON_END:
                                        xReader.Read();
                                        tag.TimeOfDayAfternoonEnd = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case TAG_TIME_OF_DAY_EVENING_START:
                                        xReader.Read();
                                        tag.TimeOfDayEveningStart = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case TAG_TIME_OF_DAY_EVENING_END:
                                        xReader.Read();
                                        tag.TimeOfDayEveningEnd = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case TAG_TIME_OF_DAY_MORNING_VALUE:
                                        xReader.Read();
                                        tag.TimeOfDayMorningValue = xReader.Value;
                                        break;

                                    case TAG_TIME_OF_DAY_AFTERNOON_VALUE:
                                        xReader.Read();
                                        tag.TimeOfDayAfternoonValue = xReader.Value;
                                        break;

                                    case TAG_TIME_OF_DAY_EVENING_VALUE:
                                        xReader.Read();
                                        tag.TimeOfDayEveningValue = xReader.Value;
                                        break;

                                    case TAG_TIME_OF_DAY_EVENING_EXTENDS_TO_NEXT_MORNING:
                                        xReader.Read();
                                        tag.EveningExtendsToNextMorning = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case TAG_ACTIVE:
                                        xReader.Read();
                                        tag.Active = Convert.ToBoolean(xReader.Value);
                                        break;
                                }
                            }
                        }

                        xReader.Close();

                        // Change the data for each Tag that's being loaded if we've detected that
                        // the XML document is from an older version of the application.
                        if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                        {
                            Log.WriteDebugMessage("An old version of the tags.xml file was detected. Attempting upgrade to new schema.");

                            Version v2300 = Settings.VersionManager.Versions.Get("Boombayah", "2.3.0.0");
                            Version configVersion = Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                            {
                                Log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                // This is a new property for Tag that was introduced in 2.3.0.0
                                // so any version before 2.3.0.0 needs to have it during an upgrade.
                                tag.Active = true;

                                // "Description" is a new property for Tag that was introduced in 2.3.0.0
                                switch (tag.Type)
                                {
                                    case TagType.ActiveWindowTitle:
                                        tag.Description = "The title of the active window";
                                        break;

                                    case TagType.DateTimeFormat:
                                        tag.Description = "A value representing either a date, a time, or a combination of the date and time (" + tag.Name + ")";
                                        break;

                                    case TagType.ImageFormat:
                                        tag.Description = "The image format of the screenshot (such as jpeg or png)";
                                        break;

                                    case TagType.ScreenCaptureCycleCount:
                                        tag.Description = "The number of capture cycles during a screen capture session";
                                        break;

                                    case TagType.ScreenName:
                                        tag.Description = "The name of the screen or region";
                                        break;

                                    case TagType.ScreenNumber:
                                        tag.Description = "The screen number. For example, the first display is screen number 1";
                                        break;

                                    case TagType.User:
                                        tag.Description = "The name of the user (" + tag.Name + ")";
                                        break;

                                    case TagType.Machine:
                                        tag.Description = "The name of the computer (" + tag.Name + ")";
                                        break;

                                    case TagType.TimeOfDay:
                                        tag.Description = "The macro to use for a specific time of day";
                                        break;

                                    case TagType.DateTimeFormatExpression:
                                        tag.Description = "An expression which represents a time that is either ahead or behind the current time (" + tag.Name + ")";
                                        break;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(tag.Name))
                        {
                            Add(tag);
                        }
                    }

                    if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                    {
                        Log.WriteDebugMessage("Tags file detected as an old version");
                        SaveToXmlFile();
                    }
                }
                else
                {
                    Log.WriteDebugMessage("WARNING: Unable to load tags");

                    // Setup a few "built in" tags by default.
                    Add(new Tag("name", "The name of the screen or region", TagType.ScreenName, active: true));
                    Add(new Tag("screen", "The screen number. For example, the first display is screen 1 and the second display is screen 2", TagType.ScreenNumber, active: true));
                    Add(new Tag("format", "The image format such as jpeg or png", TagType.ImageFormat, active: true));
                    Add(new Tag("date", "The current date (%date%)", TagType.DateTimeFormat, MacroParser.DateFormat, active: true));
                    Add(new Tag("time", "The current time (%time%)", TagType.DateTimeFormat, MacroParser.TimeFormatForWindows, active: true));
                    Add(new Tag("year", "The current year (%year%)", TagType.DateTimeFormat, MacroParser.YearFormat, active: true));
                    Add(new Tag("month", "The current month (%month%)", TagType.DateTimeFormat, MacroParser.MonthFormat, active: true));
                    Add(new Tag("day", "The current day (%day%)", TagType.DateTimeFormat, MacroParser.DayFormat, active: true));
                    Add(new Tag("hour", "The current hour (%hour%)", TagType.DateTimeFormat, MacroParser.HourFormat, active: true));
                    Add(new Tag("minute", "The current minute (%minute%)", TagType.DateTimeFormat, MacroParser.MinuteFormat, active: true));
                    Add(new Tag("second", "The current second (%second%)", TagType.DateTimeFormat, MacroParser.SecondFormat, active: true));
                    Add(new Tag("millisecond", "The current millisecond (%millisecond%)", TagType.DateTimeFormat, MacroParser.MillisecondFormat, active: true));
                    Add(new Tag("lastyear", "The previous year (%lastyear%)", TagType.DateTimeFormatExpression, "{year-1}", active: true));
                    Add(new Tag("lastmonth", "The previous month (%lastmonth%)", TagType.DateTimeFormatExpression, "{month-1}", active: true));
                    Add(new Tag("yesterday", "The previous day (%yesterday%)", TagType.DateTimeFormatExpression, "{day-1}[yyyy-MM-dd]", active: true));
                    Add(new Tag("tomorrow", "The next day (%tomorrow%)", TagType.DateTimeFormatExpression, "{day+1}[yyyy-MM-dd]", active: true));
                    Add(new Tag("6hoursbehind", "Six hours behind the current hour (%6hoursbehind%)", TagType.DateTimeFormatExpression, "{hour-6}[yyyy-MM-dd_HH-mm-ss.fff]", active: true));
                    Add(new Tag("6hoursahead", "Six hours ahead the current hour (%6hoursahead%)", TagType.DateTimeFormatExpression, "{hour+6}[yyyy-MM-dd_HH-mm-ss.fff]", active: true));
                    Add(new Tag("count", "The number of capture cycles during a running screen capture session. For example, the first round of screenshots taken is the first cycle count or count 1", TagType.ScreenCaptureCycleCount, active: true));
                    Add(new Tag("user", "The user using this computer (%user%)", TagType.User, active: true));
                    Add(new Tag("machine", "The name of the computer (%machine%)", TagType.Machine, active: true));
                    Add(new Tag("title", "The title of the active window", TagType.ActiveWindowTitle, active: true));
                    Add(new Tag("timeofday", "The macro to use at a specific time of day so you can have a macro for the morning, a macro for the afternoon, and a macro for the evening. At the moment it is %timeofday%", TagType.TimeOfDay, active: true));

                    SaveToXmlFile();
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("TagCollection::LoadXmlFileAndAddTags", ex);

                return false;
            }
        }

        /// <summary>
        /// Saves the tags.
        /// </summary>
        public bool SaveToXmlFile()
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

                if (string.IsNullOrEmpty(FileSystem.TagsFile))
                {
                    FileSystem.TagsFile = FileSystem.DefaultTagsFile;

                    if (FileSystem.FileExists(FileSystem.ConfigFile))
                    {
                        FileSystem.AppendToFile(FileSystem.ConfigFile, "\nTagsFile=" + FileSystem.TagsFile);
                    }
                }

                if (FileSystem.FileExists(FileSystem.TagsFile))
                {
                    FileSystem.DeleteFile(FileSystem.TagsFile);
                }

                using (XmlWriter xWriter =
                    XmlWriter.Create(FileSystem.TagsFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_TAGS_NODE);

                    foreach (Tag tag in base.Collection)
                    {
                        xWriter.WriteStartElement(XML_FILE_TAG_NODE);

                        xWriter.WriteElementString(TAG_ACTIVE, tag.Active.ToString());
                        xWriter.WriteElementString(TAG_NAME, tag.Name);
                        xWriter.WriteElementString(TAG_DESCRIPTION, tag.Description);
                        xWriter.WriteElementString(TAG_NOTES, tag.Notes);
                        xWriter.WriteElementString(TAG_TYPE, tag.Type.ToString());
                        xWriter.WriteElementString(TAG_DATETIME_FORMAT_VALUE, tag.DateTimeFormatValue);
                        xWriter.WriteElementString(TAG_TIME_OF_DAY_MORNING_START, tag.TimeOfDayMorningStart.ToString());
                        xWriter.WriteElementString(TAG_TIME_OF_DAY_MORNING_END, tag.TimeOfDayMorningEnd.ToString());
                        xWriter.WriteElementString(TAG_TIME_OF_DAY_AFTERNOON_START, tag.TimeOfDayAfternoonStart.ToString());
                        xWriter.WriteElementString(TAG_TIME_OF_DAY_AFTERNOON_END, tag.TimeOfDayAfternoonEnd.ToString());
                        xWriter.WriteElementString(TAG_TIME_OF_DAY_EVENING_START, tag.TimeOfDayEveningStart.ToString());
                        xWriter.WriteElementString(TAG_TIME_OF_DAY_EVENING_END, tag.TimeOfDayEveningEnd.ToString());
                        xWriter.WriteElementString(TAG_TIME_OF_DAY_MORNING_VALUE, tag.TimeOfDayMorningValue);
                        xWriter.WriteElementString(TAG_TIME_OF_DAY_AFTERNOON_VALUE, tag.TimeOfDayAfternoonValue);
                        xWriter.WriteElementString(TAG_TIME_OF_DAY_EVENING_VALUE, tag.TimeOfDayEveningValue);
                        xWriter.WriteElementString(TAG_TIME_OF_DAY_EVENING_EXTENDS_TO_NEXT_MORNING, tag.EveningExtendsToNextMorning.ToString());

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
                Log.WriteExceptionMessage("TagCollection::SaveToXmlFile", ex);

                return false;
            }
        }
    }
}