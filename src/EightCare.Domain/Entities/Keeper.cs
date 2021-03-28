using System;
using System.Collections.Generic;
using System.Linq;
using EightCare.Domain.Common;
using EightCare.Domain.Exceptions;
using EightCare.Domain.Properties;

namespace EightCare.Domain.Entities
{
    public sealed class Keeper : Entity, IAggregateRoot
    {
        private readonly List<Animal> _animals;

        public string Name { get; }
        public string Email { get; }
        public int Age { get; }
        public IReadOnlyCollection<Animal> Animals => _animals.AsReadOnly();

        public Keeper(string name, string email, int age)
        {
            Name = name;
            Email = email;
            Age = age;

            _animals = new List<Animal>();
        }

        public Animal AddNewAnimal(string scientificName, string commonName, DateTime buyDate, int buyAge)
        {
            var newAnimal = new Animal(scientificName, commonName, buyDate, buyAge);

            _animals.Add(newAnimal);

            return newAnimal;
        }

        public void FeedAnimal(Guid animalId, int amount = 1, DateTime? feedingDate = null)
        {
            var animalToFeed = FindAnimalById(animalId);

            if (animalToFeed == null)
            {
                throw new KeeperDomainException(string.Format(ExceptionMessages.AnimalNotFound, animalId));
            }

            animalToFeed.Feed(amount, feedingDate);
        }

        public void ReportMolt(Guid animalId, DateTime? moltingDate = null)
        {
            var moltingAnimal = FindAnimalById(animalId);

            if (moltingAnimal == null)
            {
                throw new KeeperDomainException(string.Format(ExceptionMessages.AnimalNotFound, animalId));
            }

            moltingAnimal.Molt(moltingDate);
        }

        private Animal FindAnimalById(Guid animalId)
        {
            return _animals.FirstOrDefault(x => x.Id == animalId);
        }
    }
}