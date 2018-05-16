using BankScrapper.Domain.Interfaces;
using System;

namespace BankScrapper.Domain.Entities
{
    public class Category : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}