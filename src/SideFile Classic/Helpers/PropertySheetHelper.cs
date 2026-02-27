using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SideFile.Classic.Helpers
{
    public static partial class PropertySheetHelper
    {
        // PROPSHEETPAGE flags
        public const uint PSP_DEFAULT = 0x00000000U;
        public const uint PSP_USEHICON = 0x00000002U;
        public const uint PSP_USEICONID = 0x00000004U;
        public const uint PSP_USETITLE = 0x00000008U;
        public const uint PSP_RTLREADING = 0x00000010U;
        public const uint PSP_HASHELP = 0x00000020U;
        public const uint PSP_USEREFPARENT = 0x00000040U;
        public const uint PSP_USEHEADERTITLE = 0x00000080U;
        public const uint PSP_USEHEADERSUBTITLE = 0x00000100U;
        public const uint PSP_USETEMPLATE = 0x00000001;

        // PROPSHEETHEADER flags
        public const uint PSH_PROPSHEETPAGE = 0x00000001U;
        public const uint PSH_USEHICON = 0x00000002U;
        public const uint PSH_USEICONID = 0x00000004U;
        public const uint PSH_PROPTITLE = 0x00000008U;
        public const uint PSH_WIZARD = 0x00000010U;
        public const uint PSH_WIZARD_LITE = 0x00000020U;
        public const uint PSH_WIZARD97 = 0x00000040U;
        public const uint PSH_WATERMARK = 0x00000080U;
        public const uint PSH_USEPSTARTPAGE = 0x00000100U;
        public const uint PSH_USEHFONT = 0x00000200U;
        public const uint PSH_HASHELP = 0x00000400U;
        public const uint PSH_MODELESS = 0x00000800U;
        public const uint PSH_RTLREADING = 0x00002000U;
        public const uint PSH_NOAPPLYNOW = 0x00008000U;
        public const uint PSH_USEHEADERTITLE = 0x00010000U;
        public const uint PSH_USEHEADERSUBTITLE = 0x00020000U;
        public const uint PSH_AEROWIZARD = 16384U;

        // PROPSHEETPAGE structure
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public unsafe struct PROPSHEETPAGE
        {
            public uint dwSize;
            public uint dwFlags;
            public nint hInstance;

            // union
            public nint pszTemplate; // overlaps pResource

            // union
            public nint hIcon;       // overlaps pszIcon

            public char* pszTitle;
            public nint pfnDlgProc;
            public nint lParam;
            public nint pfnCallback;
            public uint* pcRefParent;
            public char* pszHeaderTitle;
            public char* pszHeaderSubTitle;
            public nint hActCtx;

            // union
            public nint hbmHeader;   // overlaps pszbmHeader
        }

        // PROPSHEETHEADER structure
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public unsafe struct PROPSHEETHEADER
        {
            public uint dwSize;
            public uint dwFlags;
            public nint hwndParent;
            public nint hInstance;

            // union
            public nint hIcon;       // overlaps pszIcon

            public char* pszCaption;
            public uint nPages;

            // union
            public nint nStartPage;  // overlaps pStartPage

            // union
            public nint ppsp;        // overlaps phpage

            public nint pfnCallback;

            // union
            public nint hbmWatermark; // overlaps pszbmWatermark

            public nint hplWatermark;

            // union
            public nint hbmHeader;   // overlaps pszbmHeader
        }

        [LibraryImport("comctl32.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
        public static partial int PropertySheetW(ref PROPSHEETHEADER psh);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate nint DlgProc(nint hwnd, uint msg, nuint wParam, nint lParam);

        public static nint MakeIntResource(int id) => id;
    }
}
