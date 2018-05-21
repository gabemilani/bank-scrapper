using System.ComponentModel;

namespace BankScrapper.Enums
{
    public enum BillState
    {
        [Description("N/A")]
        Unknown,
        [Description("Aberta")]
        Open,
        [Description("Fechada")]
        Closed,
        [Description("Atrasada")]
        Overdue,
        [Description("Paga")]
        Paid
    }
}