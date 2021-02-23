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
            ToolStripSplitButton toolStripSplitButtonEdit = new ToolStripSplitButton
            {
                Text = "Edit",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                Image = Resources.edit
            };

            ToolStripButton toolStripButtonEmail = new ToolStripButton
            {
                Text = "Email",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                Image = Resources.email
            };

            toolStripButtonEmail.Click += new EventHandler(emailScreenshot_Click);

            // Check to see if Email (SMTP) is configured.
            string host = _config.Settings.Application.GetByKey("EmailServerHost", _config.Settings.DefaultSettings.EmailServerHost).Value.ToString();

            string username = _config.Settings.Application.GetByKey("EmailClientUsername", _config.Settings.DefaultSettings.EmailClientUsername).Value.ToString();
            string password = _config.Settings.Application.GetByKey("EmailClientPassword", _config.Settings.DefaultSettings.EmailClientPassword).Value.ToString();

            string from = _config.Settings.Application.GetByKey("EmailMessageFrom", _config.Settings.DefaultSettings.EmailMessageFrom).Value.ToString();
            string to = _config.Settings.Application.GetByKey("EmailMessageTo", _config.Settings.DefaultSettings.EmailMessageTo).Value.ToString();

            if (string.IsNullOrEmpty(host) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(@from) ||
                string.IsNullOrEmpty(to))
            {
                toolStripButtonEmail.ToolTipText = "SMTP settings have not been configured for " + _config.Settings.ApplicationName + " to email screenshots";
                toolStripButtonEmail.Enabled = false;
            }
            else
            {
                toolStripButtonEmail.ToolTipText = "Email this screenshot using the configured SMTP settings";
                toolStripButtonEmail.Enabled = true;
            }

            toolStripSplitButtonEdit.DropDown.Items.Add("Add New Editor ...", null, addEditor_Click);

            foreach (Editor editor in _formEditor.EditorCollection)
            {
                if (editor != null && _fileSystem.FileExists(editor.Application))
                {
                    toolStripSplitButtonEdit.DropDown.Items.Add(editor.Name, Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), runEditor_Click);
                }
            }

            ToolStripSplitButton toolStripSplitButtonConfigure = new ToolStripSplitButton
            {
                Text = "Configure",
                Alignment = ToolStripItemAlignment.Right,
                AutoToolTip = false,
                Image = Resources.configure
            };

            toolStripSplitButtonConfigure.DropDown.Items.Add("Add New Screen", null, addScreen_Click);
            toolStripSplitButtonConfigure.DropDown.Items.Add("Add New Region", null, addRegion_Click);

            toolStripSplitButtonConfigure.DropDown.Items.Add(new ToolStripSeparator());

            if (toolStrip.Tag is Screen)
            {
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

            ToolStripItem toolStripLabelFilename = new ToolStripLabel
            {
                Text = "Filename:",
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
                ToolTipText = "Show Screenshot Location",
                DisplayStyle = ToolStripItemDisplayStyle.Image
            };

            toolstripButtonOpenFolder.Click += new EventHandler(showScreenshotLocation_Click);

            toolStrip.Items.Add(toolStripSplitButtonEdit);
            toolStrip.Items.Add(toolStripButtonEmail);
            toolStrip.Items.Add(toolStripSplitButtonConfigure);
            toolStrip.Items.Add(new ToolStripSeparator { Alignment = ToolStripItemAlignment.Right });
            toolStrip.Items.Add(toolstripButtonOpenFolder);
            toolStrip.Items.Add(toolstripTextBoxFilename);
            toolStrip.Items.Add(toolStripLabelFilename);
            toolStrip.Items.Add(new ToolStripSeparator { Alignment = ToolStripItemAlignment.Right });

            return toolStrip;
        }

        private void dashboardPictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox selectedPictureBox = (PictureBox)sender;

            tabControlViews.SelectedIndex = (int)selectedPictureBox.Tag;
        }
    }
}
