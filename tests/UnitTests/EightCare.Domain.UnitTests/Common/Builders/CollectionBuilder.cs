using System;
using System.Linq;
using AutoFixture;
using EightCare.Domain.Entities;

namespace EightCare.Domain.UnitTests.Common.Builders
{
    public class CollectionBuilder
    {
        private readonly IFixture _fixture;
        private Collection _collection;

        public CollectionBuilder()
        {
            _fixture = new Fixture();
        }

        public CollectionBuilder Build(string name, string email, int age)
        {
            _collection = new Collection(name, email, age);

            return this;
        }

        public CollectionBuilder BuildDefault()
        {
            _collection = CreateDefaultCollection();

            return this;
        }

        public Collection Create()
        {
            return _collection ?? CreateDefaultCollection();
        }

        public CollectionBuilder WithAnimals(params Guid[] animalIds)
        {
            if (animalIds.Length == 0)
            {
                animalIds = _fixture.CreateMany<Guid>().ToArray();
            }
            
            foreach (var animalId in animalIds)
            {
                var scientificName = _fixture.Create<string>();
                var commonName = _fixture.Create<string>();
                var buyDate = _fixture.Create<DateTime>();
                var buyAge = _fixture.Create<int>();

                var createdAnimal = _collection.AddNewAnimal(scientificName, commonName, buyDate, buyAge);
                createdAnimal.SetId(animalId);
            }

            return this;
        }

        private Collection CreateDefaultCollection()
        {
            return _fixture.Create<Collection>();
        }
    }
}