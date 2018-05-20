using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Domain.Repositories;

namespace BankScrapper.Domain.Services.Entities
{
    public sealed class CardsService : EntitiesService<Card>, IService
    {
        private ICardsRepository _cardsRepository;

        public CardsService(IContext context) : base(context)
        {
            _cardsRepository.FindAsync();
        }

        private ICardsRepository CardsRepository => _cardsRepository ?? (_cardsRepository = _repository as ICardsRepository);
    }
}