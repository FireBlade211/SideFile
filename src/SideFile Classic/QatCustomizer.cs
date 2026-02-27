using SideFile.Classic.Helpers;
using System.Data;
using System.Diagnostics;
using WinForms.Ribbon;

namespace SideFile.Classic
{
    public partial class QatCustomizer : Form
    {
        private RibbonItems _ri;
        private List<RibbonCustomizerItem> _avItems = [];
        private List<RibbonCustomizerItem> _pinItems = [];

        public QatCustomizer(RibbonItems ri)
        {
            InitializeComponent();
            _ri = ri;
            
            foreach (var prop in ri.GetType().GetProperties())
            {
                if (prop.GetValue(ri) is IRibbonControl ctrl && (ctrl is RibbonButton or RibbonDropDownButton or RibbonSplitButton
                    or RibbonCheckBox or RibbonGroup || (OperatingSystem.IsWindowsVersionAtLeast(6, 2) && (ctrl is RibbonComboBox
                    or RibbonDropDownGallery or RibbonSplitButtonGallery or RibbonInRibbonGallery))))
                {
                    var labelprop = ctrl.GetType().GetProperty("Label");
                    var tooltipprop = ctrl.GetType().GetProperty("TooltipDescription");
                    var descprop = ctrl.GetType().GetProperty("LabelDescription");

                    var item = new RibbonCustomizerItem
                    {
                        Control = ctrl,
                        Title = ctrl is RibbonSplitButton sb
                        ? (sb.TooltipDescription ?? string.Empty) + " (split button)"
                        : (StringHelper.RemoveFirstOccurrence((string?)labelprop?.GetValue(ctrl) ?? string.Empty, '&')),
                        Description = ((string?)tooltipprop?.GetValue(ctrl)).Or((string?)descprop?.GetValue(ctrl) ?? string.Empty)
                    };

                    if (ri.QAT.QatItemsSource != null)
                    {
                        if (ri.QAT.QatItemsSource.Any(x => x.CommandId == ctrl.CommandId))
                        {
                            _pinItems.Add(item);
                            continue;
                        }
                    }

                    _avItems.Add(item);
                }
            }

            //var allItems = _avItems.Concat(_pinItems);
            //var allTitles = allItems.Select(x => x.Title);

            //foreach (var item in allItems)
            //{
            //    if (allTitles.Contains(item.Title))
            //    {
            //        var labelprop = ctrl.GetType().GetProperty("Label");
            //        item.Title = $"{item.Control}";
            //    }
            //}

            lbAll.Items.AddRange(_avItems.ToArray());
            lbCurrent.Items.AddRange(_pinItems.ToArray());
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            lbAll.Items.Clear();
            lbAll.BeginUpdate();

            if (string.IsNullOrEmpty(searchBox.Text))
            {
                // optimization
                lbAll.Items.AddRange(_avItems.ToArray());
                lbAll.EndUpdate();
                return;
            }

            var keywords = searchBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            lbAll.Items.AddRange(_avItems.Where(x =>
            {
                bool flag = false;

                // if at least one word matches count it
                foreach (var word in keywords)
                {
                    if (x.Title.Contains(word, StringComparison.OrdinalIgnoreCase))
                    {
                        flag = true;
                        break;
                    }
                }

                return flag;
            }).ToArray());

            lbAll.EndUpdate();
        }

        private void lbAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region DESCRIPTION
            if (lbAll.SelectedItems.Count > 1)
            {
                descLabel.Text = "<Multiple items selected>";
            }
            else if (lbAll.SelectedItems.Count == 0)
            {
                descLabel.Text = "Select an item to view its description";
            }
            else
            {
                descLabel.Text = ((RibbonCustomizerItem?)lbAll.SelectedItem)?.Description ?? string.Empty;
            }
            #endregion

            buttonAdd.Enabled = lbAll.SelectedItems.Count > 0;
        }

        private void lbCurrent_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRemove.Enabled = lbCurrent.SelectedItems.Count > 0;
            buttonUp.Enabled = lbCurrent.SelectedItems.Count == 1 && lbCurrent.SelectedIndex != 0;
            buttonDown.Enabled = lbCurrent.SelectedItems.Count == 1 && lbCurrent.SelectedIndex != lbCurrent.Items.Count - 1;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var itemsToAdd = lbAll.SelectedItems.Cast<object>().ToList();

            lbAll.BeginUpdate();
            lbCurrent.BeginUpdate();

            lbAll.ClearSelected();
            lbCurrent.ClearSelected();

            foreach (var item in itemsToAdd)
            {
                lbAll.Items.Remove(item);
                lbCurrent.Items.Add(item);
                lbCurrent.SelectedItems.Add(item);
            }

            lbAll.EndUpdate();
            lbCurrent.EndUpdate();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var itemsToRemove = lbCurrent.SelectedItems.Cast<object>().ToList();

            lbAll.BeginUpdate();
            lbCurrent.BeginUpdate();

            lbCurrent.ClearSelected();
            lbAll.ClearSelected();

            foreach (var item in itemsToRemove)
            {
                lbCurrent.Items.Remove(item);
                lbAll.Items.Add(item);
                lbAll.SelectedItems.Add(item);
            }

            lbAll.EndUpdate();
            lbCurrent.EndUpdate();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (_ri.QAT.QatItemsSource != null)
            {
                _ri.QAT.QatItemsSource.Clear();

                foreach (var item in lbCurrent.Items.Cast<RibbonCustomizerItem>())
                    _ri.QAT.QatItemsSource.Add(new QatCommandPropertySet // no addrange
                    {
                        CommandId = item.Control.CommandId
                    });
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            var item = lbCurrent.SelectedItem;

            if (item != null)
            {
                lbCurrent.ClearSelected();

                var idx = lbCurrent.Items.IndexOf(item);

                //if (idx == 0)
                //{
                //    var mb = new Win32MessageBox();
                //    mb.Caption = null;
                //    mb.Text = "This item cannot be moved up as it is already at the top.";
                //    mb.Icon = Win32MessageBoxIcon.Error;

                //    mb.Show(Handle);

                //    lbCurrent.SelectedIndex = idx;
                //    return;
                //}

                int newIdx = idx - 1;

                lbCurrent.Items.Remove(item);
                lbCurrent.Items.Insert(newIdx, item);

                lbCurrent.SelectedIndex = newIdx;
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            var item = lbCurrent.SelectedItem;

            if (item != null)
            {
                lbCurrent.ClearSelected();

                var idx = lbCurrent.Items.IndexOf(item);

                //if (idx == lbCurrent.Items.Count - 1)
                //{
                //    var mb = new Win32MessageBox();
                //    mb.Caption = null;
                //    mb.Text = "This item cannot be moved down as it is already at the bottom.";
                //    mb.Icon = Win32MessageBoxIcon.Error;

                //    mb.Show(Handle);

                //    lbCurrent.SelectedIndex = idx;
                //    return;
                //}

                int newIdx = idx + 1;

                lbCurrent.Items.Remove(item);
                lbCurrent.Items.Insert(newIdx, item);

                lbCurrent.SelectedIndex = newIdx;
            }
        }
    }

    public class RibbonCustomizerItem
    {
        public required IRibbonControl Control { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        public override string ToString() => Title;
    }
}
