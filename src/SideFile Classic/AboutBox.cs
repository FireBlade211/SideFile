using SideFile.Classic.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SideFile.Classic
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();

            var ver = Assembly.GetCallingAssembly().GetName().Version;

            if (ver != null)
                labelVersion.Text = $"v{ver.Major}.{ver.Minor}";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => LaunchHelper.LaunchUri("https://icons8.com/");

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            => LaunchHelper.LaunchUri("https://github.com/fireblade211/sidefile");
    }
}
