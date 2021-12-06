//-----------------------------------------------------------------------
// <copyright file="FormMain-TabPages.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Methods for building the tag pages for screens and regions including the Edit, Email, and Configure controls.</summary>
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
using System.Drawing;
using System.Windows.Forms;
using AutoScreenCapture.Properties;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        private FlowLayoutPanel flowLayoutPanel;

        const int MIN_DASHBOARD_GROUPBOX_SIZE = 100;
        const int MAX_DASHBOARD_GROUPBOX_SIZE = 900;

        int dashboardGroupBoxSize = 200;

        private void BuildViewTabPages()
        {
            tabControlViews.Controls.Clear();

            ToolStrip toolStripDashboard = new ToolStrip
            {
                Name = "toolStripDasboard",
                GripStyle = ToolStripGripStyle.Hidden
            };
            
            ToolStripDropDownButton add = new ToolStripDropDownButton
            {
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                Text = "Add",
                Image = Resources.add
            };

            ToolStripMenuItem addScreen = new ToolStripMenuItem
            {
                Text = "Screen",
                AutoToolTip = false,
                Image = Resources.screen
            };

            addScreen.Click += new EventHandler(addScreen_Click);
            add.DropDown.Items.Add(addScreen);

            ToolStripMenuItem addRegion = new ToolStripMenuItem
            {
                Text = "Region",
                AutoToolTip = false,
                Image = Resources.region
            };

            addRegion.Click += new EventHandler(addRegion_Click);
            add.DropDown.Items.Add(addRegion);

            ToolStripMenuItem addEditor = new ToolStripMenuItem
            {
                Text = "Editor",
                AutoToolTip = false,
                Image = Resources.edit
            };

            addEditor.Click += new EventHandler(addEditor_Click);
            add.DropDown.Items.Add(addEditor);

            ToolStripMenuItem addSchedule = new ToolStripMenuItem
            {
                Text = "Schedule",
                AutoToolTip = false,
                Image = Resources.schedule
            };

            addSchedule.Click += new EventHandler(addSchedule_Click);
            add.DropDown.Items.Add(addSchedule);

            ToolStripMenuItem addMacroTag = new ToolStripMenuItem
            {
                Text = "Macro Tag",
                AutoToolTip = false,
                Image = Resources.brick
            };

            addMacroTag.Click += new EventHandler(addMacroTag_Click);
            add.DropDown.Items.Add(addMacroTag);

            ToolStripMenuItem addTrigger = new ToolStripMenuItem
            {
                Text = "Trigger",
                AutoToolTip = false,
                Image = Resources.trigger
            };

            addTrigger.Click += new EventHandler(addTrigger_Click);
            add.DropDown.Items.Add(addTrigger);

            ToolStripDropDownButton change = new ToolStripDropDownButton
            {
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                Text = "Change",
                Image = Resources.configure
            };

            ToolStripMenuItem changeScreen = new ToolStripMenuItem
            {
                Text = "Screen",
                AutoToolTip = false,
                Image = Resources.screen
            };

            int autoAdaptIndex = 0;

            foreach (Screen screen in _formScreen.ScreenCollection)
            {
                if (screen.AutoAdapt)
                {
                    screen.AutoAdaptIndex = autoAdaptIndex;
                    autoAdaptIndex++;
                }

                ToolStripMenuItem screenMenuItem = new ToolStripMenuItem
                {
                    Text = screen.Name,
                    Tag = screen
                };

                screenMenuItem.Click += new EventHandler(changeScreen_Click);

                changeScreen.DropDown.Items.Add(screenMenuItem);
            }

            change.DropDown.Items.Add(changeScreen);

            ToolStripMenuItem changeRegion = new ToolStripMenuItem
            {
                Text = "Region",
                AutoToolTip = false,
                Image = Resources.region
            };

            foreach (Region region in _formRegion.RegionCollection)
            {
                ToolStripMenuItem regionMenuItem = new ToolStripMenuItem
                {
                    Text = region.Name,
                    Tag = region
                };

                regionMenuItem.Click += new EventHandler(changeRegion_Click);

                changeRegion.DropDown.Items.Add(regionMenuItem);
            }

            change.DropDown.Items.Add(changeRegion);

            ToolStripMenuItem changeEditor = new ToolStripMenuItem
            {
                Text = "Editor",
                AutoToolTip = false,
                Image = Resources.edit
            };

            foreach (Editor editor in _formEditor.EditorCollection)
            {
                ToolStripMenuItem editorMenuItem = new ToolStripMenuItem
                {
                    Text = editor.Name,
                    Tag = editor
                };

                editorMenuItem.Click += new EventHandler(changeEditor_Click);

                changeEditor.DropDown.Items.Add(editorMenuItem);
            }

            change.DropDown.Items.Add(changeEditor);

            ToolStripMenuItem changeSchedule = new ToolStripMenuItem
            {
                Text = "Schedule",
                AutoToolTip = false,
                Image = Resources.schedule
            };

            foreach (Schedule schedule in _formSchedule.ScheduleCollection)
            {
                ToolStripMenuItem scheduleMenuItem = new ToolStripMenuItem
                {
                    Text = schedule.Name,
                    Tag = schedule
                };

                scheduleMenuItem.Click += new EventHandler(changeSchedule_Click);

                changeSchedule.DropDown.Items.Add(scheduleMenuItem);
            }

            change.DropDown.Items.Add(changeSchedule);

            ToolStripMenuItem changeMacroTag = new ToolStripMenuItem
            {
                Text = "Macro Tag",
                AutoToolTip = false,
                Image = Resources.brick
            };

            foreach (MacroTag macrotag in _formMacroTag.MacroTagCollection)
            {
                ToolStripMenuItem macrotagMenuItem = new ToolStripMenuItem
                {
                    Text = macrotag.Name,
                    Tag = macrotag
                };

                macrotagMenuItem.Click += new EventHandler(changeMacroTag_Click);

                changeMacroTag.DropDown.Items.Add(macrotagMenuItem);
            }

            change.DropDown.Items.Add(changeMacroTag);

            ToolStripMenuItem changeTrigger = new ToolStripMenuItem
            {
                Text = "Trigger",
                AutoToolTip = false,
                Image = Resources.trigger
            };

            foreach (Trigger trigger in _formTrigger.TriggerCollection)
            {
                ToolStripMenuItem triggerMenuItem = new ToolStripMenuItem
                {
                    Text = trigger.Name,
                    Tag = trigger
                };

                triggerMenuItem.Click += new EventHandler(changeTrigger_Click);

                changeTrigger.DropDown.Items.Add(triggerMenuItem);
            }

            change.DropDown.Items.Add(changeTrigger);

            ToolStripButton zoomIn = new ToolStripButton
            {
                Image = Properties.Resources.zoom_in
            };

            ToolStripButton zoomOut = new ToolStripButton
            {
                Image = Properties.Resources.zoom_out
            };

            zoomIn.Click += dashboardZoomIn_Click;
            zoomOut.Click += dashboardZoomOut_Click;

            toolStripDashboard.Items.Add(add);
            toolStripDashboard.Items.Add(new ToolStripSeparator());
            toolStripDashboard.Items.Add(change);
            toolStripDashboard.Items.Add(new ToolStripSeparator());
            toolStripDashboard.Items.Add(zoomIn);
            toolStripDashboard.Items.Add(zoomOut);

            if (Convert.ToBoolean(_config.Settings.Application.GetByKey("AllowUserToConfigureEmailSettings", _config.Settings.DefaultSettings.AllowUserToConfigureEmailSettings).Value))
            {
                toolStripDropDownButtonEmailSettings.Enabled = true;
            }
            else
            {
                toolStripDropDownButtonEmailSettings.Enabled = false;
            }

            if (Convert.ToBoolean(_config.Settings.Application.GetByKey("AllowUserToConfigureFileTransferSettings", _config.Settings.DefaultSettings.AllowUserToConfigureFileTransferSettings).Value))
            {
                toolStripDropDownButtonFileTransferSettings.Enabled = true;
            }
            else
            {
                toolStripDropDownButtonFileTransferSettings.Enabled = false;
            }

            flowLayoutPanel = new FlowLayoutPanel
            {
                Name = "flowLayoutPanel",
                AutoScroll = true,
                BackColor = Color.Black,
                Location = new Point(4, 29),
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left
            };

            int i = 1;

            foreach (Screen screen in _formScreen.ScreenCollection)
            {
                GroupBox groupBox = new GroupBox
                {
                    Name = "groupBox" + i,
                    Tag = screen,
                    Text = screen.AutoAdapt ? "[" + (screen.AutoAdaptIndex + 1).ToString() + "] " + screen.Name : screen.Name,
                    BackColor = Color.LightCyan,
                    ForeColor = Color.Black,
                    Location = new Point(0, 0),
                    Size = new Size(dashboardGroupBoxSize, dashboardGroupBoxSize),
                };

                PictureBox pictureBox = new PictureBox
                {
                    Name = "pictureBox" + i,
                    BackColor = Color.Black,
                    BorderStyle = BorderStyle.Fixed3D,
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = i
                };

                pictureBox.DoubleClick += new EventHandler(dashboardPictureBox_DoubleClick);

                groupBox.Controls.Add(pictureBox);
                flowLayoutPanel.Controls.Add(groupBox);

                i++;
            }

            foreach (Region region in _formRegion.RegionCollection)
            {
                GroupBox groupBox = new GroupBox
                {
                    Name = "groupBox" + i,
                    Tag = region,
                    Text = region.Name,
                    BackColor = Color.LightPink,
                    ForeColor = Color.Black,
                    Location = new Point(0, 0),
                    Size = new Size(dashboardGroupBoxSize, dashboardGroupBoxSize),
                };

                PictureBox pictureBox = new PictureBox
                {
                    Name = "pictureBox" + i,
                    BackColor = Color.Black,
                    BorderStyle = BorderStyle.Fixed3D,
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = i
                };

                pictureBox.DoubleClick += new EventHandler(dashboardPictureBox_DoubleClick);

                groupBox.Controls.Add(pictureBox);
                flowLayoutPanel.Controls.Add(groupBox);

                i++;
            }

            TabPage tabPageDashboard = new TabPage
            {
                Name = "tabPageDashboard",
                Text = "Dashboard"
            };

            tabPageDashboard.Controls.Add(toolStripDashboard);
            tabPageDashboard.Controls.Add(flowLayoutPanel);

            tabControlViews.Controls.Add(tabPageDashboard);

            foreach (Screen screen in _formScreen.ScreenCollection)
            {
                ToolStrip toolStripScreen = new ToolStrip
                {
                    Name = screen.Name + "toolStrip",
                    GripStyle = ToolStripGripStyle.Hidden,
                    Tag = screen
                };

                toolStripScreen = BuildViewTabPageToolStripItems(toolStripScreen, screen.Name);

                PictureBox pictureBoxScreen = new PictureBox
                {
                    Name = screen.Name + "pictureBox",
                    BackColor = Color.Black,
                    Location = new Point(4, 29),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    ContextMenuStrip = contextMenuStripScreenshot,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left
                };

                TabPage tabPageScreen = new TabPage
                {
                    Name = screen.Name,
                    Text = screen.AutoAdapt ? "[" + (screen.AutoAdaptIndex + 1).ToString() + "] " + screen.Name : screen.Name,
                    Tag = screen
                };

                tabPageScreen.Controls.Add(toolStripScreen);
                tabPageScreen.Controls.Add(pictureBoxScreen);

                pictureBoxScreen.Width = (tabPageScreen.Width - 10);
                pictureBoxScreen.Height = (tabPageScreen.Height - 30);

                tabControlViews.Controls.Add(tabPageScreen);
            }

            foreach (Region region in _formRegion.RegionCollection)
            {
                ToolStrip toolStripRegion = new ToolStrip
                {
                    Name = region.Name + "toolStrip",
                    GripStyle = ToolStripGripStyle.Hidden,
                    Tag = region
                };

                toolStripRegion = BuildViewTabPageToolStripItems(toolStripRegion, region.Name);

                PictureBox pictureBoxRegion = new PictureBox
                {
                    Name = region.Name + "pictureBox",
                    BackColor = Color.Black,
                    Location = new Point(4, 29),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    ContextMenuStrip = contextMenuStripScreenshot,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left
                };

                TabPage tabPageRegion = new TabPage
                {
                    Name = region.Name,
                    Text = region.Name,
                    Tag = region
                };

                tabPageRegion.Controls.Add(toolStripRegion);
                tabPageRegion.Controls.Add(pictureBoxRegion);

                pictureBoxRegion.Width = (tabPageRegion.Width - 10);
                pictureBoxRegion.Height = (tabPageRegion.Height - 30);

                tabControlViews.Controls.Add(tabPageRegion);
            }

            ShowScreenshotBySlideIndex();
        }

        private ToolStrip BuildViewTabPageToolStripItems(ToolStrip toolStrip, string name)
        {
            Screenshot selectedScreenshot = new Screenshot();

            ToolStripSplitButton toolStripSplitButtonConfigure = new ToolStripSplitButton
            {
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Change or remove this component",
                Image = Resources.configure
            };

            if (toolStrip.Tag is Screen screen)
            {
                if (_slideShow.SelectedSlide != null)
                {
                    selectedScreenshot = _screenshotCollection.GetScreenshot(_slideShow.SelectedSlide.Name, screen.ViewId);
                }

                toolStripSplitButtonConfigure.Text = "Configure";

                toolStripSplitButtonConfigure.Tag = toolStrip.Tag;

                toolStripSplitButtonConfigure.ButtonClick += changeScreen_Click;

                ToolStripMenuItem toolStripMenuItemChangeScreen = new ToolStripMenuItem
                {
                    Text = "Change Screen",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemChangeScreen.Click += new EventHandler(changeScreen_Click);

                toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemChangeScreen);

                ToolStripMenuItem toolStripMenuItemRemoveScreen = new ToolStripMenuItem
                {
                    Text = "Remove Screen",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemRemoveScreen.Click += new EventHandler(removeScreen_Click);

                toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemRemoveScreen);
            }

            if (toolStrip.Tag is Region region)
            {
                if (_slideShow.SelectedSlide != null)
                {
                    selectedScreenshot = _screenshotCollection.GetScreenshot(_slideShow.SelectedSlide.Name, region.ViewId);
                }

                toolStripSplitButtonConfigure.Text = "Configure";

                toolStripSplitButtonConfigure.Tag = toolStrip.Tag;

                toolStripSplitButtonConfigure.ButtonClick += changeRegion_Click;

                ToolStripMenuItem toolStripMenuItemRegion = new ToolStripMenuItem
                {
                    Text = "Change Region",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemRegion.Click += new EventHandler(changeRegion_Click);

                toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemRegion);

                ToolStripMenuItem toolStripMenuItemRemoveRegion = new ToolStripMenuItem
                {
                    Text = "Remove Region",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemRemoveRegion.Click += new EventHandler(removeRegion_Click);

                toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemRemoveRegion);
            }

            ToolStripSplitButton toolStripSplitButtonEdit = new ToolStripSplitButton
            {
                Name = name + "toolStripSplitButtonEdit",
                Text = "Edit",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Edit the screenshot with an application or script",
                Image = Resources.edit
            };

            toolStripSplitButtonEdit.ButtonClick += runEditor_Click;

            toolStripSplitButtonEdit.DropDown.Items.Add("Add Editor", null, addEditor_Click);

            ToolStripButton toolStripButtonEncryptDecrypt = new ToolStripButton
            {
                Name = name + "toolStripButtonEncryptDecrypt",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false
            };

            toolStripButtonEncryptDecrypt.Click += new EventHandler(encryptDecryptScreenshot_Click);

            if (toolStrip.Tag is Region regionEncryptDecrypt)
            {
                if (regionEncryptDecrypt.Encrypt)
                {
                    toolStripButtonEncryptDecrypt.Text = "Decrypt";
                    toolStripButtonEncryptDecrypt.Image = Resources.unlock;
                }
                else
                {
                    toolStripButtonEncryptDecrypt.Text = "Encrypt";
                    toolStripButtonEncryptDecrypt.Image = Resources._lock;
                }
            }

            if (toolStrip.Tag is Screen screenEncryptDecrypt)
            {
                if (screenEncryptDecrypt.Encrypt)
                {
                    toolStripButtonEncryptDecrypt.Text = "Decrypt";
                    toolStripButtonEncryptDecrypt.Image = Resources.unlock;
                }
                else
                {
                    toolStripButtonEncryptDecrypt.Text = "Encrypt";
                    toolStripButtonEncryptDecrypt.Image = Resources._lock;
                }
            }

            foreach (Editor editor in _formEditor.EditorCollection)
            {
                if (editor != null && _fileSystem.FileExists(editor.Application))
                {
                    toolStripSplitButtonEdit.DropDown.Items.Add(editor.Name, Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), runEditor_Click);
                }
            }

            ToolStripButton toolStripButtonEmail = new ToolStripButton
            {
                Text = "Email",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                Image = Resources.email
            };

            toolStripButtonEmail.Click += new EventHandler(emailScreenshot_Click);

            string emailServerHost = Settings.SMTP.GetByKey("EmailServerHost", _config.Settings.DefaultSettings.EmailServerHost).Value.ToString();
            int.TryParse(Settings.SMTP.GetByKey("EmailServerPort", _config.Settings.DefaultSettings.EmailServerPort).Value.ToString(), out int emailServerPort);
            string emailClientUsername = Settings.SMTP.GetByKey("EmailClientUsername", _config.Settings.DefaultSettings.EmailClientUsername).Value.ToString();
            string emailClientPassword = Settings.SMTP.GetByKey("EmailClientPassword", _config.Settings.DefaultSettings.EmailClientPassword).Value.ToString();
            string emailMessageFrom = Settings.SMTP.GetByKey("EmailMessageFrom", _config.Settings.DefaultSettings.EmailMessageFrom).Value.ToString();
            string emailMessageTo = Settings.SMTP.GetByKey("EmailMessageTo", _config.Settings.DefaultSettings.EmailMessageTo).Value.ToString();

            if (string.IsNullOrEmpty(emailServerHost) ||
                emailServerPort <= 0 ||
                string.IsNullOrEmpty(emailClientUsername) ||
                string.IsNullOrEmpty(emailClientPassword) ||
                string.IsNullOrEmpty(emailMessageFrom) ||
                string.IsNullOrEmpty(emailMessageTo))
            {
                toolStripButtonEmail.ToolTipText = "Email settings have not been configured for " + _config.Settings.ApplicationName + " to email screenshots";
                toolStripButtonEmail.Enabled = false;
            }
            else
            {
                toolStripButtonEmail.ToolTipText = "Email this screenshot using the configured Email settings";
                toolStripButtonEmail.Enabled = true;
            }

            ToolStripButton toolStripButtonFileTransfer = new ToolStripButton
            {
                Text = "File Transfer",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                Image = Resources.file_transfer
            };

            toolStripButtonFileTransfer.Click += new EventHandler(fileTransferScreenshot_Click);

            string fileTransferServerHost = Settings.SFTP.GetByKey("FileTransferServerHost", _config.Settings.DefaultSettings.FileTransferServerHost).Value.ToString();
            int.TryParse(Settings.SFTP.GetByKey("FileTransferServerPort", _config.Settings.DefaultSettings.FileTransferServerPort).Value.ToString(), out int fileTransferServerPort);
            string fileTransferClientUsername = Settings.SFTP.GetByKey("FileTransferClientUsername", _config.Settings.DefaultSettings.FileTransferClientUsername).Value.ToString();
            string fileTransferClientPassword = Settings.SFTP.GetByKey("FileTransferClientPassword", _config.Settings.DefaultSettings.FileTransferClientPassword).Value.ToString();

            if (string.IsNullOrEmpty(fileTransferServerHost) ||
                fileTransferServerPort <= 0 ||
                string.IsNullOrEmpty(fileTransferClientUsername) ||
                string.IsNullOrEmpty(fileTransferClientPassword))
            {
                toolStripButtonFileTransfer.ToolTipText = "File Transfer settings have not been configured for " + _config.Settings.ApplicationName + " to send screenshots to a file server";
                toolStripButtonFileTransfer.Enabled = false;
            }
            else
            {
                toolStripButtonFileTransfer.ToolTipText = "Upload this screenshot to the specified file server using the configured File Transfer settings";
                toolStripButtonFileTransfer.Enabled = true;
            }

            ToolStripButton toolStripButtonScreenshotMetadata = new ToolStripButton
            {
                Text = "Metadata",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Show screenshot metadata (such as width and height)",
                Image = Resources.properties
            };

            toolStripButtonScreenshotMetadata.Click += new EventHandler(screenshotMetadata_Click);

            ToolStripItem toolStripLabelFilename = new ToolStripLabel
            {
                Name = name + "toolStripLabelFilename",
                Text = "File:",
                Alignment = ToolStripItemAlignment.Right,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            ToolStripItem toolStripTextBoxFilename = new ToolStripTextBox
            {
                Name = name + "toolStripTextBoxFilename",
                Alignment = ToolStripItemAlignment.Right,
                AutoSize = false,
                ReadOnly = true,
                Width = 200,
                BackColor = Color.LightYellow,
                Text = string.Empty
            };

            ToolStripItem toolstripButtonOpenFolder = new ToolStripButton
            {
                Name = name + "toolstripButtonOpenFolder",
                Image = Resources.openfolder,
                Alignment = ToolStripItemAlignment.Right,
                AutoToolTip = false,
                ToolTipText = "Show screenshot location",
                DisplayStyle = ToolStripItemDisplayStyle.Image
            };

            toolstripButtonOpenFolder.Click += new EventHandler(showScreenshotLocation_Click);

            toolStrip.Items.Add(toolStripSplitButtonConfigure);
            toolStrip.Items.Add(new ToolStripSeparator { Alignment = ToolStripItemAlignment.Left });
            toolStrip.Items.Add(toolStripSplitButtonEdit);
            toolStrip.Items.Add(toolStripButtonEncryptDecrypt);
            toolStrip.Items.Add(toolStripButtonEmail);
            toolStrip.Items.Add(toolStripButtonFileTransfer);
            toolStrip.Items.Add(toolStripButtonScreenshotMetadata);
            toolStrip.Items.Add(toolstripButtonOpenFolder);
            toolStrip.Items.Add(toolStripTextBoxFilename);
            toolStrip.Items.Add(toolStripLabelFilename);
            toolStrip.Items.Add(new ToolStripSeparator { Alignment = ToolStripItemAlignment.Right });

            return toolStrip;
        }

        private void dashboardPictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox selectedPictureBox = (PictureBox)sender;

            tabControlViews.SelectedIndex = (int)selectedPictureBox.Tag;
        }

        private void dashboardZoomIn_Click(object sender, EventArgs e)
        {
            ResizeGroupBoxes(zoomIn: true);
        }

        private void dashboardZoomOut_Click(object sender, EventArgs e)
        {
            ResizeGroupBoxes(zoomIn: false);
        }

        /// <summary>
        /// Resizes the group boxes on the dashboard.
        /// </summary>
        /// <param name="zoomIn">Determines if we increase the size for a "zoom in" or decrease the size for a "zoom out".</param>
        private void ResizeGroupBoxes(bool zoomIn)
        {
            int sizeStep = 50;

            if (flowLayoutPanel != null)
            {
                if (zoomIn)
                {
                    dashboardGroupBoxSize += sizeStep;
                }
                else
                {
                    dashboardGroupBoxSize -= sizeStep;
                }

                if (dashboardGroupBoxSize < MIN_DASHBOARD_GROUPBOX_SIZE)
                {
                    dashboardGroupBoxSize = MIN_DASHBOARD_GROUPBOX_SIZE;
                }

                if (dashboardGroupBoxSize > MAX_DASHBOARD_GROUPBOX_SIZE)
                {
                    dashboardGroupBoxSize = MAX_DASHBOARD_GROUPBOX_SIZE;
                }

                int maxGroupBoxCount = _formScreen.ScreenCollection.Count + _formRegion.RegionCollection.Count;

                for (int i = 1; i <= maxGroupBoxCount; i++)
                {
                    Control[] groupBoxes = flowLayoutPanel.Controls.Find("groupBox" + i, true);

                    foreach (GroupBox groupBox in groupBoxes)
                    {
                        groupBox.Width = dashboardGroupBoxSize;
                        groupBox.Height = dashboardGroupBoxSize;
                    }
                }
            }
        }
    }
}
