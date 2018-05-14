using System.Diagnostics;

namespace BankScrapper.Nubank
{
    [DebuggerStepThrough]
    internal static class NubankExtensions
    {
        public static double GetMonetaryValue(this long value) => (double)value / 100;
    }
}