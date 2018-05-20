using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Exceptions;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Domain.Repositories;
using BankScrapper.Enums;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Services.Entities
{
    public sealed class CardsService : EntitiesService<Card>, IService
    {
        public CardsService(IContext context) : base(context)
        {
        }

        protected override async Task ValidateAsync(Card card, bool isNew = true)
        {
            await base.ValidateAsync(card, isNew);

            if (card.Type == CardType.Unknown)
                throw new ValidationException<Card>("O tipo do cartão precisa ser informado");

            if (card.Account == null && card.AccountId == 0)
                throw new ValidationException<Card>("A conta do cartão precisa ser informada");
        }
    }
}