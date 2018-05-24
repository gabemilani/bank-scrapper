using AutoMapper;
using BankScrapper.Domain.Entities;
using BankScrapper.Web.Models.Views;
using Unity;

namespace BankScrapper.Web
{
    public static class AutoMapperConfig
    {
        public static void RegisterMapper(this IUnityContainer unityContainer)
        {
            var config = new MapperConfiguration(configExpression =>
            {
                configExpression.CreateMap<Account, AccountViewModel>();
                configExpression.CreateMap<Bill, BillViewModel>();
                configExpression.CreateMap<Card, CardViewModel>();
                configExpression.CreateMap<Customer, CustomerViewModel>();
                configExpression.CreateMap<Transaction, TransactionViewModel>();
            });

            var mapper = config.CreateMapper();
            unityContainer.RegisterInstance(mapper);
        }
    }
}