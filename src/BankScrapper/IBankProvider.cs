using System.Threading.Tasks;

namespace BankScrapper
{
    public interface IBankProvider
    {
        Task<BankScrapeResult> GetResultAsync();
    }
}