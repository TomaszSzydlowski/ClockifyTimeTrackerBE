using System;
using System.Threading.Tasks;
using ClockifyTimeTrackerBE.Domain.Services.Communication;
using Microsoft.AspNetCore.Http;

namespace ClockifyTimeTrackerBE.Domain.Services
{
    public interface IBlobService
    {
        Guid Upload(IFormFile formFile);
        Task<BlobResponse> GetSasUrl(Guid blobId);
    }
}