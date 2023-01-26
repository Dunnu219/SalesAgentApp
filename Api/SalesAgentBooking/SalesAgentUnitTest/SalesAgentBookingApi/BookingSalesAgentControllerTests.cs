using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using SalesAgentBooking.Controllers;
using SalesAgentBusiness.Classes;
using SalesAgentBusiness.Interfaces;
using SalesAgentDataAccess.Interfaces;
using SalesAgentDomain.Models;
using System.Net;

namespace SalesAgentUnitTest.SalesAgentBookingApi
{
    public class BookingSalesAgentControllerTests
    {
        private readonly Mock<IBookingSalesAgentBusiness> mockBookingSalesAgentBusiness = new();
        private readonly Mock<ILogger<BookingSalesAgentController>> mockLogger = new();
        private readonly BookingSalesAgentController controller;

        public BookingSalesAgentControllerTests()
        {
            controller = new BookingSalesAgentController(mockBookingSalesAgentBusiness.Object, mockLogger.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
        }

        [Fact]
        public async Task GetSalesAgents_DoesLog_RequestReceived()
        {
            // ACT
            await controller.GetSalesAgents().ConfigureAwait(false);

            // ASSERT
            mockLogger.Verify(
                x => x.Log
                (
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Equals("GetSalesAgent: Received the request for getting Sales Agents")),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                ),
                Times.Once);
        }

        [Fact]
        public async Task GetSalesAgentAvailability_WhenAgentAvailabiltyFetchDtoIsNull_ReturnsBadRequest()
        {
            // ACT
            var result = await controller.GetSalesAgentAvailability(null).ConfigureAwait(false);

            // ASSERT
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, ((BadRequestObjectResult)result).StatusCode);

            mockLogger.Verify(
                x => x.Log
                (
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(string.Format("GetSalesAgentAvailability: {0} Request is inappropriate", nameof(AgentAvailabiltyFetchDto)))),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                ),
                Times.Once);
        }

        [Fact]
        public async Task GetSalesAgentAvailability_WhenAgentAvailabiltyFetchDtoStateIsInvalid_ReturnsBadRequest()
        {
            controller.ControllerContext.ModelState.AddModelError("sampleField", "sampleError");
            // ACT
            var result = await controller.GetSalesAgentAvailability(null).ConfigureAwait(false);

            // ASSERT
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, ((BadRequestObjectResult)result).StatusCode);

            mockLogger.Verify(
                x => x.Log
                (
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(string.Format("GetSalesAgentAvailability: {0} Request is inappropriate", nameof(AgentAvailabiltyFetchDto)))),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                ),
                Times.Once);
        }


        [Fact]
        public async Task GetSalesAgentAvailability_DoesLog_RequestReceived()
        {
            // ACT
            await controller.GetSalesAgentAvailability(new AgentAvailabiltyFetchDto()).ConfigureAwait(false);

            // ASSERT
            mockLogger.Verify(
                x => x.Log
                (
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(string.Format("GetSalesAgentAvailability: Received the {0}", nameof(AgentAvailabiltyFetchDto)))),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                ),
                Times.Once);
        }

        [Fact]
        public async Task CreateSalesAgentBooking_WhenBookingCreationDtoIsNull_ReturnsBadRequest()
        {
            // ACT
            var result = await controller.CreateSalesAgentBooking(null).ConfigureAwait(false);

            // ASSERT
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, ((BadRequestObjectResult)result).StatusCode);

            mockLogger.Verify(
                x => x.Log
                (
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(string.Format("CreateSalesAgentBooking: {0} Request is inappropriate", nameof(BookingCreationDto)))),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                ),
                Times.Once);
        }

        [Fact]
        public async Task CreateSalesAgentBooking_WhenAgentBookingCreationDtoStateIsInvalid_ReturnsBadRequest()
        {
            controller.ControllerContext.ModelState.AddModelError("sampleField", "sampleError");
            // ACT
            var result = await controller.CreateSalesAgentBooking(null).ConfigureAwait(false);

            // ASSERT
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, ((BadRequestObjectResult)result).StatusCode);

            mockLogger.Verify(
                x => x.Log
                (
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(string.Format("CreateSalesAgentBooking: {0} Request is inappropriate", nameof(BookingCreationDto)))),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                ),
                Times.Once);
        }


        [Fact]
        public async Task CreateSalesAgentBooking_DoesLog_RequestReceived()
        {
            // ACT
            await controller.CreateSalesAgentBooking(new BookingCreationDto()).ConfigureAwait(false);

            // ASSERT
            mockLogger.Verify(
                x => x.Log
                (
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(string.Format("CreateSalesAgentBooking: Received the {0}", nameof(BookingCreationDto)))),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                ),
                Times.Once);
        }

        [Fact]
        public async Task GetSalesAgentsReturnsData()
        {
            IList<SalesAgent> salesAgents = new List<SalesAgent>();
            SalesAgent salesAgent = new SalesAgent
            {
                AgentName = "PK",
                AgentId = 1
            };

            salesAgents.Add(salesAgent);

            mockBookingSalesAgentBusiness.Setup(b => b.GetSalesAgents())
                .ReturnsAsync(salesAgents);

            //ACT
            var response = await controller.GetSalesAgents().ConfigureAwait(false);

            //ASSERT
            mockLogger.Verify(x => x.Log
                  (
                      LogLevel.Information,
                      It.IsAny<EventId>(),
                      It.Is<It.IsAnyType>((v, t) => v.ToString().Equals("GetSalesAgent: Received the request for getting Sales Agents")),
                      It.IsAny<Exception>(),
                      (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                  ),
                  Times.Once);
            Assert.IsType<OkObjectResult>(response as OkObjectResult);
            var items = Assert.IsType<List<SalesAgent>>(((OkObjectResult)response).Value);
            Assert.Single(items);
            items.Should().SatisfyRespectively(
                first =>
                {
                    first.AgentName.Should().Be("PK");
                    first.AgentId.Should().Be(1);
                });
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
                TimeSlotId = 19,
            };

            salesAgentAvailabilityList.Add(salesAgentAvailability);

            mockBookingSalesAgentBusiness.Setup(b => b.GetSalesAgentAvailability(agentAvailabiltyFetchDto))
                .ReturnsAsync(salesAgentAvailabilityList);

            //ACT
            var response = await controller.GetSalesAgentAvailability(agentAvailabiltyFetchDto).ConfigureAwait(false);

            //ASSERT
            mockLogger.Verify(x => x.Log
                  (
                      LogLevel.Information,
                      It.IsAny<EventId>(),
                      It.Is<It.IsAnyType>((v, t) => v.ToString().StartsWith("GetSalesAgentAvailability: Received")),
                      It.IsAny<Exception>(),
                      (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                  ),
                  Times.Once);
            Assert.IsType<OkObjectResult>(response as OkObjectResult);
            var items = Assert.IsType<List<SalesAgentAvailability>>(((OkObjectResult)response).Value);
            Assert.Single(items);
            items[0].Should().BeEquivalentTo(salesAgentAvailability);
        }

        [Fact]
        public async Task CreateSalesAgentBookingReturnBookingId()
        {
            BookingCreationDto bookingCreationDto = new BookingCreationDto
            {
                BookingDateTime = DateTime.Today,
                BookedBy = "Pradeep",
                AgentId = 1,
                TimeSlotId = 23,
                BookingMessage = "new Booking"
            };

            mockBookingSalesAgentBusiness.Setup(b => b.CreateSalesAgentBooking(bookingCreationDto))
                 .ReturnsAsync(1);

            //ACT
            var response = await controller.CreateSalesAgentBooking(bookingCreationDto).ConfigureAwait(false);

            //ASSERT
            mockLogger.Verify(x => x.Log
                  (
                      LogLevel.Information,
                      It.IsAny<EventId>(),
                      It.Is<It.IsAnyType>((v, t) => v.ToString().StartsWith("CreateSalesAgentBooking: Received")),
                      It.IsAny<Exception>(),
                      (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                  ),
                  Times.Once);
            Assert.IsType<OkObjectResult>(response as OkObjectResult);
            var bookingId = Assert.IsType<int>(((OkObjectResult)response).Value);
            Assert.Equal(1,bookingId);
        }
    }
}