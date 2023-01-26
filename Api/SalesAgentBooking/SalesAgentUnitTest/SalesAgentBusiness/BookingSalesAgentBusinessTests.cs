using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SalesAgentBusiness.Classes;
using SalesAgentDataAccess.Interfaces;
using SalesAgentDomain.Models;

namespace SalesAgentUnitTest.SalesAgentBusiness
{
    public class BookingSalesAgentBusinessTests
    {
        private readonly Mock<IBookingSalesAgentRepository> mockBookingSalesAgentRepository = new();
        private readonly Mock<ILogger<BookingSalesAgentBusiness>> mockLogger = new();
        private readonly BookingSalesAgentBusiness bookingSalesAgentBusiness;

        public BookingSalesAgentBusinessTests()
        {
            bookingSalesAgentBusiness = new BookingSalesAgentBusiness(mockBookingSalesAgentRepository.Object, mockLogger.Object);
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

            mockBookingSalesAgentRepository.Setup(b => b.GetSalesAgents())
                .ReturnsAsync(salesAgents);

            //ACT
            var response = await bookingSalesAgentBusiness.GetSalesAgents().ConfigureAwait(false);

            //ASSERT
            mockLogger.Verify(x => x.Log
                  (
                      LogLevel.Information,
                      It.IsAny<EventId>(),
                      It.Is<It.IsAnyType>((v, t) => v.ToString().Equals("GetSalesAgentsBusiness: Received the request")),
                      It.IsAny<Exception>(),
                      (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                  ),
                  Times.Once);
            Assert.NotNull(response);
            Assert.IsType<List<SalesAgent>>(response);
            Assert.Equal(1, response.Count);
            response.Should().BeEquivalentTo(salesAgents);
        }

        [Fact]
        public async Task GetSalesAgentAvailabilityReturnsData()
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

            mockBookingSalesAgentRepository.Setup(b=> b.GetSalesAgentAvailability(agentAvailabiltyFetchDto))
                .ReturnsAsync(salesAgentAvailabilityList);

            //ACT
            var response = await bookingSalesAgentBusiness.GetSalesAgentAvailability(agentAvailabiltyFetchDto).ConfigureAwait(false);

            //ASSERT
            mockLogger.Verify(x => x.Log
                  (
                      LogLevel.Information,
                      It.IsAny<EventId>(),
                      It.Is<It.IsAnyType>((v, t) => v.ToString().StartsWith("GetSalesAgentAvailabilityBusiness: Received")),
                      It.IsAny<Exception>(),
                      (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                  ),
                  Times.Once);
            Assert.NotNull(response);
            Assert.IsType<List<SalesAgentAvailability>>(response);
            Assert.Equal(1, response.Count);
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
                TimeSlotId = 23,
                BookingMessage = "new Booking"
            };

           mockBookingSalesAgentRepository.Setup(b => b.CreateSalesAgentBooking(bookingCreationDto))
                .ReturnsAsync(1);

            //ACT
            var response = await bookingSalesAgentBusiness.CreateSalesAgentBooking(bookingCreationDto).ConfigureAwait(false);

            //ASSERT
            mockLogger.Verify(x => x.Log
                  (
                      LogLevel.Information,
                      It.IsAny<EventId>(),
                      It.Is<It.IsAnyType>((v, t) => v.ToString().StartsWith("CreateSalesAgentBookingBusiness: Received")),
                      It.IsAny<Exception>(),
                      (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                  ),
                  Times.Once);
            Assert.IsType<int>(response);
            Assert.Equal(1, response);
        }
    }
}
