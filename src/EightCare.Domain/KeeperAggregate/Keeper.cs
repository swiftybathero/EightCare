using EightCare.Domain.Common;
using EightCare.Domain.Exceptions;
using EightCare.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EightCare.Domain.KeeperAggregate
{
    public sealed class Keeper : Entity, IAggregateRoot
    {
        private readonly List<Animal> _animals;

        public string Name { get; private set; }
        public string Email { get; private set; }
        public int Age { get; private set; }
        public IReadOnlyCollection<Animal> Animals => _animals.AsReadOnly();

        public Keeper(string name, string email, int age)
        {
            Name = name;
            Email = email;
            Age = age;

            _animals = new List<Animal>();
        }

        public Animal AddNewAnimal(string scientificName, string commonName)
        {
            var newAnimal = new Animal(scientificName, commonName);

            _animals.Add(newAnimal);

            return newAnimal;
        }

        public void FeedAnimal(int animalId, int amount = 1, DateTime? feedingDate = null)
        {
            var animalToFeed = FindAnimalById(animalId);

            if (animalToFeed == null)
                throw new KeeperDomainException(string.Format(ExceptionMessages.AnimalNotFound, animalId));

            animalToFeed.Feed(amount, feedingDate);
        }

        private Animal FindAnimalById(int animalId)
        {
            return _animals.FirstOrDefault(x => x.Id == animalId);
        }
    }
}
