using EightCare.Domain.Common;
using System.Collections.Generic;

namespace EightCare.Domain.KeeperAggregate
{
    public sealed class Keeper : Entity
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

        public void AddNewAnimal(string scientificName, string commonName)
        {
            var newAnimal = new Animal(scientificName, commonName);

            _animals.Add(newAnimal);
        }
    }
}
