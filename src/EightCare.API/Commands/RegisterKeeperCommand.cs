using MediatR;
using System;

namespace EightCare.API.Commands
{
    public class RegisterKeeperCommand : IRequest<Guid>
    {
        public string Name { get; }
        public string Email { get; }
        public int Age { get; }

        public RegisterKeeperCommand(string name, string email, int age)
        {
            Name = name;
            Email = email;
            Age = age;
        }
    }
}
