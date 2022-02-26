using System;
using System.Collections.Generic;
using EightCare.Domain.Common;
using EightCare.Domain.Exceptions;
using EightCare.Domain.Properties;
using EightCare.Domain.ValueObjects;

namespace EightCare.Domain.Entities
{
    public sealed class Animal : Entity
    {
        private readonly List<Feeding> _feedings;
        private readonly List<Molt> _molts;

        // TODO: Refactor to Species ValueObject
        public Species Species { get; private set; }
        public DateTime BuyDate { get; private set; }
        public int BuyAge { get; private set; }
        public int Age => BuyAge + _molts.Count;
        public IReadOnlyCollection<Feeding> Feedings => _feedings.AsReadOnly();
        public IReadOnlyCollection<Molt> Molts => _molts.AsReadOnly();

        public Animal(Species species, DateTime buyDate, int buyAge)
        {
            Species = species;

            SetBuyDate(buyDate);
            SetBuyAge(buyAge);

            _feedings = new List<Feeding>();
            _molts = new List<Molt>();
        }

        private void SetBuyDate(DateTime buyDate)
        {
            BuyDate = buyDate;
        }

        private void SetBuyAge(int buyAge)
        {
            if (buyAge < 1)
            {
                throw new CollectionDomainException(ExceptionMessages.BuyAgeCannotBeLowerThanOne);
            }

            BuyAge = buyAge;
        }

        public void Feed(int amount = 1, DateTime? feedingDate = null)
        {
            if (amount < 1)
            {
                throw new CollectionDomainException(ExceptionMessages.FeedAmountCannotBeLowerThanOne);
            }

            // TODO: Provide DateTime.Now from external dependency
            _feedings.Add(new Feeding(feedingDate ?? DateTime.Now, amount));
        }

        public void Molt(DateTime? moltingDate = null)
        {
            // TODO: Provide DateTime.Now from external dependency
            _molts.Add(new Molt(moltingDate ?? DateTime.UtcNow));
        }
    }
}
