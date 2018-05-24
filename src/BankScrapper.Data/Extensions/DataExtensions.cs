using BankScrapper.Domain.Attributes;
using BankScrapper.Domain.Interfaces;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Reflection;

namespace BankScrapper.Data
{
    internal static class DataExtensions
    {
        public static void ConfigTable<TEntity>(this EntityTypeConfiguration<TEntity> config) where TEntity : class, IEntity
        {
            var collectionAttribute = typeof(TEntity).GetCustomAttribute<CollectionAttribute>();
            if (collectionAttribute == null)
                throw new Exception($"A entidade \"{typeof(TEntity).Name}\" não implementa o atributo Collection");

            config.ToTable(collectionAttribute.Name);
        }        
    }
}