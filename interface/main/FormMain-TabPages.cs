//-----------------------------------------------------------------------
// <copyright file="FormMain-TabPages.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
        private void BuildViewTabPages()
        {
            tabControlViews.Controls.Clear();

            int dashboardBoxSize = 220;

            ToolStrip toolStripDashboard = new ToolStrip
            {
                Name = "toolStripDasboard",
                GripStyle = ToolStripGripStyle.Hidden
            };

            ToolStripButton toolStripButtonAddScreen = new ToolStripButton
            {
                Text = "Screen",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Add a new Screen",
                Image = Resources.screen
            };

            toolStripButtonAddScreen.Click += new EventHandler(addScreen_Click);

            ToolStripButton toolStripButtonAddRegion = new ToolStripButton
            {
                Text = "Region",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Add a new Region",
                Image = Resources.region
            };

            toolStripButtonAddRegion.Click += new EventHandler(addRegion_Click);

            ToolStripButton toolStripButtonAddEditor = new ToolStripButton
            {
                Text = "Editor",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Add a new Editor",
                Image = Resources.edit
            };

            toolStripButtonAddEditor.Click += new EventHandler(addEditor_Click);

            ToolStripButton toolStripButtonAddSchedule = new ToolStripButton
            {
                Text = "Schedule",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Add a new Schedule",
                Image = Resources.schedule
            };

            toolStripButtonAddSchedule.Click += new EventHandler(addSchedule_Click);

            ToolStripButton toolStripButtonAddMacroTag = new ToolStripButton
            {
                Text = "Macro Tag",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Add a new Macro Tag",
                Image = Resources.brick
            };

            toolStripButtonAddMacroTag.Click += new EventHandler(addMacroTag_Click);

            ToolStripButton toolStripButtonAddTrigger = new ToolStripButton
            {
                Text = "Trigger",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Add a new Trigger",
                Image = Resources.trigger
            };

            toolStripButtonAddTrigger.Click += new EventHandler(addTrigger_Click);

            ToolStripButton toolStripButtonEmailSettings = new ToolStripButton
            {
                Text = "Email Settings",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Configure email (SMTP) settings",
                Image = Resources.email
            };

            toolStripButtonEmailSettings.Click += new EventHandler(toolStripMenuItemEmailSettings_Click);

            ToolStripButton toolStripButtonFileTransferSettings = new ToolStripButton
            {
                Text = "File Transfer Settings",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Configure file transfer (SFTP) settings",
                Image = Resources.file_transfer
            };

            toolStripButtonFileTransferSettings.Click += new EventHandler(toolStripMenuItemFileTransferSettings_Click);

            toolStripDashboard.Items.Add(toolStripButtonAddScreen);
            toolStripDashboard.Items.Add(toolStripButtonAddRegion);
            toolStripDashboard.Items.Add(toolStripButtonAddEditor);
            toolStripDashboard.Items.Add(toolStripButtonAddSchedule);
            toolStripDashboard.Items.Add(toolStripButtonAddMacroTag);
            toolStripDashboard.Items.Add(toolStripButtonAddTrigger);

            if (Convert.ToBoolean(Settings.Application.GetByKey("AllowUserToConfigureEmailSettings", DefaultSettings.AllowUserToConfigureEmailSettings).Value))
            {
                toolStripDashboard.Items.Add(toolStripButtonEmailSettings);
            }

            if (Convert.ToBoolean(Settings.Application.GetByKey("AllowUserToConfigureFileTransferSettings", DefaultSettings.AllowUserToConfigureFileTransferSettings).Value))
            {
                toolStripDashboard.Items.Add(toolStripButtonFileTransferSettings);
            }

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
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
                    Tag = screen,
                    Text = screen.Name,
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Location = new Point(0, 0),
                    Size = new Size(dashboardBoxSize, dashboardBoxSize),
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
                    Tag = region,
                    Text = region.Name,
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Location = new Point(0, 0),
                    Size = new Size(dashboardBoxSize, dashboardBoxSize),
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
                    Text = screen.Name,
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
            ToolStripSplitButton toolStripSplitButtonConfigure = new ToolStripSplitButton
            {
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Change this component and other settings",
                Image = Resources.configure
            };

            if (toolStrip.Tag is Screen)
            {
                toolStripSplitButtonConfigure.Text = "Configure Screen";

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

            if (toolStrip.Tag is Region)
            {
                toolStripSplitButtonConfigure.Text = "Configure Region";

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

            toolStripSplitButtonConfigure.DropDown.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem toolStripMenuItemAdd = new ToolStripMenuItem
            {
                Text = "Add"
            };

            toolStripMenuItemAdd.DropDown.Items.Add("Screen", Resources.screen, addScreen_Click);
            toolStripMenuItemAdd.DropDown.Items.Add("Region", Resources.region, addRegion_Click);
            toolStripMenuItemAdd.DropDown.Items.Add("Editor", Resources.edit, addEditor_Click);
            toolStripMenuItemAdd.DropDown.Items.Add("Schedule", Resources.schedule, addSchedule_Click);
            toolStripMenuItemAdd.DropDown.Items.Add("Macro Tag", Resources.brick, addMacroTag_Click);
            toolStripMenuItemAdd.DropDown.Items.Add("Trigger", Resources.trigger, addTrigger_Click);

            toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemAdd);

            toolStripSplitButtonConfigure.DropDown.Items.Add(new ToolStripSeparator());

            if (Convert.ToBoolean(Settings.Application.GetByKey("AllowUserToConfigureEmailSettings", DefaultSettings.AllowUserToConfigureEmailSettings).Value))
            {
                toolStripSplitButtonConfigure.DropDown.Items.Add("Email Settings", Resources.email, toolStripMenuItemEmailSettings_Click);
            }

            if (Convert.ToBoolean(Settings.Application.GetByKey("AllowUserToConfigureFileTransferSettings", DefaultSettings.AllowUserToConfigureFileTransferSettings).Value))
            {
                toolStripSplitButtonConfigure.DropDown.Items.Add("File Transfer Settings", Resources.file_transfer, toolStripMenuItemFileTransferSettings_Click);
            }

            ToolStripSplitButton toolStripSplitButtonEdit = new ToolStripSplitButton
            {
                Text = "Edit Screenshot",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                ToolTipText = "Edit the screenshot with an application or script",
                Image = Resources.edit
            };

            toolStripSplitButtonEdit.DropDown.Items.Add("Add Editor", null, addEditor_Click);

            foreach (Editor editor in _formEditor.EditorCollection)
            {
                if (editor != null && FileSystem.FileExists(editor.Application))
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

            string emailServerHost = Settings.SMTP.GetByKey("EmailServerHost", DefaultSettings.EmailServerHost).Value.ToString();
            int.TryParse(Settings.SMTP.GetByKey("EmailServerPort", DefaultSettings.EmailServerPort).Value.ToString(), out int emailServerPort);
            string emailClientUsername = Settings.SMTP.GetByKey("EmailClientUsername", DefaultSettings.EmailClientUsername).Value.ToString();
            string emailClientPassword = Settings.SMTP.GetByKey("EmailClientPassword", DefaultSettings.EmailClientPassword).Value.ToString();
            string emailMessageFrom = Settings.SMTP.GetByKey("EmailMessageFrom", DefaultSettings.EmailMessageFrom).Value.ToString();
            string emailMessageTo = Settings.SMTP.GetByKey("EmailMessageTo", DefaultSettings.EmailMessageTo).Value.ToString();

            if (string.IsNullOrEmpty(emailServerHost) ||
                emailServerPort <= 0 ||
                string.IsNullOrEmpty(emailClientUsername) ||
                string.IsNullOrEmpty(emailClientPassword) ||
                string.IsNullOrEmpty(emailMessageFrom) ||
                string.IsNullOrEmpty(emailMessageTo))
            {
                toolStripButtonEmail.ToolTipText = "Email settings have not been configured for " + Settings.ApplicationName + " to email screenshots";
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

            string fileTransferServerHost = Settings.SFTP.GetByKey("FileTransferServerHost", DefaultSettings.FileTransferServerHost).Value.ToString();
            int.TryParse(Settings.SFTP.GetByKey("FileTransferServerPort", DefaultSettings.FileTransferServerPort).Value.ToString(), out int fileTransferServerPort);
            string fileTransferClientUsername = Settings.SFTP.GetByKey("FileTransferClientUsername", DefaultSettings.FileTransferClientUsername).Value.ToString();
            string fileTransferClientPassword = Settings.SFTP.GetByKey("FileTransferClientPassword", DefaultSettings.FileTransferClientPassword).Value.ToString();

            if (string.IsNullOrEmpty(fileTransferServerHost) ||
                fileTransferServerPort <= 0 ||
                string.IsNullOrEmpty(fileTransferClientUsername) ||
                string.IsNullOrEmpty(fileTransferClientPassword))
            {
                toolStripButtonFileTransfer.ToolTipText = "File Transfer settings have not been configured for " + Settings.ApplicationName + " to send screenshots to a file server";
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
                Text = "File:",
                Alignment = ToolStripItemAlignment.Right,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            ToolStripItem toolstripTextBoxFilename = new ToolStripTextBox
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
            toolStrip.Items.Add(toolStripButtonEmail);
            toolStrip.Items.Add(toolStripButtonFileTransfer);
            toolStrip.Items.Add(toolStripButtonScreenshotMetadata);
            toolStrip.Items.Add(toolstripButtonOpenFolder);
            toolStrip.Items.Add(toolstripTextBoxFilename);
            toolStrip.Items.Add(toolStripLabelFilename);

            return toolStrip;
        }

        private void dashboardPictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox selectedPictureBox = (PictureBox)sender;

            tabControlViews.SelectedIndex = (int)selectedPictureBox.Tag;
        }
    }
}
