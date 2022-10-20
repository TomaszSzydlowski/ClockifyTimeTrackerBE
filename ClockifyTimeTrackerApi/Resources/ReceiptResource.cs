using System;

namespace ClockifyTimeTrackerBE.Resources
{
    public class ReceiptResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BoughtAt { get; set; }
        public string Shop { get; set; }
        public string Type { get; set; }
        public Guid BlobId { get; set; }
        public Guid UserId { get; set; }
    }
}