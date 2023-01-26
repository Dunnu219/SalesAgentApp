using Castle.Core.Resource;
using Dapper;
using Microsoft.Data.SqlClient;
using Moq;
using SalesAgentDataAccess.Classes;
using SalesAgentDataAccess.Interfaces;
using SalesAgentDomain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace SalesAgentUnitTest.SalesAgentDataAccess
{
    public class BookingSalesAgentRepositoryTests
    {
        private readonly Mock<ISalesAgentDbContext> mockSalesAgentDbContext = new();
        private readonly Mock<IDapperWrapper> mockDapperWrapper = new();
        private readonly string getSalesAgentAvailability = "GetSalesAgentAvailability";
        private readonly string createSalesAgentBooking = "CreateSalesAgentBooking";
        private readonly BookingSalesAgentRepository sut;

        public BookingSalesAgentRepositoryTests()
        {
            sut = new BookingSalesAgentRepository(mockSalesAgentDbContext.Object, mockDapperWrapper.Object);
        }

        [Fact]
        public async Task GetSalesAgentsReturnsDataWhenExists()
        {
            IList<SalesAgent> salesAgents = new List<SalesAgent>();

            SalesAgent salesAgent = new SalesAgent
            {
                AgentName = "PK",
                AgentId = 1
            };

            salesAgents.Add(salesAgent);

            mockDapperWrapper.Setup(dw =>
            dw.QueryAsync<SalesAgent>(It.IsAny<IDbConnection>(), It.IsAny<string>(), It.IsAny<DynamicParameters>(), It.Is<CommandType>(c => c == CommandType.Text)))
                .ReturnsAsync(salesAgents);

            //ACT
            var response = await sut.GetSalesAgents().ConfigureAwait(false);

            //ASSERT
            mockDapperWrapper.Verify(x => x.QueryAsync<SalesAgent>
                (
                    It.IsAny<IDbConnection>(),
                    It.IsAny<string>(),
                    It.IsAny<DynamicParameters>(),
                    CommandType.Text
                ), Times.Once);
            Assert.NotNull(response);
            Assert.IsType<List<SalesAgent>>(response);
            Assert.Equal(1, response.Count);
            response.Should().BeEquivalentTo(salesAgents);
        }

        [Fact]
        public async Task GetSalesAgentAvailabilityReturnsDataWhenExists()
        {
            IList<SalesAgentAvailability> salesAgentAvailabilityList = new List<SalesAgentAvailability>();

            AgentAvailabiltyFetchDto agentAvailabiltyFetchDto = new AgentAvailabiltyFetchDto
            {
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                AgentId = 1
            };

            SalesAgentAvailability salesAgentAvailability = new SalesAgentAvailability
            {
                AgentName = "PK",
                AgentId = 1,
                AvailableDateTime = DateTime.Today,
                Day = DayOfWeek.Monday,
                TimeSlotId = 19
            };

            salesAgentAvailabilityList.Add(salesAgentAvailability);

            mockDapperWrapper.Setup(dw => 
            dw.QueryAsync<SalesAgentAvailability>(It.IsAny<IDbConnection>(), It.Is<string>(s => s == getSalesAgentAvailability), It.IsAny<DynamicParameters>(), It.Is<CommandType>(c => c == CommandType.StoredProcedure)))
                .ReturnsAsync(salesAgentAvailabilityList);

            //ACT
            var response = await sut.GetSalesAgentAvailability(agentAvailabiltyFetchDto).ConfigureAwait(false);

            //ASSERT
            mockDapperWrapper.Verify(x => x.QueryAsync<SalesAgentAvailability>
                (
                    It.IsAny<IDbConnection>(),
                    getSalesAgentAvailability,
                    It.IsAny<DynamicParameters>(),
                    CommandType.StoredProcedure
                ), Times.Once);
            Assert.NotNull(response);
            Assert.IsType<List<SalesAgentAvailability>>(response);
            Assert.Equal(1, response.Count);
            response.Should().BeEquivalentTo(salesAgentAvailabilityList);
        }

        [Fact]
        public async Task GetSalesAgentAvailabilityReturnsNoDataWhenNotExists()
        {
            IList<SalesAgentAvailability> salesAgentAvailabilityList = new List<SalesAgentAvailability>();

            AgentAvailabiltyFetchDto agentAvailabiltyFetchDto = new AgentAvailabiltyFetchDto
            {
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                AgentId = 1
            };

            mockDapperWrapper.Setup(dw =>
            dw.QueryAsync<SalesAgentAvailability>(It.IsAny<IDbConnection>(), It.Is<string>(s => s == getSalesAgentAvailability), It.IsAny<DynamicParameters>(), It.Is<CommandType>(c => c == CommandType.StoredProcedure)))
                .ReturnsAsync(salesAgentAvailabilityList);

            //ACT
            var response = await sut.GetSalesAgentAvailability(agentAvailabiltyFetchDto).ConfigureAwait(false);

            //ASSERT
            mockDapperWrapper.Verify(x => x.QueryAsync<SalesAgentAvailability>
                (
                    It.IsAny<IDbConnection>(),
                    getSalesAgentAvailability,
                    It.IsAny<DynamicParameters>(),
                    CommandType.StoredProcedure
                ), Times.Once);
            Assert.NotNull(response);
            Assert.IsType<List<SalesAgentAvailability>>(response);
            Assert.Equal(0, response.Count);
            response.Should().BeEquivalentTo(salesAgentAvailabilityList);
        }

        [Fact]
        public async Task CreateSalesAgentBookingReturnsBookingId()
        {
            BookingCreationDto bookingCreationDto = new BookingCreationDto
            {
                BookingDateTime = DateTime.Today,
                BookedBy = "Pradeep",
                AgentId = 1,
                TimeSlotId=23,
                BookingMessage= "new Booking"
            };

            IList<int> bookingId = new List<int> {1};

            mockDapperWrapper.Setup(dw =>
            dw.QueryAsync<int>(It.IsAny<IDbConnection>(), It.Is<string>(s => s == createSalesAgentBooking), It.IsAny<DynamicParameters>(), It.Is<CommandType>(c => c == CommandType.StoredProcedure)))
                .ReturnsAsync(bookingId);

            //ACT
            var response = await sut.CreateSalesAgentBooking(bookingCreationDto).ConfigureAwait(false);

            //ASSERT
            mockDapperWrapper.Verify(x => x.QueryAsync<int>
                (
                    It.IsAny<IDbConnection>(),
                    createSalesAgentBooking, 
                    It.IsAny<DynamicParameters>(),
                    CommandType.StoredProcedure
                ), Times.Once);
            Assert.IsType<int>(response);
            Assert.Equal(1, response);
        }
    }
}
