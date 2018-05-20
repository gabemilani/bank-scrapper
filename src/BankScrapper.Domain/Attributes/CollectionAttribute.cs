using System;

namespace BankScrapper.Domain.Attributes
{
    public sealed class CollectionAttribute : Attribute
    {
        public CollectionAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome da coleção precisa ser informado", nameof(name));

            Name = name;
        }

        public string Name { get; }
    }
}