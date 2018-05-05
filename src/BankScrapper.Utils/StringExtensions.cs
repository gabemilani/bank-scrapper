using System;
using System.Diagnostics;

namespace BankScrapper.Utils
{
    [DebuggerStepThrough]
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source);

        public static bool EqualsIgnoreCase(this string source, string value) => string.Equals(source, value, StringComparison.OrdinalIgnoreCase);
    }
}