using AutoMapper;
using BankScrapper.Domain.Entities;
using BankScrapper.Utils;
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

                configExpression.CreateMap<Account, AccountViewModel>()
                    .ForMember(m => m.Bank, opt => opt.MapFrom(a => a.Bank.GetDescription()))
                    .ForMember(m => m.Type, opt => opt.MapFrom(a => a.Type.GetDescription()));

                configExpression.CreateMap<Bill, BillViewModel>()
                    .ForMember(m => m.State, opt => opt.MapFrom(b => b.State.GetDescription()));

                configExpression.CreateMap<Card, CardViewModel>()
                    .ForMember(m => m.Type, opt => opt.MapFrom(c => c.Type.GetDescription()));

                configExpression.CreateMap<Customer, CustomerViewModel>()
                    .ForMember(m => m.Gender, opt => opt.MapFrom(c => c.Gender.GetDescription()));

                configExpression.CreateMap<Transaction, TransactionViewModel>();
            });

            var mapper = config.CreateMapper();
            unityContainer.RegisterInstance(mapper);
        }
    }
}