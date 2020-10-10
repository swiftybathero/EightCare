using EightCare.Domain.Common;
using System;

namespace EightCare.Domain.KeeperAggregate
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
