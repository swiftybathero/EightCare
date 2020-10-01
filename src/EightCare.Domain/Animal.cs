namespace EightCare.Domain
{
    public sealed class Animal
    {
        public string CommonName { get; }
        public string ScientificName { get; }

        public Animal(string commonName, string scientificName)
        {
            CommonName = commonName;
            ScientificName = scientificName;
        }
    }
}