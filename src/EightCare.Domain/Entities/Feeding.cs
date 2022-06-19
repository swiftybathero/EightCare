using System;
using EightCare.Domain.Common;

namespace EightCare.Domain.Entities
{
    public sealed class Feeding : Entity
    {
        public DateTimeOffset Date { get; private set; }
        public int Amount { get; private set; }
        public string Feeder { get; private set; }

        public Feeding(DateTimeOffset date, int amount, string feeder)
        {
            Date = date;
            Amount = amount;
            Feeder = feeder;
        }
    }
}
