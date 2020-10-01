namespace EightCare.Domain
{
    public sealed class Keeper
    {
        public string Name { get; }
        public string Email { get; }
        public int Age { get; }

        public Keeper(string name, string email, int age)
        {
            Name = name;
            Email = email;
            Age = age;
        }
    }
}