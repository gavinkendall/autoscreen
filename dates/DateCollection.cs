//-----------------------------------------------------------------------
// <copyright file="DateCollection.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------

using System.IO;
using System.Linq;
using System.Text;

namespace AutoScreenCapture
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Threading;

    public class DateCollection
    {
        private XmlDocument xDoc;
        private List<Date> _dateList;

        // Required when multiple threads are writing to the same file.
        private readonly Mutex _mutexWriteFile = new Mutex();

        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_DATE_NODE = "date";
        private const string XML_FILE_DATES_NODE = "dates";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string DATE_VALUE = "value";

        private const string DATES_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_DATES_NODE;
        private const string DATE_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_DATES_NODE + "/" + XML_FILE_DATE_NODE;

        private string AppCodename { get; set; }
        private string AppVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        public void Add(Date date)
        {
            lock (_dateList)
            {
                if (!_dateList.Contains(date))
                {
                    _dateList.Add(date);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Date Get(int index)
        {
            return (Date) _dateList[index];
        }

        public void Load()
        {
            try
            {
                if (_dateList == null)
                {
                    _dateList = new List<Date>();
                    Log.Write("Initialized date list");
                }

                if (_dateList != null && !File.Exists(FileSystem.DatesFile))
                {
                    Log.Write("Could not find \"" + FileSystem.DatesFile + "\" so creating default version");

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

                    using (XmlWriter xWriter = XmlWriter.Create(FileSystem.DatesFile, xSettings))
                    {
                        xWriter.WriteStartDocument();
                        xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                        xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                        xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                        xWriter.WriteStartElement(XML_FILE_DATES_NODE);

                        xWriter.WriteEndElement();
                        xWriter.WriteEndElement();
                        xWriter.WriteEndDocument();

                        xWriter.Flush();
                        xWriter.Close();
                    }

                    Log.Write("Created \"" + FileSystem.DatesFile + "\"");
                }

                if (_dateList != null && File.Exists(FileSystem.DatesFile))
                {
                    xDoc = new XmlDocument();

                    lock (xDoc)
                    {
                        xDoc.Load(FileSystem.DatesFile);

                        AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                        AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                        Log.Write("Getting dates from \"" + FileSystem.DatesFile + "\" using XPath query \"" + DATE_XPATH + "\"");

                        XmlNodeList xDates = xDoc.SelectNodes(DATE_XPATH);

                        if (xDates != null)
                        {
                            Log.Write("Loading " + xDates.Count + " dates from \"" + FileSystem.DatesFile + "\" ...");

                            foreach (XmlNode xDate in xDates)
                            {
                                Date date = new Date();

                                XmlNodeReader xReader = new XmlNodeReader(xDate);

                                while (xReader.Read())
                                {
                                    if (xReader.IsStartElement())
                                    {
                                        switch (xReader.Name)
                                        {
                                            case DATE_VALUE:
                                                xReader.Read();
                                                date.Value = Convert.ToDateTime(xReader.Value);
                                                break;
                                        }
                                    }
                                }

                                xReader.Close();
                            }
                        }
                        else
                        {
                            Log.Write("WARNING: Unable to load dates from \"" + FileSystem.DatesFile + "\"");
                        }

                        if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                        {
                            Log.Write("Old application version discovered when loading \"" + FileSystem.DatesFile + "\"");

                            // Apply the latest version and codename if the version of Auto Screen Capture is older than this version.
                            xDoc.SelectSingleNode("/" + XML_FILE_ROOT_NODE).Attributes["app:version"].Value = Settings.ApplicationVersion;
                            xDoc.SelectSingleNode("/" + XML_FILE_ROOT_NODE).Attributes["app:codename"].Value = Settings.ApplicationCodename;

                            xDoc.Save(FileSystem.DatesFile);

                            Log.Write("Upgraded \"" + FileSystem.DatesFile + "\"");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("DateCollection::Load", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keepScreenshotsForDays"></param>
        public void Save(int keepDatesForDays)
        {
            try
            {
                _mutexWriteFile.WaitOne();

                if (string.IsNullOrEmpty(FileSystem.DatesFile))
                {
                    FileSystem.DatesFile = FileSystem.DefaultDatesFile;

                    if (xDoc == null)
                    {
                        xDoc = new XmlDocument();

                        XmlElement rootElement = xDoc.CreateElement(XML_FILE_ROOT_NODE);
                        XmlElement xDates = xDoc.CreateElement(XML_FILE_DATES_NODE);

                        XmlAttribute attributeVersion = xDoc.CreateAttribute("app", "version", "autoscreen");
                        XmlAttribute attributeCodename = xDoc.CreateAttribute("app", "codename", "autoscreen");

                        attributeVersion.Value = Settings.ApplicationVersion;
                        attributeCodename.Value = Settings.ApplicationCodename;

                        rootElement.Attributes.Append(attributeVersion);
                        rootElement.Attributes.Append(attributeCodename);

                        rootElement.AppendChild(xDates);

                        xDoc.AppendChild(rootElement);

                        xDoc.Save(FileSystem.DatesFile);
                    }

                    if (File.Exists(FileSystem.ConfigFile))
                    {
                        using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                        {
                            sw.WriteLine("DatesFile=" + FileSystem.DatesFile);
                        }
                    }
                }

                lock (_dateList)
                {
                    if (_dateList != null && _dateList.Count > 0 && keepDatesForDays > 0)
                    {
                        List<Date> datesToDelete = _dateList.Where(x => x.Value != null && Convert.ToDateTime(x.Value) <= DateTime.Now.Date.AddDays(-keepDatesForDays)).ToList();

                        if (datesToDelete != null && datesToDelete.Count > 0)
                        {
                            foreach (Date date in datesToDelete)
                            {
                                XmlNodeList nodesToDelete = xDoc.SelectNodes(DATE_XPATH + "[" + DATE_VALUE + "='" + date.Value + "']");

                                foreach (XmlNode node in nodesToDelete)
                                {
                                    node.ParentNode.RemoveChild(node);
                                }

                                _dateList.Remove(date);
                            }
                        }
                    }

                    for (int i = 0; i < _dateList.Count; i++)
                    {
                        Date date = _dateList[i];

                        if (!date.Saved && xDoc != null)
                        {
                            XmlElement xDate = xDoc.CreateElement(XML_FILE_DATE_NODE);

                            XmlElement xValue = xDoc.CreateElement(DATE_VALUE);
                            xValue.InnerText = date.Value.ToString();

                            xDate.AppendChild(xValue);

                            XmlNode xDates = xDoc.SelectSingleNode(DATES_XPATH);

                            if (xDates != null)
                            {
                                if (xDates.HasChildNodes)
                                {
                                    xDates.InsertAfter(xDate, xDates.LastChild);
                                }
                                else
                                {
                                    xDates.AppendChild(xDate);
                                }

                                date.Saved = true;

                                _dateList[i] = date;
                            }
                        }
                    }

                    if (xDoc != null)
                    {
                        lock (xDoc)
                        {
                            xDoc.Save(FileSystem.DatesFile);
                        }
                    }
                }
            }
            finally
            {
                _mutexWriteFile.ReleaseMutex();
            }
        }
    }
}
