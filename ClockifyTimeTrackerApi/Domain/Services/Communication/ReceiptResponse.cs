using ClockifyTimeTrackerBE.Domain.Models;

namespace ClockifyTimeTrackerBE.Domain.Services.Communication
{
    public class ReceiptResponse : BaseResponse
    {
        public Receipt Receipt { get; private set; }

        private ReceiptResponse(bool success, string message, Receipt receipt) : base(success, message)
        {
            Receipt = receipt;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="receipt">Saved receipt.</param>
        /// <returns>Response.</returns>
        public ReceiptResponse(Receipt receipt) : this(true, string.Empty, receipt)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ReceiptResponse(string message) : this(false, message, null)
        { }
    }
}