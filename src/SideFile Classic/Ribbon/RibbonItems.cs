using SideFile.Classic;
using FireBlade.WinInteropUtils;
using SideFile.Classic.Helpers;
using static SideFile.Classic.Helpers.PropertySheetHelper;
using System.Runtime.InteropServices;
using Windows.Win32;
using System.ComponentModel;

namespace WinForms.Ribbon
{
    public partial class RibbonItems
    {
        //private Bitmap si(StockIcon id, StockIconOptions options = StockIconOptions.Default)
        //{
        //    var icon = StockIconHelper.GetIcon(id, options);
        //    var bmp = icon.ToBitmap();
        //    icon.Dispose();
        //    return bmp;
        //}

        public void Init(Form1 form)
        {
            //btnNewWindow.SmallImage = new UIImage(form.Ribbon, si(StockIcon.Application, StockIconOptions.SmallIcon), false);
            //btnNewWindow.LargeImage = new UIImage(form.Ribbon, si(StockIcon.Application), false);

            sbgCliHere.ItemsSourceReady += CliHereGallery_ItemsSourceReady;
            QAT.Click += (s, e) => new QatCustomizer(this).ShowDialog();
            btnAboutWin.Click += (s, e) => Shell32.ShellAbout(form.Handle, "Windows", null!);
            btnMkFLnk.Click += BtnMakeFile_WinLink_Click;
            HelpButton.Click += (s, e) => LaunchHelper.LaunchUri("https://github.com/fireblade211/sidefile");
            btnAbout.Click += (s, e) => new AboutBox().ShowDialog();
        }

        private static readonly DlgProc Page1DlgProc = WinLinkLocation_WizardDlgProc;
        private static readonly DlgProc Page2DlgProc = WinLinkName_WizardDlgProc;

        private unsafe void BtnMakeFile_WinLink_Click(object? sender, EventArgs e)
        {
#pragma warning disable CS8500
            //fixed (PROPSHEETPAGE* ppsp = new PROPSHEETPAGE[2])
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        ppsp[i] = default;
            //    }

            //    fixed (char* title1 = "Where do you want the shortcut to go?",
            //        title2 = "Choose a name for the shortcut",
            //        caption = "Create shortcut")
            //    {
            //        ppsp[0].dwFlags = PSP_USEHEADERTITLE | PSP_USETEMPLATE;
            //        ppsp[0].pszHeaderTitle = title1;
            //        ppsp[0].pszTemplate = MakeIntResource(107);
            //        ppsp[0].pfnDlgProc = Marshal.GetFunctionPointerForDelegate(Page1DlgProc);

            //        ppsp[1].dwFlags = PSP_USEHEADERTITLE | PSP_USETEMPLATE;
            //        ppsp[1].pszHeaderTitle = title2;
            //        ppsp[1].pszTemplate = MakeIntResource(108);
            //        ppsp[1].pfnDlgProc = Marshal.GetFunctionPointerForDelegate(Page2DlgProc);

            //        var span = new Span<PROPSHEETPAGE>(ppsp, 2);

            //        for (int i = 0; i < span.Length; i++)
            //        {
            //            ppsp[i].dwSize = (uint)Marshal.SizeOf<PROPSHEETPAGE>();
            //            ppsp[i].hInstance = PInvoke.GetModuleHandle().DangerousGetHandle();
            //        }

            //        PROPSHEETHEADER psh = new PROPSHEETHEADER();
            //        psh.nPages = (uint)span.Length;
            //        psh.ppsp = (nint)ppsp;
            //        psh.pszCaption = caption;
            //        psh.hInstance = PInvoke.GetModuleHandle().DangerousGetHandle();
            //        psh.dwSize = (uint)Marshal.SizeOf<PROPSHEETHEADER>();
            //        psh.hwndParent = Form1.Current.Handle;
            //        psh.dwFlags = PSH_PROPSHEETPAGE | PSH_WIZARD | PSH_AEROWIZARD;

            //        int result = PropertySheetW(ref psh);
            //        // modal propsheets are synchronous (documented behavior)

            //        if (result == -1)
            //            DebugPanel.OnExceptionReceived(new Win32Exception());
            //    }
            //}
#pragma warning restore
        }

        private static nint WinLinkLocation_WizardDlgProc(nint hwnd, uint msg, nuint wParam, nint lParam)
        {
            return nint.Zero;
        }

        private static nint WinLinkName_WizardDlgProc(nint hwnd, uint msg, nuint wParam, nint lParam)
        {
            return nint.Zero;
        }

        private void CliHereGallery_ItemsSourceReady(object? sender, EventArgs e)
        {
            sbgCliHere.GalleryCategories!.Add(new CategoriesPropertySet
            {
                Label = "Start a command processor here",
                CategoryId = 0
            });

            sbgCliHere.GalleryCategories!.Add(new CategoriesPropertySet
            {
                CategoryId = 1
            });

            // BROWSE
            sbgCliHere.GalleryCommandItemsSource!.Add(new GalleryCommandPropertySet
            {
                CommandId = 16,
                CategoryId = 0,
                CommandType = CommandType.Action
            });

            // CMD
            sbgCliHere.GalleryCommandItemsSource!.Add(new GalleryCommandPropertySet
            {
                CommandId = 17,
                CategoryId = 1,
                CommandType = CommandType.Action
            });

            // WIN PWSH
            sbgCliHere.GalleryCommandItemsSource!.Add(new GalleryCommandPropertySet
            {
                CommandId = 18,
                CategoryId = 1,
                CommandType = CommandType.Action
            });

            // PWSH CORE
            if (EnvironmentHelper.CommandExists("pwsh.exe"))
            {
                sbgCliHere.GalleryCommandItemsSource!.Add(new GalleryCommandPropertySet
                {
                    CommandId = 19,
                    CategoryId = 1,
                    CommandType = CommandType.Action
                });
            }
        }
    }
}
