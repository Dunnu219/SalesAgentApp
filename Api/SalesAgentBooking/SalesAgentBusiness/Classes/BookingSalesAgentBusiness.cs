using Microsoft.Extensions.Logging;
using SalesAgentBusiness.Interfaces;
using SalesAgentDataAccess.Interfaces;
using SalesAgentDomain.Models;
using System.Globalization;

namespace SalesAgentBusiness.Classes
{
    public class BookingSalesAgentBusiness : IBookingSalesAgentBusiness
    {
        private readonly IBookingSalesAgentRepository bookingSalesAgentRepository;
        private readonly ILogger<BookingSalesAgentBusiness> logger;

        public BookingSalesAgentBusiness(IBookingSalesAgentRepository bookingSalesAgentRepository, ILogger<BookingSalesAgentBusiness> logger)
        {
            this.bookingSalesAgentRepository = bookingSalesAgentRepository;
            this.logger = logger;
        }

        public async Task<IList<SalesAgentAvailability>> GetSalesAgentAvailability(AgentAvailabiltyFetchDto agentAvailabiltyFetchDto)
        {
            logger.LogInformation("GetSalesAgentAvailabilityBusiness: Received the {0}. CorrelationId: {1}", nameof(AgentAvailabiltyFetchDto), agentAvailabiltyFetchDto.CorrelationId);

            var salesAgentAvailability = await bookingSalesAgentRepository.GetSalesAgentAvailability(agentAvailabiltyFetchDto);
            return salesAgentAvailability;
        }

        public async Task<int> CreateSalesAgentBooking(BookingCreationDto bookingDetails)
        {
            logger.LogInformation("CreateSalesAgentBookingBusiness: Received the {0}. CorrelationId: {1}", nameof(BookingCreationDto), bookingDetails.CorrelationId);

            var bookingId = await bookingSalesAgentRepository.CreateSalesAgentBooking(bookingDetails);

            return bookingId;
        }

        public async Task<IList<SalesAgent>> GetSalesAgents()
        {
            logger.LogInformation("GetSalesAgentsBusiness: Received the request");
            var salesAgents = await bookingSalesAgentRepository.GetSalesAgents();

            return salesAgents;
        }
    }
}