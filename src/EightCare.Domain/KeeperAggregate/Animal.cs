using EightCare.Domain.Common;
using EightCare.Domain.Exceptions;
using EightCare.Domain.Properties;
using System;
using System.Collections.Generic;

namespace EightCare.Domain.KeeperAggregate
{
    public sealed class Animal : Entity
    {
        private readonly List<Feeding> _feedings;
        private readonly List<Molt> _molts;

        public string ScientificName { get; private set; }
        public string CommonName { get; private set; }
        public DateTime BuyDate { get; private set; }
        public int BuyAge { get; private set; }
        public IReadOnlyCollection<Feeding> Feedings => _feedings.AsReadOnly();
        public IReadOnlyCollection<Molt> Molts => _molts.AsReadOnly();

        public Animal(string scientificName, string commonName)
        {
            SetScientificName(scientificName);
            SetCommonName(commonName);

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
