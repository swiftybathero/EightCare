using System;
using System.Collections.Generic;
using AutoFixture;
using EightCare.Domain.Entities;

namespace EightCare.Domain.UnitTests.Common.Builders
{
    public class CollectionBuilder
    {
        private readonly string _name;
        private readonly string _email;
        private readonly int _age;
        private List<Animal> _animals = new();

        private CollectionBuilder()
        {
            IFixture fixture = new Fixture();

            _name = fixture.Create<string>();
            _email = fixture.Create<string>();
            _age = fixture.Create<int>();
        }

        public static CollectionBuilder GivenCollection()
        {
            return new CollectionBuilder();
        }

        public CollectionBuilder WithAnimals(Action<IAnimalsBuilder> customizeAnimals = null)
        {
            var animalsBuilder = new AnimalsBuilder();
            customizeAnimals?.Invoke(animalsBuilder);

            _animals = animalsBuilder.Build();

            return this;
        }

        public Collection Build()
        {
            var collection = new Collection(_name, _email, _age);

            foreach (var animal in _animals)
            {
                var createdAnimal = collection.AddNewAnimal
                (
                    animal.Species.ScientificName, 
                    animal.Species.CommonName, 
                    animal.BuyDate, 
                    animal.BuyAge
                );
                createdAnimal.SetId(animal.Id);
            }

            return collection;
        }
    }
}