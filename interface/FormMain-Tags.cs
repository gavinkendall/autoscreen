using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Shows the "Add Tag" window to enable the user to add a chosen Tag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addTag(object sender, EventArgs e)
        {
            formTag.TagObject = null;

            formTag.ShowDialog(this);

            if (formTag.DialogResult == DialogResult.OK)
            {
                BuildTagsModule();

                formTag.TagCollection.SaveToXmlFile();
            }
        }

        /// <summary>
        /// Removes the selected Tags from the Tags tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_removeSelectedTags(object sender, EventArgs e)
        {
            int countBeforeRemoval = formTag.TagCollection.Count;

            foreach (Control control in tabPageTags.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Tag trigger = formTag.TagCollection.Get((Tag)checkBox.Tag);
                        formTag.TagCollection.Remove(trigger);
                    }
                }
            }

            if (countBeforeRemoval > formTag.TagCollection.Count)
            {
                BuildTagsModule();

                formTag.TagCollection.SaveToXmlFile();
            }
        }

        /// <summary>
        /// Shows the "Change Tag" window to enable the user to edit a chosen Tag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_changeTag(object sender, EventArgs e)
        {
            Button buttonSelected = (Button)sender;

            if (buttonSelected.Tag != null)
            {
                formTag.TagObject = (Tag)buttonSelected.Tag;

                formTag.ShowDialog(this);

                if (formTag.DialogResult == DialogResult.OK)
                {
                    BuildTagsModule();

                    formTag.TagCollection.SaveToXmlFile();
                }
            }
        }
    }
}
