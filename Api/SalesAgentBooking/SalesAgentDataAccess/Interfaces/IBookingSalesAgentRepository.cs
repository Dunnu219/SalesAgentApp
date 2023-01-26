using SalesAgentDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAgentDataAccess.Interfaces
{
    public interface IBookingSalesAgentRepository
    {
        public Task<IList<SalesAgentAvailability>> GetSalesAgentAvailability(AgentAvailabiltyFetchDto agentAvailabiltyFetchDto);

        public Task<int> CreateSalesAgentBooking(BookingCreationDto bookingDetails);

        public Task<IList<SalesAgent>> GetSalesAgents();
    }
}
