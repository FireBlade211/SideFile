using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Win32;
using Windows.Win32.Foundation;
using WinForms.Ribbon;

namespace SideFile.Classic.Helpers
{
    /// <summary>
    /// Provides helper methods for DWM (Desktop Window Manager) theme operations.
    /// </summary>
    public static class ThemeHelper
    {
        /// <summary>
        /// Gets the current accent color (or aero glass color, on earlier Windows versions).
        /// </summary>
        /// <returns>The current accent color if successful; otherwise, <see langword="null"/>.</returns>
        public static Color? GetAccentColor()
        {
            if (PInvoke.DwmGetColorizationColor(out uint colorization, out BOOL opaqueBlend).Succeeded)
            {
                byte a = (byte)((colorization >> 24) & 0xFF);
                byte r = (byte)((colorization >> 16) & 0xFF);
                byte g = (byte)((colorization >> 8) & 0xFF);
                byte b = (byte)(colorization & 0xFF);

                return Color.FromArgb(a, r, g, b);
            }

            return null;
        }

        /// <summary>
        /// Checks if the current theme is dark mode.
        /// </summary>
        /// <returns><see langword="true"/> if the current theme uses dark mode; otherwise, <see langword="false"/>.</returns>
        /// <remarks>This function always returns <see langword="false"/> on operating systems earlier than Windows 10.</remarks>
        public static bool IsDarkMode()
        {
            const string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            using var key = Registry.CurrentUser.OpenSubKey(keyPath);
            if (key != null)
            {
                var appsUseLightTheme = key.GetValue("AppsUseLightTheme");
                if (appsUseLightTheme is int value)
                    return value == 0; // 0 = dark, 1 = light
            }
            return false; // fallback default
        }
    }

    /// <summary>
    /// Represents a watcher for Windows theme settings.
    /// </summary>
    /// <remarks>
    /// This class is implemented because WM_THEMECHANGED and WM_DWMCOLORIZATIONCOLORCHANGED do not fire for dark theme and accent color changes.
    /// </remarks>
    public class ThemeWatcher : IDisposable
    {
        private Thread _watchThread;
        private bool _running;

        private const int KEY_NOTIFY = 0x0010;
        private IntPtr _hKeyTheme;
        private IntPtr _hKeyDwm;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int RegOpenKeyEx(
            UIntPtr hKey, string subKey, uint options, int samDesired, out IntPtr phkResult);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int RegNotifyChangeKeyValue(
            IntPtr hKey, bool bWatchSubtree, int dwNotifyFilter,
            IntPtr hEvent, bool fAsynchronous);

        [DllImport("advapi32.dll")]
        private static extern int RegCloseKey(IntPtr hKey);

        private const int REG_NOTIFY_CHANGE_LAST_SET = 0x00000004;

        public event Action? ThemeChanged;
        public event Action? AccentColorChanged;

        public ThemeWatcher()
        {
            RegOpenKeyEx((UIntPtr)0x80000001, // HKEY_CURRENT_USER
                @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                0, KEY_NOTIFY, out _hKeyTheme);

            RegOpenKeyEx((UIntPtr)0x80000001,
                @"Software\Microsoft\Windows\DWM",
                0, KEY_NOTIFY, out _hKeyDwm);

            _running = true;
            _watchThread = new Thread(WatchLoop) { IsBackground = true };
            _watchThread.Start();
        }

        private void WatchLoop()
        {
            while (_running)
            {
                // Wait for theme changes
                RegNotifyChangeKeyValue(_hKeyTheme, false, REG_NOTIFY_CHANGE_LAST_SET, IntPtr.Zero, false);
                ThemeChanged?.Invoke();

                // Wait for accent changes
                RegNotifyChangeKeyValue(_hKeyDwm, false, REG_NOTIFY_CHANGE_LAST_SET, IntPtr.Zero, false);
                AccentColorChanged?.Invoke();
            }
        }

        public void Dispose()
        {
            _running = false;
            _watchThread.Join();
            RegCloseKey(_hKeyTheme);
            RegCloseKey(_hKeyDwm);
        }
    }
}
