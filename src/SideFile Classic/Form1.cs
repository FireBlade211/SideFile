using FireBlade.WinInteropUtils.Dialogs;
using SideFile.Classic.Helpers;
using Windows.Win32;
using WinForms.Ribbon;
using FireBlade.WinInteropUtils;

namespace SideFile.Classic
{
    public partial class Form1 : Form
    {
        public static Form1 Current { get; set; } = null!;

        private RibbonItems RibbonItems { get; set; }

        private ThemeWatcher? _themeWatcher;

        public Form1()
        {
            InitializeComponent();
            Current = this;
            
            RibbonItems = new RibbonItems(Ribbon);
            RibbonItems.Init(this);

            Icon = Properties.Resources.logo;
        }

        private void Ribbon_ViewCreated(object sender, EventArgs e)
        {
            var watcher = new ThemeWatcher();
            watcher.ThemeChanged += ApplyRibbonTheme;
            watcher.AccentColorChanged += ApplyRibbonTheme;

            _themeWatcher = watcher;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //var dark = Application.IsDarkModeEnabled; // does not work
            ApplyRibbonTheme();
        }

        private unsafe void ApplyRibbonTheme()
        {
            if (InvokeRequired)
            {
                Invoke(ApplyRibbonTheme);
                return;
            }

            /*
            var dark = ThemeHelper.IsDarkMode();
            Ribbon.SetDarkModeRibbon(dark);

            leftListView.BackColor = dark ? Color.Black : SystemColors.Window;
            rightListView.BackColor = dark ? Color.Black : SystemColors.Window;
            BackColor = dark ? Color.Black : SystemColors.Control;

            leftListView.ForeColor = dark ? Color.White : SystemColors.WindowText;
            rightListView.ForeColor = dark ? Color.White : SystemColors.WindowText;

            Application.SetColorMode(dark ? SystemColorMode.Dark : SystemColorMode.Classic);
            */

            //BOOL immersive = dark;

            //PInvoke.DwmSetWindowAttribute(new HWND(Handle), DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, &immersive, (uint)sizeof(BOOL));

            if (ThemeHelper.GetAccentColor() is Color accent)
                Ribbon.SetApplicationButtonColor(new UI_HSBCOLOR(accent));

            Ribbon.SetTextColor(new UI_HSBCOLOR(0, 0, 0));

            // Color.FromArgb(255, 32, 32, 32)
            /* FormCaptionBackColor = dark ? Color.FromArgb(255, 128, 128, 128) : Color.Empty; */
            //FormCaptionTextColor = Color.White;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case (int)PInvoke.WM_THEMECHANGED:
                case (int)PInvoke.WM_DWMCOLORIZATIONCOLORCHANGED:
                    ApplyRibbonTheme();
                    break;
            }
        }

        private void Ribbon_ViewDestroy(object sender, EventArgs e)
        {
            if (_themeWatcher != null) _themeWatcher.Dispose();
        }

        public void UpdateTitle(string title, bool left)
        {
            if (left)
                Text = $"SideFile - [{title} | {rightListView.DisplayDir}]";
            else
                Text = $"SideFile - [{leftListView.DisplayDir} | {title}]";
        }

        private void leftListView_Navigated(object sender, string e)
        {
            UpdateTitle(e, true);
        }

        private void rightListView_Navigated(object sender, string e)
        {
            UpdateTitle(e, false);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
