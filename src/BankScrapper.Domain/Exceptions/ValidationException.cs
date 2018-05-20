using System;

namespace BankScrapper.Domain.Exceptions
{
    public sealed class ValidationException<TEntity> : Exception
    {
        public ValidationException(string message) : base(message)
        {
            EntityType = typeof(TEntity);
        }

        public Type EntityType { get; }
    }
}