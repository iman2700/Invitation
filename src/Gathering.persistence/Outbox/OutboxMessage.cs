﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gathering.persistence.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; set; }
        public DateTime OccurredOnUtx { get; set; }
        public DateTime? ProcessedOnUtc { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;    
        public string? Error { get; set; }
    }
}
