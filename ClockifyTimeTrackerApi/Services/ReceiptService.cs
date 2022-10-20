using System;
using System.Threading.Tasks;
using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Domain.Repositories;
using ClockifyTimeTrackerBE.Domain.Services;
using ClockifyTimeTrackerBE.Domain.Services.Communication;
using ClockifyTimeTrackerBE.Resources;

namespace ClockifyTimeTrackerBE.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReceiptService(IReceiptRepository receiptRepository, IUnitOfWork unitOfWork)
        {
            _receiptRepository = receiptRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReceiptResponse> FindAsync(Guid id)
        {
            try
            {
                var receipt = await _receiptRepository.GetById(id);
                return new ReceiptResponse(receipt);
            }
            catch (Exception ex)
            {
                return new ReceiptResponse($"An error occurred when finding the receipt: {ex.Message}");
            }
        }

        public async Task<ReceiptsResponse> FindAsync(ReceiptFilters filters)
        {
            try
            {
                var receipts = await _receiptRepository.Find(filters);
                return new ReceiptsResponse(receipts);
            }
            catch (Exception ex)
            {
                return new ReceiptsResponse($"An error occurred when finding the receipts: {ex.Message}");
            }
        }

        public async Task<ReceiptResponse> AddAsync(Receipt receipt)
        {
            try
            {
                _receiptRepository.Add(receipt);
                await _unitOfWork.Commit();

                return new ReceiptResponse(receipt);
            }
            catch (Exception ex)
            {
                return new ReceiptResponse($"An error occurred when saving the receipt: {ex.Message}");
            }
        }
    }
}