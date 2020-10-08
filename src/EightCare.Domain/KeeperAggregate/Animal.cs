using EightCare.Domain.Common;
using EightCare.Domain.Exceptions;

namespace EightCare.Domain.KeeperAggregate
{
    public sealed class Animal : Entity
    {
        public string ScientificName { get; private set; }
        public string CommonName { get; private set; }

        public Animal(string scientificName, string commonName)
        {
            SetScientificName(scientificName);
            SetCommonName(commonName);
        }

        private void SetScientificName(string scientificName)
        {
            if (string.IsNullOrEmpty(scientificName))
                throw new KeeperDomainException("Scientific Name cannot be empty.");

            ScientificName = scientificName;
        }

        private void SetCommonName(string commonName)
        {
            CommonName = commonName;
        }
    }
}
