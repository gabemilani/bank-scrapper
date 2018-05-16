using System;

namespace BankScrapper.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}