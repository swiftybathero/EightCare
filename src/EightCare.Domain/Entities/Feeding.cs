using System;
using EightCare.Domain.Common;

namespace EightCare.Domain.Entities
{
    public sealed class Feeding : Entity
    {
        public DateTime Date { get; private set; }
        public int Amount { get; private set; }

        public Feeding(DateTime date, int amount)
        {
            Date = date;
            Amount = amount;
        }
    }
}
