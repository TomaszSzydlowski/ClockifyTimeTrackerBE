using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Domain.Services;
using ClockifyTimeTrackerBE.Domain.Services.Communication;
using ClockifyTimeTrackerBE.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ServiceStack;

namespace ClockifyTimeTrackerBE.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        private readonly IConfiguration _configuration;

        public BlobService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
            
            var containerName = _configuration.GetSection("Storage:ContainerName").Value;
            _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        }

        public Guid Upload(IFormFile formFile)
        {

            var blobId = Guid.NewGuid();
            var extension = formFile.FileName.GetExtension();
            var blobClient = _containerClient.GetBlobClient(blobId + extension);

            //TODO: Good example for searching controller
            // var test = containerClient.GetBlobs().FirstOrDefault(b => b.Name.Contains("f1e75cb9-13c9-46b4-b522-8b5cc2c4fe37"));

            using var stream = formFile.OpenReadStream();
            blobClient.Upload(stream, true);

            return blobId;
        }

        public async Task<BlobResponse> GetSasUrl(Guid blobId)
        {
            try
            {
                var blob = _containerClient.GetBlobs().FirstOrDefault(b => b.Name.Contains(blobId.ToString()));
                var blobClient = _containerClient.GetBlobClient(blob?.Name);
                var blobSasUrl = GetServiceSasUriForBlob(blobClient);
                return new BlobResponse(new Blob(){SasUrl = blobSasUrl.ToString()});
            }
            catch (Exception ex)
            {
                return new BlobResponse($"An error occurred when creating sas blob url: {ex.Message}");
            }
        }
        
        private static Uri GetServiceSasUriForBlob(BlobClient blobClient,
            string storedPolicyName = null)
        {
            
            
            // Check whether this BlobClient object has been authorized with Shared Key.
            if (blobClient.CanGenerateSasUri)
            {
                // Create a SAS token that's valid for one hour.
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                    BlobName = blobClient.Name,
                    Resource = "b"
                };

                if (storedPolicyName == null)
                {
                    sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
                    sasBuilder.SetPermissions(BlobSasPermissions.Read);
                }
                else
                {
                    sasBuilder.Identifier = storedPolicyName;
                }

                Uri sasUri = blobClient.GenerateSasUri(sasBuilder);

                return sasUri;
            }

            Console.WriteLine(@"BlobClient must be authorized with Shared Key 
                          credentials to create a service SAS.");
            return null;
        }
    }
}