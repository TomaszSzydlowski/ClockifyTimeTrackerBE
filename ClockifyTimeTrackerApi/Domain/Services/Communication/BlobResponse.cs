using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Resources;

namespace ClockifyTimeTrackerBE.Domain.Services.Communication
{
    public class BlobResponse : BaseResponse
    {
        public Blob Blob { get; private set; }

        private BlobResponse(bool success, string message, Blob blob) : base(success, message)
        {
            Blob = blob;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="blob">Saved blob.</param>
        /// <returns>Response.</returns>
        public BlobResponse(Blob blob) : this(true, string.Empty, blob)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public BlobResponse(string message) : this(false, message, null)
        { }
    }
}