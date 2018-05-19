namespace BankScrapper
{
    public interface IBankConnectionData
    {
        Bank Bank { get; }

        bool IsValid();
    }
}