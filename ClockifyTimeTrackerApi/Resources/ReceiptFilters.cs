#nullable enable
using System;

namespace ClockifyTimeTrackerBE.Resources
{
    public class ReceiptFilters
    {
        public string? Name { get; set; }
        public Guid UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Shop { get; set; }
        public string? Type { get; set; }

    }
}