//-----------------------------------------------------------------------
// <copyright file="TagCollection.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// 
    /// </summary>
    public class TagCollection : IEnumerable<Tag>
    {
        private readonly List<Tag> _tagList = new List<Tag>();

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

        private const string TAG_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_TAGS_NODE + "/" + XML_FILE_TAG_NODE;

        private static string AppCodename { get; set; }
        private static string AppVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Tag>.Enumerator GetEnumerator()
        {
            return _tagList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Tag>)_tagList).GetEnumerator();
        }

        IEnumerator<Tag> IEnumerable<Tag>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        public void Add(Tag tag)
        {
            if (string.IsNullOrEmpty(tag.Name)) return;

            if (!tag.Name.StartsWith("%"))
                tag.Name = "%" + tag.Name;

            if (!tag.Name.EndsWith("%"))
                tag.Name += "%";

            _tagList.Add(tag);

            Log.Write("Tag added: " + tag.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        public void Remove(Tag tag)
        {
            _tagList.Remove(tag);

            Log.Write("Tag removed: " + tag.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return _tagList.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagToFind"></param>
        /// <returns></returns>
        public Tag Get(Tag tagToFind)
        {
            foreach (Tag tag in _tagList)
            {
                if (tag.Equals(tagToFind))
                {
                    return tag;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Tag GetByName(string name)
        {
            foreach (Tag tag in _tagList)
            {
                if (tag.Name.Equals(name))
                {
                    return tag;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the tags.
        /// </summary>
        public void Load()
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
                        Save();
                    }
                }
                else
                {
                    Log.Write($"WARNING: {FileSystem.TagsFile} not found. Creating default tags");

                    // Setup a few "built in" tags by default.
                    Add(new Tag("name", TagType.ScreenName, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("screen", TagType.ScreenNumber, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("format", TagType.ImageFormat, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("date", TagType.DateTimeFormat, MacroParser.DateFormat, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("time", TagType.DateTimeFormat, MacroParser.TimeFormatForWindows, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("year", TagType.DateTimeFormat, MacroParser.YearFormat, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("month", TagType.DateTimeFormat, MacroParser.MonthFormat, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("day", TagType.DateTimeFormat, MacroParser.DayFormat, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("hour", TagType.DateTimeFormat, MacroParser.HourFormat, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("minute", TagType.DateTimeFormat, MacroParser.MinuteFormat, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("second", TagType.DateTimeFormat, MacroParser.SecondFormat, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("millisecond", TagType.DateTimeFormat, MacroParser.MillisecondFormat, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("count", TagType.ScreenCaptureCycleCount, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("user", TagType.User, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("machine", TagType.Machine, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));
                    Add(new Tag("title", TagType.ActiveWindowTitle, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty, DateTime.Now, DateTime.Now, string.Empty));

                    // The time of the day split out into morning, afternoon, and evening using default DateTime values.
                    Add(new Tag("timeofday", TagType.TimeOfDay,
                        dateTimeFormatValue: string.Empty,
                        timeOfDayMorningStart: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0),
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 59, 59),
                        timeOfDayMorningValue: "morning",
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0),
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 59, 59),
                        timeOfDayAfternoonValue: "afternoon",
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0),
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59),
                        timeOfDayEveningValue: "evening"));

                    Save();
                }
            }
            catch (Exception ex)
            {
                Log.Write("TagCollection::Load", ex);
            }
        }

        /// <summary>
        /// Saves the tags.
        /// </summary>
        public void Save()
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

                    foreach (object obj in _tagList)
                    {
                        Tag tag = (Tag)obj;

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
                Log.Write("TagCollection::Save", ex);
            }
        }
    }
}