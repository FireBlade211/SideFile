using FireBlade.WinInteropUtils.Dialogs;
using System.Reflection.Metadata;

namespace SideFile.Classic
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

#if DEBUG
#pragma warning disable CS0618
            //var mb = new WinMessageBox();
            //mb.Caption = "Debug mode";
            //mb.Text = "Do you want to show the debug panel?";
            //mb.Icon = WinMessageBoxIcon.Question;
            //mb.Buttons = WinMessageBoxButtons.OkCancel;
            //mb.DefaultButton = 1;

            //if (mb.Show() == WinMessageBoxResult.Ok)
            //{
                var panel = new DebugPanel();
                panel.Show();
            //}
#pragma warning restore
#endif

            Application.Run(new Form1());
        }
    }
}