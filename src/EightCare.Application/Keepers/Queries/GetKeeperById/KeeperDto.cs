﻿using System;

namespace EightCare.Application.Keepers.Queries.GetKeeperById
{
    public class KeeperDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
