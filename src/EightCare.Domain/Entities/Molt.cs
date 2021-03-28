using System;
using EightCare.Domain.Common;

namespace EightCare.Domain.Entities
{
    public sealed class Molt : Entity
    {
        public DateTime Date { get; private set; }

        public Molt(DateTime date)
        {
            Date = date;
        }
    }
}
