using System.Collections.Generic;

namespace EightCare.Domain
{
    public sealed class Keeper
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

        public void AddAnimal(Animal animal)
        {
            _animals.Add(animal);
        }
    }
}