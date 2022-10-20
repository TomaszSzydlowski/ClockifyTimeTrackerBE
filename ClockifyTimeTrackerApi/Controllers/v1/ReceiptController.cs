using System;
using System.Threading.Tasks;
using AutoMapper;
using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Domain.Services;
using ClockifyTimeTrackerBE.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClockifyTimeTrackerBE.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ReceiptController : Controller
    {
        private readonly IReceiptService _receiptService;
        private readonly IMapper _mapper;

        public ReceiptController(IReceiptService receiptService, IMapper mapper)
        {
            _receiptService = receiptService;
            _mapper = mapper;
        }
        
        //GET: api/receipt
        [HttpGet]
        [ProducesResponseType(typeof(ReceiptResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> GetAsync([FromQuery] ReceiptFilters filters)
        {
            var result = await _receiptService.FindAsync(filters);
            
            if (!result.Success)
                return BadRequest(new ErrorResource(result.Message));
            
            // var receiptResource = _mapper.Map<Receipt, ReceiptResource>(result.Receipts);
            return Ok(result);
        }

        //GET: api/receipt/2
        [HttpGet("{receiptId:guid}")]
        [ProducesResponseType(typeof(ReceiptResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> GetAsync(Guid receiptId)
        {
            var result = await _receiptService.FindAsync(receiptId);

            if (!result.Success)
                return BadRequest(new ErrorResource(result.Message));

            var receiptResource = _mapper.Map<Receipt, ReceiptResource>(result.Receipt);
            return Ok(receiptResource);
        }

        //POST:api/receipt
        [HttpPost]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveReceiptResource resource)
        {
            var receipt = _mapper.Map<SaveReceiptResource, Receipt>(resource);
            var result = await _receiptService.AddAsync(receipt);

            if (!result.Success)
                return BadRequest(new ErrorResource(result.Message));

            return Ok(result.Receipt.Id);
        }
    }
}