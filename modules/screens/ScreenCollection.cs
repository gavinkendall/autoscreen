//-----------------------------------------------------------------------
// <copyright file="ScreenCollection.cs" company="Gavin Kendall">
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

    public class ScreenCollection : IEnumerable<Screen>
    {
        private readonly List<Screen> _screenList = new List<Screen>();

        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_SCREEN_NODE = "screen";
        private const string XML_FILE_SCREENS_NODE = "screens";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string SCREEN_NAME = "name";
        private const string SCREEN_FOLDER = "folder";
        private const string SCREEN_MACRO = "macro";
        private const string SCREEN_COMPONENT = "component";
        private const string SCREEN_FORMAT = "format";
        private const string SCREEN_JPEG_QUALITY = "jpeg_quality";
        private const string SCREEN_RESOLUTION_RATIO = "resolution_ratio";
        private const string SCREEN_MOUSE = "mouse";
        private const string SCREEN_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_SCREENS_NODE + "/" + XML_FILE_SCREEN_NODE;

        public List<Screen>.Enumerator GetEnumerator()
        {
            return _screenList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Screen>)_screenList).GetEnumerator();
        }

        IEnumerator<Screen> IEnumerable<Screen>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Screen screen)
        {
            _screenList.Add(screen);

            Log.Write("Screen added: " + screen.Name);
        }

        public void Remove(Screen screen)
        {
            _screenList.Remove(screen);

            Log.Write("Screen removed: " + screen.Name);
        }

        public int Count
        {
            get { return _screenList.Count; }
        }

        public Screen Get(Screen screenToFind)
        {
            foreach (Screen screen in _screenList)
            {
                if (screen.Equals(screenToFind))
                {
                    return screen;
                }
            }

            return null;
        }

        public Screen GetByName(string name)
        {
            foreach (Screen screen in _screenList)
            {
                if (screen.Name.Equals(name))
                {
                    return screen;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the screens.
        /// </summary>
        public void Load(ImageFormatCollection imageFormatCollection)
        {
            if (File.Exists(FileSystem.ApplicationFolder + FileSystem.ScreensFile))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(FileSystem.ApplicationFolder + FileSystem.ScreensFile);

                XmlNodeList xScreens = xDoc.SelectNodes(SCREEN_XPATH);

                foreach (XmlNode xScreen in xScreens)
                {
                    Screen screen = new Screen();
                    XmlNodeReader xReader = new XmlNodeReader(xScreen);

                    while (xReader.Read())
                    {
                        if (xReader.IsStartElement())
                        {
                            switch (xReader.Name)
                            {
                                case SCREEN_NAME:
                                    xReader.Read();
                                    screen.Name = xReader.Value;
                                    break;

                                case SCREEN_FOLDER:
                                    xReader.Read();
                                    screen.Folder = xReader.Value;
                                    break;

                                case SCREEN_MACRO:
                                    xReader.Read();
                                    screen.Macro = xReader.Value;
                                    break;

                                case SCREEN_COMPONENT:
                                    xReader.Read();
                                    screen.Component = Convert.ToInt32(xReader.Value);
                                    break;

                                case SCREEN_FORMAT:
                                    xReader.Read();
                                    screen.Format = imageFormatCollection.GetByName(xReader.Value);
                                    break;

                                case SCREEN_JPEG_QUALITY:
                                    xReader.Read();
                                    screen.JpegQuality = Convert.ToInt32(xReader.Value);
                                    break;

                                case SCREEN_RESOLUTION_RATIO:
                                    xReader.Read();
                                    screen.ResolutionRatio = Convert.ToInt32(xReader.Value);
                                    break;

                                case SCREEN_MOUSE:
                                    xReader.Read();
                                    screen.Mouse = Convert.ToBoolean(xReader.Value);
                                    break;
                            }
                        }
                    }

                    xReader.Close();

                    if (!string.IsNullOrEmpty(screen.Name))
                    {
                        Add(screen);
                    }
                }
            }
            else
            {
                // Setup some screens based on what we can find.

                Add(new Screen("Active Window", FileSystem.ScreenshotsFolder, MacroParser.ScreenMacro, 0,
                    imageFormatCollection.GetByName(ScreenCapture.DefaultImageFormat), 100, 100, true));

                for (int screenNumber = 1; screenNumber <= System.Windows.Forms.Screen.AllScreens.Length; screenNumber++)
                {
                    Add(new Screen($"Screen {screenNumber}", FileSystem.ScreenshotsFolder, MacroParser.ScreenMacro, screenNumber,
                        imageFormatCollection.GetByName(ScreenCapture.DefaultImageFormat), 100, 100, true));
                }

                Save();
            }
        }

        /// <summary>
        /// Saves the screens.
        /// </summary>
        public void Save()
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

            using (XmlWriter xWriter = XmlWriter.Create(FileSystem.ApplicationFolder + FileSystem.ScreensFile, xSettings))
            {
                xWriter.WriteStartDocument();
                xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                xWriter.WriteStartElement(XML_FILE_SCREENS_NODE);

                foreach (object obj in _screenList)
                {
                    Screen screen = (Screen)obj;

                    xWriter.WriteStartElement(XML_FILE_SCREEN_NODE);
                    xWriter.WriteElementString(SCREEN_NAME, screen.Name);
                    xWriter.WriteElementString(SCREEN_FOLDER, screen.Folder);
                    xWriter.WriteElementString(SCREEN_MACRO, screen.Macro);
                    xWriter.WriteElementString(SCREEN_COMPONENT, screen.Component.ToString());
                    xWriter.WriteElementString(SCREEN_FORMAT, screen.Format.Name);
                    xWriter.WriteElementString(SCREEN_JPEG_QUALITY, screen.JpegQuality.ToString());
                    xWriter.WriteElementString(SCREEN_RESOLUTION_RATIO, screen.ResolutionRatio.ToString());
                    xWriter.WriteElementString(SCREEN_MOUSE, screen.Mouse.ToString());

                    xWriter.WriteEndElement();
                }

                xWriter.WriteEndElement();
                xWriter.WriteEndElement();
                xWriter.WriteEndDocument();

                xWriter.Flush();
                xWriter.Close();
            }
        }
    }
}
