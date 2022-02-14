using System;

namespace EightCare.Application.Collections.Queries.GetCollectionById
{
    public class CollectionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
