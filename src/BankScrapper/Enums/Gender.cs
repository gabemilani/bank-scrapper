using System.ComponentModel;

namespace BankScrapper.Enums
{
    public enum Gender
    {
        [Description("N/A")]
        Unknown,
        [Description("Masculino")]
        Male,
        [Description("Feminino")]
        Female
    }
}