using System;
using EightCare.Domain.Common;

namespace EightCare.Domain.Entities
{
    public sealed class Molt : Entity
    {
        public DateTimeOffset Date { get; private set; }

        public Molt(DateTimeOffset date)
        {
            Date = date;
        }
    }
}
