//-----------------------------------------------------------------------
// <copyright file="ScheduleCollection.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace AutoScreenCapture
{
    /// <summary>
    /// A collection class to store and manage Schedule objects.
    /// </summary>
    public class ScheduleCollection : IEnumerable<Schedule>
    {
        private readonly List<Schedule> _scheduleList = new List<Schedule>();

        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_SCHEDULE_NODE = "schedule";
        private const string XML_FILE_SCHEDULES_NODE = "schedules";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string SCHEDULE_NAME = "name";
        private const string SCHEDULE_ARGUMENTS = "arguments";
        private const string SCHEDULE_APPLICATION = "application";
        private const string SCHEDULE_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_SCHEDULES_NODE + "/" + XML_FILE_SCHEDULE_NODE;

        private static string AppCodename { get; set; }
        private static string AppVersion { get; set; }

        /// <summary>
        /// Returns the enumerator for the collection.
        /// </summary>
        /// <returns>A list of Schedule objects.</returns>
        public List<Schedule>.Enumerator GetEnumerator()
        {
            return _scheduleList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Schedule>)_scheduleList).GetEnumerator();
        }

        IEnumerator<Schedule> IEnumerable<Schedule>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds an Schedule object to the collection.
        /// </summary>
        /// <param name="schedule">An Schedule object to add.</param>
        public void Add(Schedule schedule)
        {
            _scheduleList.Add(schedule);

            Log.Write("Schedule added: " + schedule.Name);
        }

        /// <summary>
        /// Removes an Schedule object from the collection.
        /// </summary>
        /// <param name="schedule">The Schedule object to remove.</param>
        public void Remove(Schedule schedule)
        {
            _scheduleList.Remove(schedule);

            Log.Write("Schedule removed: " + schedule.Name);
        }

        /// <summary>
        /// Gets the number of Schedule objects in the collection.
        /// </summary>
        /// <returns>A count of Schedule objects.</returns>
        public int Count
        {
            get { return _scheduleList.Count; }
        }

        /// <summary>
        /// Gets a specific Schedule object from the collection.
        /// </summary>
        /// <param name="scheduleToFind">The Schedule object to retrieve.</param>
        /// <returns>An Schedule object.</returns>
        public Schedule Get(Schedule scheduleToFind)
        {
            foreach (Schedule schedule in _scheduleList)
            {
                if (schedule.Equals(scheduleToFind))
                {
                    return schedule;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets an Schedule object based on its name.
        /// </summary>
        /// <param name="name">The name of an Schedule object.</param>
        /// <returns>An Schedule object.</returns>
        public Schedule GetByName(string name)
        {
            foreach (Schedule schedule in _scheduleList)
            {
                if (schedule.Name.Equals(name))
                {
                    return schedule;
                }
            }

            return null;
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
                                }
                            }
                        }

                        xReader.Close();

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
                    Log.Write($"WARNING: {FileSystem.SchedulesFile} not found. Unable to load schedules");
                }
            }
            catch (Exception ex)
            {
                Log.Write("ScheduleCollection::LoadXmlFileAndAddSchedules", ex);
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

                    foreach (object obj in _scheduleList)
                    {
                        Schedule schedule = (Schedule) obj;

                        xWriter.WriteStartElement(XML_FILE_SCHEDULE_NODE);
                        xWriter.WriteElementString(SCHEDULE_NAME, schedule.Name);

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
                Log.Write("ScheduleCollection::SaveToXmlFile", ex);
            }
        }
    }
}