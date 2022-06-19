using System.Collections.Generic;
using EightCare.Domain.Common;
using EightCare.Domain.Exceptions;
using EightCare.Domain.Properties;

namespace EightCare.Domain.ValueObjects
{
    public class Species : ValueObject
    {
        public string ScientificName { get; private set; }
        public string CommonName { get; private set; }

        private Species(string scientificName, string commonName)
        {
            ScientificName = scientificName;
            CommonName = commonName;
        }

        public static Species From(string scientificName, string commonName)
        {
            if (string.IsNullOrEmpty(scientificName))
            {
                throw new CollectionDomainException(ExceptionMessages.ScientificNameCannotBeEmpty);
            }

            return new Species(scientificName, commonName);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return ScientificName;
            yield return CommonName;
        }
    }
}
