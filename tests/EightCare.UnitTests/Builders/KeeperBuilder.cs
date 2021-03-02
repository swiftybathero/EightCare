using AutoFixture;
using System;
using System.Linq;
using EightCare.Domain.Entities;

namespace EightCare.UnitTests.Builders
{
    public class KeeperBuilder
    {
        private readonly IFixture _fixture;
        private Keeper _keeper;

        public KeeperBuilder()
        {
            _fixture = new Fixture();
        }

        public KeeperBuilder Build(string name, string email, int age)
        {
            _keeper = new Keeper(name, email, age);

            return this;
        }

        public KeeperBuilder BuildDefault()
        {
            _keeper = CreateDefaultKeeper();

            return this;
        }

        public Keeper Create()
        {
            return _keeper ?? CreateDefaultKeeper();
        }

        public KeeperBuilder WithAnimals(params Guid[] animalIds)
        {
            if (animalIds.Length == 0)
                animalIds = _fixture.CreateMany<Guid>().ToArray();

            foreach (var animalId in animalIds)
            {
                var scientificName = _fixture.Create<string>();
                var commonName = _fixture.Create<string>();
                var buyDate = _fixture.Create<DateTime>();
                var buyAge = _fixture.Create<int>();

                var createdAnimal = _keeper.AddNewAnimal(scientificName, commonName, buyDate, buyAge);
                createdAnimal.SetId(animalId);
            }

            return this;
        }

        private Keeper CreateDefaultKeeper()
        {
            return _fixture.Create<Keeper>();
        }
    }
}