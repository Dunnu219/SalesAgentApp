using SalesAgentDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAgentBusiness.Interfaces
{
    public interface IBookingSalesAgentBusiness
    {
        public Task<IList<SalesAgentAvailability>> GetSalesAgentAvailability(AgentAvailabiltyFetchDto agentAvailabiltyFetchDto);

        public Task<int> CreateSalesAgentBooking(BookingCreationDto bookingDetails);

        public Task<IList<SalesAgent>> GetSalesAgents();
    }
}
