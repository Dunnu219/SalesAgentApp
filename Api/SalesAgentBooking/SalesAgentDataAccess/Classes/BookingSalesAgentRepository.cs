using Dapper;
using SalesAgentDataAccess.Interfaces;
using SalesAgentDomain.Models;
using System.Data;
using static Azure.Core.HttpHeader;

namespace SalesAgentDataAccess.Classes
{
    public class BookingSalesAgentRepository : IBookingSalesAgentRepository
    {
        private readonly ISalesAgentDbContext salesAgentDbContext;
        private readonly IDapperWrapper dapperWrapper;
        public BookingSalesAgentRepository(ISalesAgentDbContext salesAgentDbContext, IDapperWrapper dapperWrapper)
        {
            this.salesAgentDbContext = salesAgentDbContext;
            this.dapperWrapper = dapperWrapper;
        }

        public async Task<IList<SalesAgentAvailability>> GetSalesAgentAvailability(AgentAvailabiltyFetchDto agentAvailabiltyFetchDto)
        {
            var procedureName = "GetSalesAgentAvailability";
            var parameters = new DynamicParameters();
            parameters.Add("StartDate", agentAvailabiltyFetchDto.StartDate.ToDateTime(TimeOnly.MinValue), DbType.Date, ParameterDirection.Input);
            parameters.Add("EndDate", agentAvailabiltyFetchDto.EndDate.ToDateTime(TimeOnly.MaxValue), DbType.Date, ParameterDirection.Input);
            parameters.Add("AgentId", agentAvailabiltyFetchDto.AgentId, DbType.Int32, ParameterDirection.Input);

            using var connection = salesAgentDbContext.CreateConnection();
            var salesAgentAvailability = await dapperWrapper.QueryAsync<SalesAgentAvailability>(connection, procedureName, parameters, CommandType.StoredProcedure);

            return salesAgentAvailability.ToList();
        }

        public async Task<int> CreateSalesAgentBooking(BookingCreationDto bookingDetails)
        {
            var procedureName = "CreateSalesAgentBooking";
            var parameters = new DynamicParameters();
            parameters.Add("@BookingDate", bookingDetails.BookingDateTime.ToUniversalTime().Date, DbType.Date, ParameterDirection.Input);
            parameters.Add("@BookedBy", bookingDetails.BookedBy, DbType.String, ParameterDirection.Input);
            parameters.Add("@AgentId", bookingDetails.AgentId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@TimeSlotId", bookingDetails.TimeSlotId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@BookingMessage", bookingDetails.BookingMessage, DbType.String, ParameterDirection.Input);

            using var connection = salesAgentDbContext.CreateConnection();
            var bookingId = await dapperWrapper.QueryAsync<int>(connection, procedureName, parameters, commandType: CommandType.StoredProcedure);

            return bookingId.FirstOrDefault();
        }

        public async Task<IList<SalesAgent>> GetSalesAgents()
        {
            var query = "SELECT FirstName + ' ' + LastName As AgentName, AgentId FROM SalesAgent";
            var parameters = new DynamicParameters();
            using var connection = salesAgentDbContext.CreateConnection();
            var salesAgents = await dapperWrapper.QueryAsync<SalesAgent>(connection,query, parameters, CommandType.Text);
            return salesAgents.ToList();
        }
    }
}