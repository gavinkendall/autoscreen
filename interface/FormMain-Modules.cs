using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain
    {
        // A generic method for building a module.
        private void BuildModule<T>(string name, IEnumerable<T> list, TabPage tabPage,
            EventHandler eventHandlerForAddNew, EventHandler eventHandlerForRemoveSelected, EventHandler eventhandlerForChange)
        {
            int xPos = 5;
            int yPos = 3;

            const int HEIGHT = 20;
            const int CHECKBOX_WIDTH = 20;
            const int CHECKBOX_HEIGHT = 20;
            const int X_POS_ICON = 20;
            const int BIG_BUTTON_WIDTH = 205;
            const int BIG_BUTTON_HEIGHT = 25;
            const int SMALL_IMAGE_WIDTH = 20;
            const int SMALL_IMAGE_HEIGHT = 20;
            const int SMALL_BUTTON_WIDTH = 27;
            const int SMALL_BUTTON_HEIGHT = 20;
            const int X_POS_TEXTBOX = 48;
            const int X_POS_BUTTON = 178;
            const int TEXTBOX_WIDTH = 125;
            const int Y_POS_INCREMENT = 23;
            const int TEXTBOX_MAX_LENGTH = 50;

            const string EDIT_BUTTON_TEXT = "...";

            tabPage.Controls.Clear();

            // The button for adding a new object (this could be a Screen, Region, Editor, Trigger, or Tag).
            Button buttonAddNew = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPos, yPos),
                Text = $"Add New {name} ...",
                TabStop = false
            };
            buttonAddNew.Click += eventHandlerForAddNew;
            tabPage.Controls.Add(buttonAddNew);

            // Move down a bit.
            yPos += 27;

            // Render the button for removing multiple selected objects.
            Button buttonRemoveSelected = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPos, yPos),
                Text = $"Remove Selected {name}s",
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

                    if (File.Exists(application))
                    {
                        // Add an image showing the application icon of the Editor if we can find the application's path.
                        appIcon.Image = Icon.ExtractAssociatedIcon(application).ToBitmap();
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

                    // Types of Screen, Region, Tag, Schedule, and Trigger have an "Enabled" property
                    // so we change the color of the label background depending on the value of "Enabled".
                    if (t.Equals(typeof(Screen)) ||
                        t.Equals(typeof(Region)) ||
                        t.Equals(typeof(Tag)) ||
                        t.Equals(typeof(Schedule)) ||
                        t.Equals(typeof(Trigger)))
                    {
                        bool enabled = (bool)t.GetProperty("Enabled").GetValue(@object, null);
                        labelEnabledStatus.BackColor = enabled ? Color.PaleGreen : Color.PaleVioletRed;
                    }

                    labelEnabledStatus.Click += Click_enabledStatus;

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
                    Text = EDIT_BUTTON_TEXT,
                    Tag = @object,
                    TabStop = false
                };
                buttonChange.Click += eventhandlerForChange;
                tabPage.Controls.Add(buttonChange);

                // Move down the tab page so we're ready to loop around again and add the next object to it.
                yPos += Y_POS_INCREMENT;
            }
        }

        private void Click_enabledStatus(object sender, EventArgs e)
        {
            Label label = (Label)sender;

            if (label.Tag.GetType() == typeof(Screen))
            {
                Screen screen = (Screen)label.Tag;
                
                if (screen.Enabled)
                {
                    screen.Enabled = false;
                    label.BackColor = Color.Red;
                }
                else
                {
                    screen.Enabled = true;
                    label.BackColor = Color.Green;
                }
            }

            //Type t = label.Tag.GetType();

            //Console.WriteLine(sender.ToString());
        }

        private void BuildScreensModule()
        {
            BuildModule("Screen", formScreen.ScreenCollection, tabPageScreens, Click_addScreen, Click_removeSelectedScreens, Click_changeScreen);
        }

        private void BuildRegionsModule()
        {
            BuildModule("Region", formRegion.RegionCollection, tabPageRegions, Click_addRegion, Click_removeSelectedRegions, Click_changeRegion);
        }

        private void BuildTagsModule()
        {
            BuildModule("Tag", formTag.TagCollection, tabPageTags, Click_addTag, Click_removeSelectedTags, Click_changeTag);
        }

        private void BuildEditorsModule()
        {
            BuildModule("Editor", formEditor.EditorCollection, tabPageEditors, Click_addEditor, Click_removeSelectedEditors, Click_changeEditor);
        }

        private void BuildTriggersModule()
        {
            BuildModule("Trigger", formTrigger.TriggerCollection, tabPageTriggers, Click_addTrigger, Click_removeSelectedTriggers, Click_changeTrigger);
        }

        private void BuildSchedulesModule()
        {
            BuildModule("Schedule", formSchedule.ScheduleCollection, tabPageSchedules, Click_addSchedule, Click_removeSelectedSchedules, Click_changeSchedule);
        }
    }
}
