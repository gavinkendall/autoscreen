//-----------------------------------------------------------------------
// <copyright file="FormMain-Filters.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for handling filters.</summary>
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
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        private void comboBoxFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save the selected index of Filter Type.
            _config.Settings.User.SetValueByKey("FilterType", comboBoxFilterType.SelectedIndex);

            SearchFilterValues();

            if (!string.IsNullOrEmpty(comboBoxFilterType.Text))
            {
                comboBoxFilterValue.Enabled = true;

                buttonRefreshFilterValues.Enabled = true;
            }
            else
            {
                SearchDates();
                ShowScreenshots();

                comboBoxFilterValue.Text = string.Empty;
                comboBoxFilterValue.Enabled = false;

                buttonRefreshFilterValues.Enabled = false;
            }
        }

        private void comboBoxFilterValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make sure the filter search thread is not busy when we save the Filter Value selected index.
            // This avoids problems when the DataSource for comboBoxFilterValue is set (which triggers this method to be called).
            if (!runFilterSearchThread.IsBusy)
            {
                // Save the selected index of Filter Value.
                _config.Settings.User.SetValueByKey("FilterValue", comboBoxFilterValue.SelectedIndex);
            }

            SearchDates();
            ShowScreenshots();
        }

        private void DoWork_runFilterSearchThread(object sender, DoWorkEventArgs e)
        {
            RunFilterSearch(e);
        }

        private void RunFilterSearch(DoWorkEventArgs e)
        {
            if (comboBoxFilterValue.InvokeRequired)
            {
                comboBoxFilterValue.Invoke(new RunTitleSearchDelegate(RunFilterSearch), new object[] { e });
            }
            else
            {
                if (comboBoxFilterType.SelectedItem != null && !string.IsNullOrEmpty(comboBoxFilterType.Text))
                {
                    List<string> filterValueList = new List<string>();
                    filterValueList = _screenshotCollection.GetFilterValueList(comboBoxFilterType.Text);
                    filterValueList.Add(string.Empty);
                    filterValueList.Sort();

                    comboBoxFilterValue.DataSource = filterValueList;

                    int selectedFilterValueIndex = Convert.ToInt32(_config.Settings.User.GetByKey("FilterValue", 0).Value);

                    if (comboBoxFilterValue.Items.Count > 0)
                    {
                        if (selectedFilterValueIndex == -1)
                        {
                            selectedFilterValueIndex = 0;
                        }

                        if (selectedFilterValueIndex >= comboBoxFilterValue.Items.Count)
                        {
                            selectedFilterValueIndex = comboBoxFilterValue.Items.Count - 1;
                        }

                        comboBoxFilterValue.SelectedIndex = selectedFilterValueIndex;
                    }
                }
            }
        }

        private void SearchFilterValues()
        {
            comboBoxFilterValue.BeginUpdate();

            if (runFilterSearchThread == null)
            {
                runFilterSearchThread = new BackgroundWorker
                {
                    WorkerReportsProgress = false,
                    WorkerSupportsCancellation = true
                };

                runFilterSearchThread.DoWork += new DoWorkEventHandler(DoWork_runFilterSearchThread);
            }

            if (!runFilterSearchThread.IsBusy)
            {
                runFilterSearchThread.RunWorkerAsync();
            }

            comboBoxFilterValue.EndUpdate();
        }
    }
}