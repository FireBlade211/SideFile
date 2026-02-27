using System;
using System.Collections.Generic;
using System.Text;

namespace SideFile.Classic.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Removes the first occurence of a character in a string.
        /// </summary>
        /// <param name="s">The string to remove the character from.</param>
        /// <param name="c">The character to remove.</param>
        /// <returns>The string <paramref name="s"/> with the first occurence of the character <paramref name="c"/> removed,
        /// or the same string <paramref name="s"/> if the string didn't contain the character.</returns>
        public static string RemoveFirstOccurrence(string s, char c)
        {
            int index = s.IndexOf(c);
            return index < 0 ? s : s.Remove(index, 1);
        }

        /// <summary>
        /// Returns the source string <paramref name="s"/>, or falls back to the string <paramref name="fallback"/> depending on the state.
        /// </summary>
        /// <param name="s">The source string.</param>
        /// <param name="fallback">The fallback string.</param>
        /// <returns>The source string <paramref name="s"/>, or the fallback string <paramref name="fallback"/> if the source string
        /// is <see langword="null"/>, empty, or whitespace.</returns>
        public static string Or(this string? s, string fallback)
        {
            if (string.IsNullOrEmpty(s))      return fallback;
            if (string.IsNullOrWhiteSpace(s)) return fallback;

            return s;
        }
    }
}
