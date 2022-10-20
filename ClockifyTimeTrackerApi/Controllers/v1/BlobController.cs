using System;
using System.Threading.Tasks;
using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Domain.Services;
using ClockifyTimeTrackerBE.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClockifyTimeTrackerBE.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BlobController:Controller
    {
        private readonly IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost("fileUpload")]
        [ProducesResponseType(typeof(Guid), 201)]
        public IActionResult Upload(IFormFile file)
        {
            var blobId= _blobService.Upload(file);
            return Ok(blobId);
        }

        [HttpGet("{blobId:guid}")]
        [ProducesResponseType(typeof(Blob), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> GetAsync(Guid blobId)
        {
            var result = await _blobService.GetSasUrl(blobId);

            if (!result.Success)
                return BadRequest(new ErrorResource(result.Message));
            
            return Ok(result.Blob);
        }
    }
}