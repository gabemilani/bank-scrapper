using BankScrapper.Domain.Attributes;
using BankScrapper.Domain.Interfaces;

namespace BankScrapper.Domain.Entities
{
    [Collection("Categories")]
    public class Category : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}