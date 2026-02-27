using System;
using System.Collections.Generic;
using System.Text;

namespace SideFile.Classic.Helpers
{
    public static class EnvironmentHelper
    {
        /// <summary>
        /// Checks if a command exists based on PATH environment variables.
        /// </summary>
        /// <param name="command">The command to find.</param>
        /// <returns><see langword="true"/> if the command exists; otherwise, <see langword="false"/>.</returns>
        public static bool CommandExists(string command)
        {
            // Absolute or relative path given?
            if (File.Exists(command))
                return true;

            var paths = (Environment.GetEnvironmentVariable("PATH") ?? "")
                .Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries);

            var extensions = (Environment.GetEnvironmentVariable("PATHEXT") ?? "")
                .Split(';', StringSplitOptions.RemoveEmptyEntries);

            // If the command already has an extension, check directly
            bool hasExtension = Path.HasExtension(command);

            foreach (var path in paths)
            {
                if (hasExtension)
                {
                    var fullPath = Path.Combine(path, command);
                    if (File.Exists(fullPath))
                        return true;
                }
                else
                {
                    foreach (var ext in extensions)
                    {
                        var fullPath = Path.Combine(path, command + ext);
                        if (File.Exists(fullPath))
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
