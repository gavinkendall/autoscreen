using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoScreenCapture.Properties;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        private void BuildViewTabPages()
        {
            tabControlViews.Controls.Clear();

            foreach (Screen screen in formScreen.ScreenCollection)
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
                    ContextMenuStrip = contextMenuStripScreenshotPreview,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left,
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

            foreach (Region region in formRegion.RegionCollection)
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
                    ContextMenuStrip = contextMenuStripScreenshotPreview,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left,
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

            toolStripButtonEmail.Click += new EventHandler(Click_emailScreenshot);

            toolStripSplitButtonEdit.DropDown.Items.Add("Add New Editor ...", null, Click_addEditorToolStripMenuItem);

            foreach (Editor editor in formEditor.EditorCollection)
            {
                if (editor != null && File.Exists(editor.Application))
                {
                    toolStripSplitButtonEdit.DropDown.Items.Add(editor.Name, Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), Click_runEditor);
                }
            }

            ToolStripSplitButton toolStripSplitButtonConfigure = new ToolStripSplitButton
            {
                Text = "Configure",
                Alignment = ToolStripItemAlignment.Right,
                AutoToolTip = false,
                Image = Resources.options
            };

            toolStripSplitButtonConfigure.DropDown.Items.Add("Add New Screen", null, Click_addScreen);
            toolStripSplitButtonConfigure.DropDown.Items.Add("Add New Region", null, Click_addRegion);

            toolStripSplitButtonConfigure.DropDown.Items.Add(new ToolStripSeparator());

            if (toolStrip.Tag is Screen)
            {
                ToolStripMenuItem toolStripMenuItemChangeScreen = new ToolStripMenuItem
                {
                    Text = "Change Screen",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemChangeScreen.Click += new EventHandler(Click_changeScreen);

                toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemChangeScreen);

                ToolStripMenuItem toolStripMenuItemRemoveScreen = new ToolStripMenuItem
                {
                    Text = "Remove Screen",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemRemoveScreen.Click += new EventHandler(Click_removeScreen);

                toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemRemoveScreen);
            }

            if (toolStrip.Tag is Region)
            {
                ToolStripMenuItem toolStripMenuItemRegion = new ToolStripMenuItem
                {
                    Text = "Change Region",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemRegion.Click += new EventHandler(Click_changeRegion);

                toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemRegion);

                ToolStripMenuItem toolStripMenuItemRemoveRegion = new ToolStripMenuItem
                {
                    Text = "Remove Region",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemRemoveRegion.Click += new EventHandler(Click_removeRegion);

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

            toolstripButtonOpenFolder.Click += new EventHandler(Click_toolStripMenuItemShowScreenshotLocation);

            toolStrip.Items.Add(toolStripButtonEmail);
            toolStrip.Items.Add(toolStripSplitButtonEdit);
            toolStrip.Items.Add(toolStripSplitButtonConfigure);
            toolStrip.Items.Add(new ToolStripSeparator { Alignment = ToolStripItemAlignment.Right });
            toolStrip.Items.Add(toolstripButtonOpenFolder);
            toolStrip.Items.Add(toolstripTextBoxFilename);
            toolStrip.Items.Add(toolStripLabelFilename);
            toolStrip.Items.Add(new ToolStripSeparator { Alignment = ToolStripItemAlignment.Right });

            return toolStrip;
        }
    }
}
