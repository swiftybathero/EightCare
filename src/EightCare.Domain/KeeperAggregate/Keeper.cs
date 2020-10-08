using EightCare.Domain.Common;
using System.Collections.Generic;

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

        public void AddNewAnimal(string scientificName, string commonName)
        {
            var newAnimal = new Animal(scientificName, commonName);

            _animals.Add(newAnimal);
        }
    }
}
