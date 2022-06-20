using System;

namespace EightCare.Application.Collections.Queries.GetCollectionById
{
    // TODO: Finish up the collection to fetch all the data
    public class CollectionDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
