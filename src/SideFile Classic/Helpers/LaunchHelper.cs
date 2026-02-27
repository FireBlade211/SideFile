using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SideFile.Classic.Helpers
{
    /// <summary>
    /// Provides helper methods for launching different types of applications.
    /// </summary>
    public static class LaunchHelper
    {
        /// <summary>
        /// Launches the specified URI.
        /// </summary>
        /// <param name="uri">The URI to launch.</param>
        public static void LaunchUri(string uri) => Process.Start(new ProcessStartInfo
        {
            FileName = uri,
            UseShellExecute = true
        });

        /// <summary>
        /// Launches the specified URI.
        /// </summary>
        /// <param name="uri">The URI to launch.</param>
        public static void LaunchUri(Uri uri) => Process.Start(new ProcessStartInfo
        {
            FileName = uri.ToString(),
            UseShellExecute = true
        });
    }
}
