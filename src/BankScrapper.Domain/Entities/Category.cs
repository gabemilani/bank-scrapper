using BankScrapper.Domain.Interfaces;

namespace BankScrapper.Domain.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}