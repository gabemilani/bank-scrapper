using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Exceptions;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Domain.Repositories;
using BankScrapper.Utils;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Services
{
    public sealed class CustomersService : EntitiesService<Customer>, IService
    {
        private ICustomersRepository _customersRepository;

        public CustomersService(IContext context) : base(context)
        {
        }

        private ICustomersRepository CustomersRepository => _customersRepository ?? (_customersRepository = _repository as ICustomersRepository);

        public Task<Customer> GetByCpfAsync(string cpf) => CustomersRepository.FindByCpfAsync(cpf);

        protected override async Task ValidateAsync(Customer customer, bool isNew = true)
        {
            await base.ValidateAsync(customer, isNew);

            if (customer.Name.IsNullOrEmpty())
                throw new ValidationException<Customer>("O nome do cliente precisa ser informado");

            if (customer.Cpf.IsNullOrEmpty())
                throw new ValidationException<Customer>("O CPF do cliente precisa ser informado");

            var foundCustomer = await GetByCpfAsync(customer.Cpf);
            if (foundCustomer != null && (!isNew || !foundCustomer.Cpf.Equals(customer.Cpf)))
                throw new ValidationException<Customer>("Já existe um cliente com esse CPF");
        }
    }
}