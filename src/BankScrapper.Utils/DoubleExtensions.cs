using System.Diagnostics;
using System.Globalization;

namespace BankScrapper.Utils
{
    [DebuggerStepThrough]
    public static class DoubleExtensions
    {
        private static CultureInfo _brazillianCulture = CultureInfo.CreateSpecificCulture("pt-BR");

        public static string ToBrazillianCurrency(this double value) => value.ToString("C", _brazillianCulture);
    }
}