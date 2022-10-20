using System;

namespace ClockifyTimeTrackerBE.Domain.Models
{
    public class Receipt
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BoughtAt { get; set; }
        public string Shop { get; set; }
        public string Type { get; set; }
        public Guid BlobId { get; set; }
        public Guid UserId { get; set; }
    }
}