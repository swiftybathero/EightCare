using System;
using System.Collections.Generic;
using EightCare.Domain.Common;
using EightCare.Domain.Exceptions;
using EightCare.Domain.Properties;

namespace EightCare.Domain.Entities
{
    public sealed class Animal : Entity
    {
        private readonly List<Feeding> _feedings;
        private readonly List<Molt> _molts;

        public string ScientificName { get; private set; }
        public string CommonName { get; private set; }
        public DateTime BuyDate { get; private set; }
        public int BuyAge { get; private set; }
        public int Age => BuyAge + _molts.Count;
        public IReadOnlyCollection<Feeding> Feedings => _feedings.AsReadOnly();
        public IReadOnlyCollection<Molt> Molts => _molts.AsReadOnly();

        public Animal(string scientificName, string commonName, DateTime buyDate, int buyAge)
        {
            SetScientificName(scientificName);
            SetCommonName(commonName);
            SetBuyDate(buyDate);
            SetBuyAge(buyAge);

            _feedings = new List<Feeding>();
            _molts = new List<Molt>();
        }

        private void SetScientificName(string scientificName)
        {
            if (string.IsNullOrEmpty(scientificName))
                throw new KeeperDomainException(ExceptionMessages.ScientificNameCannotBeEmpty);

            ScientificName = scientificName;
        }

        private void SetCommonName(string commonName)
        {
            CommonName = commonName;
        }

        private void SetBuyDate(DateTime buyDate)
        {
            BuyDate = buyDate;
        }

        private void SetBuyAge(int buyAge)
        {
            if (buyAge < 1)
                throw new KeeperDomainException(ExceptionMessages.BuyAgeCannotBeLowerThanOne);

            BuyAge = buyAge;
        }

        public void Feed(int amount = 1, DateTime? feedingDate = null)
        {
            if (amount < 1)
                throw new KeeperDomainException(ExceptionMessages.FeedAmountCannotBeLowerThanOne);

            _feedings.Add(new Feeding(feedingDate ?? DateTime.Now, amount));
        }

        public void Molt(DateTime? moltingDate = null)
        {
            _molts.Add(new Molt(moltingDate ?? DateTime.UtcNow));
        }
    }
}
