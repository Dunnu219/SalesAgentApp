using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesAgentBusiness.Interfaces;
using SalesAgentDomain.Models;

namespace SalesAgentBooking.Controllers
{

    [Route("api/salesagentbooking")]
    [ApiController]
    public class BookingSalesAgentController : Controller
    {
        private readonly IBookingSalesAgentBusiness bookingSalesAgentBusiness;
        private readonly ILogger<BookingSalesAgentController> logger;

        public BookingSalesAgentController(IBookingSalesAgentBusiness bookingSalesAgentBusiness, ILogger<BookingSalesAgentController> logger)
        {
            this.bookingSalesAgentBusiness = bookingSalesAgentBusiness;
            this.logger = logger;
        }

        [HttpGet("getsalesagents")]
        public async Task<IActionResult> GetSalesAgents()
        {
            try
            {
                logger.LogInformation("GetSalesAgent: Received the request for getting Sales Agents");

                var salesAgents = await bookingSalesAgentBusiness.GetSalesAgents();
                return Ok(salesAgents);
            }
            catch (Exception ex)
            {
                logger.LogError("GetSalesAgent: Error occured while processing the request. Error: {0} ", ex);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { LoggerMessage = "GetSalesAgent: Error occured while processing the request." });
            }
        }

        [HttpGet("getsalesagentavailability")]
        public async Task<IActionResult> GetSalesAgentAvailability([FromQuery] AgentAvailabiltyFetchDto agentAvailabiltyFetchDto)
        {
            if (agentAvailabiltyFetchDto == null || !ModelState.IsValid)
            {
                logger.LogError("GetSalesAgentAvailability: {0} Request is inappropriate. CorrelationId= {1}. Errors: {2}",
                    nameof(AgentAvailabiltyFetchDto), agentAvailabiltyFetchDto?.CorrelationId, new
                    {
                        messages = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                return BadRequest(ModelState);
            }

            try
            {
                logger.LogInformation("GetSalesAgentAvailability: Received the {0}. CorrelationId: {1}", nameof(AgentAvailabiltyFetchDto), agentAvailabiltyFetchDto.CorrelationId);

                var salesAgentAvailability = await bookingSalesAgentBusiness.GetSalesAgentAvailability(agentAvailabiltyFetchDto);
                return Ok(salesAgentAvailability);
            }
            catch (Exception ex)
            {
                logger.LogError("GetSalesAgentAvailability: Error occured while processing the {0} request : {1}. Error: {2}", nameof(AgentAvailabiltyFetchDto),
                    JsonConvert.SerializeObject(agentAvailabiltyFetchDto), ex);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { LoggerMessage = string.Format("Error occured while processing the {0} request. CorrelationId: {1}", nameof(AgentAvailabiltyFetchDto), agentAvailabiltyFetchDto?.CorrelationId) });
            }
        }

        [HttpPost("createsalesagentbooking")]
        public async Task<IActionResult> CreateSalesAgentBooking([FromBody] BookingCreationDto bookingDetails)
        {
            if (bookingDetails == null || !ModelState.IsValid)
            {
                logger.LogError("CreateSalesAgentBooking: {0} Request is inappropriate. CorrelationId= {1}. Errors: {2}",
                    nameof(BookingCreationDto), bookingDetails?.CorrelationId, new
                    {
                        messages = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                return BadRequest(ModelState);
            }
            try
            {
                logger.LogInformation("CreateSalesAgentBooking: Received the {0}. CorrelationId: {1}", nameof(BookingCreationDto), bookingDetails.CorrelationId);
                var bookingId = await bookingSalesAgentBusiness.CreateSalesAgentBooking(bookingDetails);
                return Ok(bookingId);
            }
            catch (Exception ex)
            {
                logger.LogError("CreateSalesAgentBooking: Error occured while processing the {0} request : {1}. Error: {2}", nameof(BookingCreationDto),
                    JsonConvert.SerializeObject(bookingDetails), ex);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { LoggerMessage = string.Format("Error occured while processing the {0} request. CorrelationId: {1}", nameof(BookingCreationDto), bookingDetails?.CorrelationId) });
            }
        }
    }
}
