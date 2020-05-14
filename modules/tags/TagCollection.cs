//-----------------------------------------------------------------------
// <copyright file="TagCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A collection of macro tags.</summary>
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
        public void LoadXmlFileAndAddTags()
        {
            try
            {
                Log.WriteDebugMessage(":: LoadXmlFileAndAddTags Start ::");

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
                            if (xReader.IsStartElement())
                            {
                                switch (xReader.Name)
                                {
                                    case TAG_NAME:
                                        xReader.Read();
                                        tag.Name = xReader.Value;
                                        break;

                                    case TAG_TYPE:
                                        xReader.Read();

                                        string value = xReader.Value;

                                        if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                                        {
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
                    Log.WriteDebugMessage($"WARNING: {FileSystem.TagsFile} not found. Creating default tags");

                    // Setup a few "built in" tags by default.
                    Add(new Tag("name", TagType.ScreenName, active: true));
                    Add(new Tag("screen", TagType.ScreenNumber, active: true));
                    Add(new Tag("format", TagType.ImageFormat, active: true));
                    Add(new Tag("date", TagType.DateTimeFormat, MacroParser.DateFormat, active: true));
                    Add(new Tag("time", TagType.DateTimeFormat, MacroParser.TimeFormatForWindows, active: true));
                    Add(new Tag("year", TagType.DateTimeFormat, MacroParser.YearFormat, active: true));
                    Add(new Tag("month", TagType.DateTimeFormat, MacroParser.MonthFormat, active: true));
                    Add(new Tag("day", TagType.DateTimeFormat, MacroParser.DayFormat, active: true));
                    Add(new Tag("hour", TagType.DateTimeFormat, MacroParser.HourFormat, active: true));
                    Add(new Tag("minute", TagType.DateTimeFormat, MacroParser.MinuteFormat, active: true));
                    Add(new Tag("second", TagType.DateTimeFormat, MacroParser.SecondFormat, active: true));
                    Add(new Tag("millisecond", TagType.DateTimeFormat, MacroParser.MillisecondFormat, active: true));
                    Add(new Tag("lastyear", TagType.DateTimeFormatExpression, "{year-1}", active: true));
                    Add(new Tag("lastmonth", TagType.DateTimeFormatExpression, "{month-1}", active: true));
                    Add(new Tag("yesterday", TagType.DateTimeFormatExpression, "{day-1}", active: true));
                    Add(new Tag("tomorrow", TagType.DateTimeFormatExpression, "{day+1}", active: true));
                    Add(new Tag("6hoursbehind", TagType.DateTimeFormatExpression, "{hour-6}", active: true));
                    Add(new Tag("6hoursahead", TagType.DateTimeFormatExpression, "{hour+6}", active: true));
                    Add(new Tag("count", TagType.ScreenCaptureCycleCount, active: true));
                    Add(new Tag("user", TagType.User, active: true));
                    Add(new Tag("machine", TagType.Machine, active: true));
                    Add(new Tag("title", TagType.ActiveWindowTitle, active: true));
                    Add(new Tag("timeofday", TagType.TimeOfDay, active: true));

                    SaveToXmlFile();
                }

                Log.WriteDebugMessage(":: LoadXmlFileAndAddTags End ::");
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("TagCollection::LoadXmlFileAndAddTags", ex);
            }
        }

        /// <summary>
        /// Saves the tags.
        /// </summary>
        public void SaveToXmlFile()
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
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("TagCollection::SaveToXmlFile", ex);
            }
        }
    }
}