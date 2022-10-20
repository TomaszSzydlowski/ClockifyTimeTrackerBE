using System;
using System.ComponentModel.DataAnnotations;

namespace ClockifyTimeTrackerBE.Resources
{
    public class SaveReceiptResource
    {
        [Required] public string Name { get; set; }
        [Required] public string BoughtAt { get; set; }
        [Required] public string Shop { get; set; }
        [Required] public string Type { get; set; }
        [Required] public Guid BlobId { get; set; }
        [Required] public Guid UserId { get; set; }
    }
}