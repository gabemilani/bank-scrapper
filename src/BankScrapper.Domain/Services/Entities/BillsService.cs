using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Exceptions;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Enums;
using System;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Services
{
    public sealed class BillsService : EntitiesService<Bill>, IService
    {
        public BillsService(IContext context) : base(context)
        {
        }

        protected override async Task ValidateAsync(Bill bill, bool isNew = true)
        {
            await base.ValidateAsync(bill, isNew);

            if (bill.State == BillState.Unknown)
                throw new ValidationException<Bill>("O estado da fatura precisa ser informado");

            if (bill.OpenDate == default(DateTime))
                throw new ValidationException<Bill>("A data de abertura da fatura precisa ser informada");

            if (bill.CloseDate == default(DateTime))
                throw new ValidationException<Bill>("A data de fechamento da fatura precisa ser informada");

            if (bill.Account == null && bill.AccountId == 0)
                throw new ValidationException<Bill>("A conta da fatura precisa ser informada");
        }
    }
}