using System;
using System.Collections.Generic;
using EightCare.Domain.Common;
using EightCare.Domain.Enums;
using EightCare.Domain.Exceptions;
using EightCare.Domain.Properties;
using EightCare.Domain.ValueObjects;

namespace EightCare.Domain.Entities
{
    public sealed class Animal : Entity
    {
        private readonly List<Feeding> _feedings;
        private readonly List<Molt> _molts;

        public string Name { get; private set; }
        public DateTimeOffset Received { get; private set; }
        public DateTimeOffset LastRehoused { get; private set; }
        public DateTimeOffset LastHydrated { get; private set; }
        public DateTimeOffset LastSubstrateChanged { get; private set; }
        public LifeStage LifeStage { get; private set; }
        public Sex Sex { get; private set; }
        public Species Species { get; private set; }

        public IReadOnlyCollection<Feeding> Feedings => _feedings.AsReadOnly();
        public IReadOnlyCollection<Molt> Molts => _molts.AsReadOnly();

        public Animal(Species species, string name, DateTimeOffset received, LifeStage lifeStage, Sex sex)
        {
            Species = species;
            Name = name;
            Received = received;
            LifeStage = lifeStage;
            Sex = sex;

            _feedings = new List<Feeding>();
            _molts = new List<Molt>();
        }

        public void Feed(int amount = 1, DateTimeOffset? feedingDate = null, string feeder = "")
        {
            if (amount < 1)
            {
                throw new CollectionDomainException(ExceptionMessages.FeedAmountCannotBeLowerThanOne);
            }

            // TODO: Provide DateTime.Now from external dependency
            _feedings.Add(new Feeding(feedingDate ?? DateTimeOffset.UtcNow, amount, feeder));
        }

        public void Molt(DateTimeOffset? moltingDate = null)
        {
            // TODO: Provide DateTime.Now from external dependency
            _molts.Add(new Molt(moltingDate ?? DateTimeOffset.UtcNow));
        }
    }
}
