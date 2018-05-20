using System.Text;

namespace BankScrapper.Data
{
    public static class SqlQueryConditionExtensions
    {
        public static void And(this StringBuilder sb, string value)
        {
            if (sb.Length > 0)
                sb.Append(" AND ");

            sb.Append(value);
        }
    }
}