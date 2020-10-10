using EightCare.Domain.Common;
using System;

namespace EightCare.Domain.KeeperAggregate
{
    public sealed class Feeding : Entity
    {
        public DateTime Date { get; private set; }
        public Feed Feed { get; private set; }
        public int Count { get; private set; }

        public Feeding(DateTime date, Feed feed, int count)
        {
            Date = date;
            Feed = feed;
            Count = count;
        }
    }
}
