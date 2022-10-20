using System.Collections.Generic;
using ClockifyTimeTrackerBE.Domain.Models;

namespace ClockifyTimeTrackerBE.Domain.Services.Communication
{
    public class ReceiptsResponse : BaseResponse
    {
        public List<Receipt> Receipts { get; private set; }

        private ReceiptsResponse(bool success, string message, List<Receipt> receipts) : base(success, message)
        {
            Receipts = receipts;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="receipt">Saved receipt.</param>
        /// <returns>Response.</returns>
        public ReceiptsResponse(List<Receipt> receipts) : this(true, string.Empty, receipts)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ReceiptsResponse(string message) : this(false, message, null)
        { }
    }
}