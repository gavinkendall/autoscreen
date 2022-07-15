//-----------------------------------------------------------------------
// <copyright file="FormMain-Modules.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain
    {
        private ToolTip _toolTipButtonAdd = new ToolTip();
        private ToolTip _toolTipButtonConfigure = new ToolTip();
        private ToolTip _toolTipLabelEnabledStatus = new ToolTip();
        private ToolTip _toolTipButtonRemoveSelected = new ToolTip();

        // A generic method for building a module.
        private void BuildModule<T>(IEnumerable<T> list, TabPage tabPage,
            EventHandler eventHandlerForAdd, EventHandler eventHandlerForRemoveSelected, EventHandler eventhandlerForConfigure)
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
            Button buttonAdd = new Button
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
            buttonAdd.Click += eventHandlerForAdd;
            tabPage.Controls.Add(buttonAdd);
            _toolTipButtonAdd.SetToolTip(buttonAdd, "Add a new item to the list");

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
            _toolTipButtonRemoveSelected.SetToolTip(buttonRemoveSelected, "Remove selected items from the list");

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

                    if (_fileSystem.FileExists(application))
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
                        t.Equals(typeof(MacroTag)) ||
                        t.Equals(typeof(Schedule)) ||
                        t.Equals(typeof(Trigger)))
                    {
                        bool enabled = (bool)t.GetProperty("Enable").GetValue(@object, null);
                        labelEnabledStatus.BackColor = enabled ? Color.PaleGreen : Color.PaleVioletRed;

                        _toolTipLabelEnabledStatus.SetToolTip(labelEnabledStatus, "Green is enabled. Red is disabled. Click to toggle");
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
                    TabStop = false,
                    BackColor = Color.LightYellow
                };

                // Add a tool tip for any text that's more than 20 characters so you can hover over the text field
                // and see the full name of a Screen, Region, Trigger, Schedule, Macro Tag, etc.
                if (text.Length > 20)
                {
                    ToolTip toolTipForTextBoxObjectName = new ToolTip();
                    toolTipForTextBoxObjectName.AutomaticDelay = 100;
                    toolTipForTextBoxObjectName.AutoPopDelay = 5000;
                    toolTipForTextBoxObjectName.SetToolTip(textBoxObjectName, text);
                }

                // Add a button so that the user can configure the object.
                Button buttonConfigure = new Button
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
                buttonConfigure.Click += eventhandlerForConfigure;
                _toolTipButtonConfigure.SetToolTip(buttonConfigure, "Configure this item");

                Button buttonScheduleTimer = new Button();

                // The following is only specific to Schedules.
                if (t.Equals(typeof(Schedule)))
                {
                    buttonScheduleTimer = new Button
                    {
                        Size = new Size(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT),
                        Location = new Point(xPos + X_POS_BUTTON + 23, yPos),
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.Transparent,
                        ForeColor = Color.Transparent,
                        ImageAlign = ContentAlignment.MiddleCenter,
                        Tag = @object,
                        TabStop = false
                    };

                    // Convert the generic type to a Schedule.
                    Schedule schedule = (Schedule)Convert.ChangeType(@object, typeof(Schedule));

                    if (schedule.Timer.Enabled)
                    {
                        // If the schedule's timer is running make sure to indiciate this to the user by changing the color of the text field.
                        textBoxObjectName.BackColor = Color.PaleGreen;

                        // Change the image to a "stop" symbol.
                        buttonScheduleTimer.Image = Properties.Resources.stop_screen_capture;

                        // Add the Stop Schedule event to Click.
                        buttonScheduleTimer.Click += ScheduleModuleList_StopSchedule;
                    }
                    else
                    {
                        // Change the image to a "play" symbol.
                        buttonScheduleTimer.Image = Properties.Resources.start_screen_capture;

                        // Add the Start Schedule event to Click.
                        buttonScheduleTimer.Click += ScheduleModuleList_StartSchedule;
                    }
                }

                // Add the controls to the tab page.
                tabPage.Controls.Add(textBoxObjectName);
                tabPage.Controls.Add(buttonConfigure);

                if (t.Equals(typeof(Schedule)))
                {
                    tabPage.Controls.Add(buttonScheduleTimer);
                }

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
                
                if (region.Enable)
                {
                    region.Enable = false;
                    label.BackColor = Color.PaleVioletRed;
                }
                else
                {
                    region.Enable = true;
                    label.BackColor = Color.PaleGreen;
                }

                if (!_formRegion.RegionCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }

            if (label.Tag.GetType() == typeof(Schedule))
            {
                Schedule schedule = (Schedule)label.Tag;

                if (schedule.Enable)
                {
                    schedule.Enable = false;
                    label.BackColor = Color.PaleVioletRed;
                }
                else
                {
                    schedule.Enable = true;
                    label.BackColor = Color.PaleGreen;
                }

                if (!_formSchedule.ScheduleCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }

            if (label.Tag.GetType() == typeof(Screen))
            {
                Screen screen = (Screen)label.Tag;

                if (screen.Enable)
                {
                    screen.Enable = false;
                    label.BackColor = Color.PaleVioletRed;
                }
                else
                {
                    screen.Enable = true;
                    label.BackColor = Color.PaleGreen;
                }

                if (!_formScreen.ScreenCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }

            if (label.Tag.GetType() == typeof(MacroTag))
            {
                MacroTag tag = (MacroTag)label.Tag;

                if (tag.Enable)
                {
                    tag.Enable = false;
                    label.BackColor = Color.PaleVioletRed;
                }
                else
                {
                    tag.Enable = true;
                    label.BackColor = Color.PaleGreen;
                }

                if (!_formMacroTag.MacroTagCollection.SaveToXmlFile(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }

            if (label.Tag.GetType() == typeof(Trigger))
            {
                Trigger trigger = (Trigger)label.Tag;

                if (trigger.Enable)
                {
                    trigger.Enable = false;
                    label.BackColor = Color.PaleVioletRed;
                }
                else
                {
                    trigger.Enable = true;
                    label.BackColor = Color.PaleGreen;
                }

                if (!_formTrigger.TriggerCollection.SaveToXmlFile(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        private void BuildScreensModule()
        {
            BuildModule(_formScreen.ScreenCollection, tabPageScreens, addScreen_Click, removeSelectedScreens_Click, configureScreen_Click);
        }

        private void BuildRegionsModule()
        {
            BuildModule(_formRegion.RegionCollection, tabPageRegions, addRegion_Click, removeSelectedRegions_Click, configureRegion_Click);
        }

        private void BuildMacroTagsModule()
        {
            BuildModule(_formMacroTag.MacroTagCollection, tabPageMacroTags, addMacroTag_Click, removeSelectedMacroTags_Click, changeMacroTag_Click);
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
