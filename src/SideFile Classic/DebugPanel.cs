using FireBlade.WinInteropUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SideFile.Classic
{
    public partial class DebugPanel : Form
    {
        private static event EventHandler<Exception>? ExceptionReceived;
        private static List<Exception> exceptions = [];

        public DebugPanel()
        {
            InitializeComponent();

            DoubleBuffered = true;
            ExceptionReceived += (s, e) => AddException(e);

            foreach (var ex in exceptions.ToList()) // clone
            {
                AddException(ex);
                exceptions.Remove(ex);
            }
        }

        private void AddException(Exception ex)
        {
            var item = new ListViewItem(ex.GetType().FullName ?? ex.GetType().Name);
            item.Tag = ex;
            item.SubItems.Add(ex.Message);
            item.SubItems.Add($"{ex.ToHResult()} (0x{ex.HResult:X})");

            exListView.Items.Insert(0, item);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debugger.Launch();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Debugger.Break();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Debugger.Log((int)numericUpDown1.Value, string.IsNullOrEmpty(textBox1.Text) ? null : textBox1.Text,
                string.IsNullOrEmpty(textBox2.Text) ? null : textBox2.Text);
        }

        public static void OnExceptionReceived(Exception ex)
        {
            exceptions.Add(ex);
            ExceptionReceived?.Invoke(null, ex);
        }

        private void exListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            exPropertyGrid.SelectedObjects = (from ListViewItem item in exListView.SelectedItems
                                             select item.Tag).ToArray();
        }
    }
}
