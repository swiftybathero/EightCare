using System;

namespace EightCare.API.Models
{
    public class KeeperModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
