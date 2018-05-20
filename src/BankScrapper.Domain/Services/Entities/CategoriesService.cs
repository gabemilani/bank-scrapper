using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Exceptions;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Domain.Repositories;
using BankScrapper.Utils;
using System;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Services
{
    public sealed class CategoriesService : EntitiesService<Category>, IService
    {
        private ICategoriesRepository _categoriesRepository;

        public CategoriesService(IContext context) : base(context)
        {
        }

        private ICategoriesRepository CategoriesRepository => _categoriesRepository ?? (_categoriesRepository = _repository as ICategoriesRepository);

        public Task<Category> GetByNameAsync(string name) => CategoriesRepository.FindByNameAsync(name);

        protected override async Task ValidateAsync(Category category, bool isNew = true)
        {
            await base.ValidateAsync(category, isNew);

            if (category.Name.IsNullOrEmpty())
                throw new ValidationException<Category>("O nome da categoria precisa ser informado");

            var foundCategory = await GetByNameAsync(category.Name);
            if (foundCategory != null && (isNew || foundCategory.Id != category.Id))
                throw new ValidationException<Category>("Já existe uma categoria com esse nome");
        }
    }
}