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
                configExpression.CreateMap<BankScrapper.Models.Account, Account>();

                configExpression.CreateMap<BankScrapper.Models.Bill, Bill>();

                configExpression.CreateMap<BankScrapper.Models.Card, Card>();

                configExpression.CreateMap<BankScrapper.Models.Customer, Customer>()
                    .ForMember(c => c.Address, opt => opt.MapFrom(m => m.Address.ToString()))
                    .ForMember(c => c.BillingAddress, opt => opt.MapFrom(m => m.BillingAddress.ToString()));

                configExpression.CreateMap<BankScrapper.Models.Transaction, Transaction>()
                    .ForMember(t => t.Category, opt => opt.Ignore());

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