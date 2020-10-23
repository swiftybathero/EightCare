using AutoFixture;
using EightCare.Domain.KeeperAggregate;
using System;
using System.Linq;

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
                var createdAnimal = _keeper.AddNewAnimal(_fixture.Create<string>(), _fixture.Create<string>());
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