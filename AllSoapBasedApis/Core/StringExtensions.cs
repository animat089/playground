using System;
using System.Linq;

namespace Core
{
    public static class StringExtensions
    {
        public static int CountString(this string input)
        {
            return string.IsNullOrWhiteSpace(input) ? 0 : input.Length;
        }

        public static string ReverseString(this string input)
        {
            return string.IsNullOrWhiteSpace(input) ? null : new string(input.Reverse().ToArray());
        }
    }
}