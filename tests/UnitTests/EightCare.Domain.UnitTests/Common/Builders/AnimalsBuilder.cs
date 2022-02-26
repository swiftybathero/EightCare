using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using EightCare.Domain.Entities;
using EightCare.Domain.ValueObjects;

namespace EightCare.Domain.UnitTests.Common.Builders
{
    public class AnimalsBuilder : IAnimalsBuilder
    {
        private readonly IFixture _fixture;
        private Guid[] _animalIds;

        public AnimalsBuilder()
        {
            _fixture = new Fixture();
        }

        public IAnimalsBuilder WithIds(params Guid[] animalIds)
        {
            if (animalIds.Length == 0)
            {
                throw new ArgumentNullException(nameof(animalIds));
            }

            _animalIds = animalIds;

            return this;
        }

        public List<Animal> Build()
        {
            return _animalIds is not null ? BuildFromIds(_animalIds) : BuildDefault();
        }

        private List<Animal> BuildFromIds(IEnumerable<Guid> animalIds)
        {
            var animals = new List<Animal>();

            foreach (var animalId in animalIds)
            {
                var animal = new Animal
                (
                    Species.From(_fixture.Create<string>(), _fixture.Create<string>()),
                    _fixture.Create<DateTime>(),
                    _fixture.Create<int>()
                );

                animal.SetId(animalId);
                animals.Add(animal);
            }

            return animals;
        }

        private List<Animal> BuildDefault()
        {
            return _fixture.CreateMany<Animal>().ToList();
        }
    }

    public interface IAnimalsBuilder
    {
        IAnimalsBuilder WithIds(params Guid[] animalIds);
    }
}
