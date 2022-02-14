using System;

namespace EightCare.Application.Collections.Queries.GetCollectionById
{
    public class CollectionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
