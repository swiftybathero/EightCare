using EightCare.Domain.Common;
using EightCare.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace EightCare.Domain.KeeperAggregate
{
    public sealed class Animal : Entity
    {
        private readonly List<Feeding> _feedings;

        public string ScientificName { get; private set; }
        public string CommonName { get; private set; }
        public IReadOnlyCollection<Feeding> Feedings => _feedings.AsReadOnly();

        public Animal(string scientificName, string commonName)
        {
            SetScientificName(scientificName);
            SetCommonName(commonName);

            _feedings = new List<Feeding>();
        }

        private void SetScientificName(string scientificName)
        {
            if (string.IsNullOrEmpty(scientificName))
                throw new KeeperDomainException("Scientific Name cannot be empty.");

            ScientificName = scientificName;
        }

        private void SetCommonName(string commonName)
        {
            CommonName = commonName;
        }

        public void Feed(int amount = 1, DateTime? feedingDate = null)
        {
            if (amount < 1)
                throw new KeeperDomainException("Amount of feed can't be lower than 1.");

            _feedings.Add(new Feeding(feedingDate ?? DateTime.Now, amount));
        }
    }
}
