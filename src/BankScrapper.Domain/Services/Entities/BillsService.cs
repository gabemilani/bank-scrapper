using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Exceptions;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Domain.Repositories;
using BankScrapper.Enums;
using System;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Services
{
    public sealed class BillsService : EntitiesService<Bill>, IService
    {
        private IBillsRepository _billsRepository;

        public BillsService(IContext context) : base(context)
        {
        }

        private IBillsRepository BillsRepository => _billsRepository ?? (_billsRepository = _repository as IBillsRepository);

        public Task<Bill[]> GetManyAsync(int? accountId = null, BillState? state = null, DateTime? fromDate = null, DateTime? toDate = null) =>
            BillsRepository.FindAsync(accountId, state, fromDate, toDate);

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