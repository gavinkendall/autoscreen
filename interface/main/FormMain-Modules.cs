//-----------------------------------------------------------------------
// <copyright file="FormMain-Modules.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for handling modules. This uses generics so we can build modules without having separate methods specifically for each type of module. Each module shared common interface elements so it made sense to use generics. Any changes applied here will affect every module being built.</summary>
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
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain
    {
        // A generic method for building a module.
        private void BuildModule<T>(IEnumerable<T> list, TabPage tabPage,
            EventHandler eventHandlerForAddNew, EventHandler eventHandlerForRemoveSelected, EventHandler eventhandlerForChange)
        {
            int xPos = 5;
            int yPos = 3;

            const int HEIGHT = 20;
            const int CHECKBOX_WIDTH = 20;
            const int CHECKBOX_HEIGHT = 20;
            const int X_POS_ICON = 20;
            const int SMALL_IMAGE_WIDTH = 20;
            const int SMALL_IMAGE_HEIGHT = 20;
            const int SMALL_BUTTON_WIDTH = 21;
            const int SMALL_BUTTON_HEIGHT = 21;
            const int X_POS_TEXTBOX = 48;
            const int X_POS_BUTTON = 178;
            const int TEXTBOX_WIDTH = 125;
            const int Y_POS_INCREMENT = 23;
            const int TEXTBOX_MAX_LENGTH = 50;

            tabPage.Controls.Clear();

            // The button for adding a new object (this could be a Screen, Region, Editor, Trigger, or Tag).
            Button buttonAddNew = new Button
            {
                Size = new Size(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT),
                Location = new Point(xPos, yPos),
                Image = Properties.Resources.add,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.Transparent,
                ImageAlign = ContentAlignment.MiddleCenter,
                TabStop = false
            };
            buttonAddNew.Click += eventHandlerForAddNew;
            tabPage.Controls.Add(buttonAddNew);

            // Render the button for removing multiple selected objects.
            Button buttonRemoveSelected = new Button
            {
                Size = new Size(SMALL_IMAGE_WIDTH, SMALL_IMAGE_HEIGHT),
                Location = new Point(xPos + 27, yPos),
                Image = Properties.Resources.delete,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.Transparent,
                ImageAlign = ContentAlignment.MiddleCenter,
                TabStop = false
            };
            buttonRemoveSelected.Click += eventHandlerForRemoveSelected;
            tabPage.Controls.Add(buttonRemoveSelected);

            // Move down a bit.
            yPos += 28;

            // Populate the tab page with each object from the list of objects.
            foreach (T @object in list)
            {
                // Add a checkbox so that the user has the ability to remove the selected object.
                CheckBox checkbox = new CheckBox
                {
                    Size = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT),
                    Location = new Point(xPos, yPos),
                    Tag = @object,
                    TabStop = false
                };
                tabPage.Controls.Add(checkbox);

                Type t = @object.GetType();

                // Find the "Name" property of the object. Every object of type Screen, Editor, Region, etc. has this property.
                string text = t.GetProperty("Name").GetValue(@object, null).ToString();

                // Editors have application icons so we'd like to display those in the list on the tab page.
                if (t.Equals(typeof(Editor)))
                {
                    string application = t.GetProperty("Application").GetValue(@object, null).ToString();

                    PictureBox appIcon = new PictureBox
                    {
                        Size = new Size(SMALL_IMAGE_WIDTH, SMALL_IMAGE_HEIGHT),
                        Location = new Point(xPos + X_POS_ICON, yPos),
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };

                    if (FileSystem.FileExists(application))
                    {
                        // Add an image showing the application icon of the Editor if we can find the application's path.
                        appIcon.Image = Icon.ExtractAssociatedIcon(application).ToBitmap();
                    }
                    else
                    {
                        appIcon.Image = Properties.Resources.warning;
                    }

                    tabPage.Controls.Add(appIcon);
                }
                else
                {
                    // Show the status of the object in a label and make the background color green if it's enabled or red if it's disabled.
                    Label labelEnabledStatus = new Label
                    {
                        Size = new Size(SMALL_IMAGE_WIDTH, SMALL_IMAGE_HEIGHT),
                        Location = new Point(xPos + X_POS_ICON, yPos),
                        Tag = @object,
                        TabStop = false,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    // Types of Screen, Region, Tag, Schedule, and Trigger have an "Active" property
                    // so we change the color of the label background depending on the value of "Active".
                    if (t.Equals(typeof(Screen)) ||
                        t.Equals(typeof(Region)) ||
                        t.Equals(typeof(Tag)) ||
                        t.Equals(typeof(Schedule)) ||
                        t.Equals(typeof(Trigger)))
                    {
                        bool enabled = (bool)t.GetProperty("Active").GetValue(@object, null);
                        labelEnabledStatus.BackColor = enabled ? Color.PaleGreen : Color.PaleVioletRed;
                    }

                    labelEnabledStatus.Click += enabledStatus_Click;

                    tabPage.Controls.Add(labelEnabledStatus);
                }

                // Add another read-only text box showing the name of the object.
                TextBox textBoxObjectName = new TextBox
                {
                    Width = TEXTBOX_WIDTH,
                    Height = HEIGHT,
                    MaxLength = TEXTBOX_MAX_LENGTH,
                    Location = new Point(xPos + X_POS_TEXTBOX, yPos),
                    Text = text,
                    ReadOnly = true,
                    TabStop = false
                };

                tabPage.Controls.Add(textBoxObjectName);

                // Add a button so that the user can change the object.
                Button buttonChange = new Button
                {
                    Size = new Size(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT),
                    Location = new Point(xPos + X_POS_BUTTON, yPos),
                    Image = Properties.Resources.configure,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Transparent,
                    ForeColor = Color.Transparent,
                    ImageAlign = ContentAlignment.MiddleCenter,
                    Tag = @object,
                    TabStop = false
                };
                buttonChange.Click += eventhandlerForChange;
                tabPage.Controls.Add(buttonChange);

                // Move down the tab page so we're ready to loop around again and add the next object to it.
                yPos += Y_POS_INCREMENT;
            }
        }

        private void enabledStatus_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;

            if (label.Tag.GetType() == typeof(Region))
            {
                Region region = (Region)label.Tag;
                
                if (region.Active)
                {
                    region.Active = false;
                    label.BackColor = Color.PaleVioletRed;
                }
                else
                {
                    region.Active = true;
                    label.BackColor = Color.PaleGreen;
                }

                if (!_formRegion.RegionCollection.SaveToXmlFile())
                {
                    _screenCapture.ApplicationError = true;
                }
            }

            if (label.Tag.GetType() == typeof(Schedule))
            {
                Schedule schedule = (Schedule)label.Tag;

                if (schedule.Active)
                {
                    schedule.Active = false;
                    label.BackColor = Color.PaleVioletRed;
                }
                else
                {
                    schedule.Active = true;
                    label.BackColor = Color.PaleGreen;
                }

                if (!_formSchedule.ScheduleCollection.SaveToXmlFile())
                {
                    _screenCapture.ApplicationError = true;
                }
            }

            if (label.Tag.GetType() == typeof(Screen))
            {
                Screen screen = (Screen)label.Tag;

                if (screen.Active)
                {
                    screen.Active = false;
                    label.BackColor = Color.PaleVioletRed;
                }
                else
                {
                    screen.Active = true;
                    label.BackColor = Color.PaleGreen;
                }

                if (!_formScreen.ScreenCollection.SaveToXmlFile())
                {
                    _screenCapture.ApplicationError = true;
                }
            }

            if (label.Tag.GetType() == typeof(Tag))
            {
                Tag tag = (Tag)label.Tag;

                if (tag.Active)
                {
                    tag.Active = false;
                    label.BackColor = Color.PaleVioletRed;
                }
                else
                {
                    tag.Active = true;
                    label.BackColor = Color.PaleGreen;
                }

                if (!_formTag.TagCollection.SaveToXmlFile())
                {
                    _screenCapture.ApplicationError = true;
                }
            }

            if (label.Tag.GetType() == typeof(Trigger))
            {
                Trigger trigger = (Trigger)label.Tag;

                if (trigger.Active)
                {
                    trigger.Active = false;
                    label.BackColor = Color.PaleVioletRed;
                }
                else
                {
                    trigger.Active = true;
                    label.BackColor = Color.PaleGreen;
                }

                if (!_formTrigger.TriggerCollection.SaveToXmlFile())
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        private void BuildScreensModule()
        {
            BuildModule(_formScreen.ScreenCollection, tabPageScreens, addScreen_Click, removeSelectedScreens_Click, changeScreen_Click);
        }

        private void BuildRegionsModule()
        {
            BuildModule(_formRegion.RegionCollection, tabPageRegions, addRegion_Click, removeSelectedRegions_Click, changeRegion_Click);
        }

        private void BuildTagsModule()
        {
            BuildModule(_formTag.TagCollection, tabPageTags, addTag_Click, removeSelectedTags_Click, changeTag_Click);
        }

        private void BuildEditorsModule()
        {
            BuildModule(_formEditor.EditorCollection, tabPageEditors, addEditor_Click, removeSelectedEditors_Click, changeEditor_Click);
        }

        private void BuildTriggersModule()
        {
            BuildModule(_formTrigger.TriggerCollection, tabPageTriggers, addTrigger_Click, removeSelectedTriggers_Click, changeTrigger_Click);
        }

        private void BuildSchedulesModule()
        {
            BuildModule(_formSchedule.ScheduleCollection, tabPageSchedules, addSchedule_Click, removeSelectedSchedules_Click, changeSchedule_Click);
        }
    }
}
