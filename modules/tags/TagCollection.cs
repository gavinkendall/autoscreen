//-----------------------------------------------------------------------
// <copyright file="TagCollection.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
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

        private const string TAG_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_TAGS_NODE + "/" + XML_FILE_TAG_NODE;

        private static string AppCodename { get; set; }
        private static string AppVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
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
                if (File.Exists(FileSystem.TagsFile))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(FileSystem.TagsFile);

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
                                        tag.Type = (TagType)Enum.Parse(typeof(TagType), xReader.Value);
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
                                }
                            }
                        }

                        xReader.Close();

                        if (!string.IsNullOrEmpty(tag.Name))
                        {
                            Add(tag);
                        }
                    }

                    if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                    {
                        SaveToXmlFile();
                    }
                }
                else
                {
                    Log.Write($"WARNING: {FileSystem.TagsFile} not found. Creating default tags");

                    // Setup a few "built in" tags by default.
                    Add(new Tag("name", TagType.ScreenName));
                    Add(new Tag("screen", TagType.ScreenNumber));
                    Add(new Tag("format", TagType.ImageFormat));
                    Add(new Tag("date", TagType.DateTimeFormat, MacroParser.DateFormat));
                    Add(new Tag("time", TagType.DateTimeFormat, MacroParser.TimeFormatForWindows));
                    Add(new Tag("year", TagType.DateTimeFormat, MacroParser.YearFormat));
                    Add(new Tag("month", TagType.DateTimeFormat, MacroParser.MonthFormat));
                    Add(new Tag("day", TagType.DateTimeFormat, MacroParser.DayFormat));
                    Add(new Tag("hour", TagType.DateTimeFormat, MacroParser.HourFormat));
                    Add(new Tag("minute", TagType.DateTimeFormat, MacroParser.MinuteFormat));
                    Add(new Tag("second", TagType.DateTimeFormat, MacroParser.SecondFormat));
                    Add(new Tag("millisecond", TagType.DateTimeFormat, MacroParser.MillisecondFormat));
                    Add(new Tag("lastyear", TagType.DateTimeFormatFunction, "{year-1}"));
                    Add(new Tag("lastmonth", TagType.DateTimeFormatFunction, "{month-1}"));
                    Add(new Tag("yesterday", TagType.DateTimeFormatFunction, "{day-1}"));
                    Add(new Tag("tomorrow", TagType.DateTimeFormatFunction, "{day+1}"));
                    Add(new Tag("6hoursbehind", TagType.DateTimeFormatFunction, "{hour-6}"));
                    Add(new Tag("6hoursahead", TagType.DateTimeFormatFunction, "{hour+6}"));
                    Add(new Tag("count", TagType.ScreenCaptureCycleCount));
                    Add(new Tag("user", TagType.User));
                    Add(new Tag("machine", TagType.Machine));
                    Add(new Tag("title", TagType.ActiveWindowTitle));
                    Add(new Tag("timeofday", TagType.TimeOfDay));

                    SaveToXmlFile();
                }
            }
            catch (Exception ex)
            {
                Log.Write("TagCollection::LoadXmlFileAndAddTags", ex);
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

                    if (File.Exists(FileSystem.ConfigFile))
                    {
                        using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                        {
                            sw.WriteLine("TagsFile=" + FileSystem.TagsFile);
                        }
                    }
                }

                if (File.Exists(FileSystem.TagsFile))
                {
                    File.Delete(FileSystem.TagsFile);
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
                Log.Write("TagCollection::SaveToXmlFile", ex);
            }
        }
    }
}