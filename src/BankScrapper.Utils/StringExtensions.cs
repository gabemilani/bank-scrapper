using System;
using System.Diagnostics;

namespace BankScrapper.Utils
{
    [DebuggerStepThrough]
    public static class StringExtensions
    {
        public static string Capitalize(this string source) => $"{source[0].ToString().ToUpper()}{source.Substring(1, source.Length - 1)}";

        public static bool EqualsIgnoreCase(this string source, string value) => string.Equals(source, value, StringComparison.OrdinalIgnoreCase);

        public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source);

        public static double ToDouble(this string source) => double.TryParse(source, out var result) ? result : default(double);

        public static int ToInt(this string source) => int.TryParse(source, out var result) ? result : default(int);
    }
}