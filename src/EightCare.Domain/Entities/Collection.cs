using System;
using System.Collections.Generic;
using System.Linq;
using EightCare.Domain.Common;
using EightCare.Domain.Enums;
using EightCare.Domain.Exceptions;
using EightCare.Domain.Properties;
using EightCare.Domain.ValueObjects;

namespace EightCare.Domain.Entities
{
    public sealed class Collection : Entity, IAggregateRoot
    {
        private readonly List<Animal> _animals;

        // TODO: Change to UserId OwnerId
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<Animal> Animals => _animals.AsReadOnly();

        public Collection(Guid userId, string name)
        {
            UserId = userId;
            Name = name;

            _animals = new List<Animal>();
        }

        // TODO: Returning Animal here for Unit Test purposes only - will fix only after ID generation place change
        public Animal AddNewAnimal(string scientificName, string commonName, string animalName, DateTimeOffset received,
            LifeStage lifeStage, Sex sex)
        {
            var newAnimal = new Animal(Species.From(scientificName, commonName), animalName, received, lifeStage, sex);

            _animals.Add(newAnimal);

            return newAnimal;
        }

        public void FeedAnimal(Guid animalId, int amount = 1, DateTimeOffset? feedingDate = null)
        {
            var animalToFeed = FindAnimalById(animalId);

            if (animalToFeed is null)
            {
                throw new CollectionDomainException(string.Format(ExceptionMessages.AnimalNotFound, animalId));
            }

            animalToFeed.Feed(amount, feedingDate);
        }

        public void ReportMolt(Guid animalId, DateTimeOffset? moltingDate = null)
        {
            var moltingAnimal = FindAnimalById(animalId);

            if (moltingAnimal is null)
            {
                throw new CollectionDomainException(string.Format(ExceptionMessages.AnimalNotFound, animalId));
            }

            moltingAnimal.Molt(moltingDate);
        }

        private Animal? FindAnimalById(Guid animalId)
        {
            return _animals.FirstOrDefault(x => x.Id == animalId);
        }
    }
}