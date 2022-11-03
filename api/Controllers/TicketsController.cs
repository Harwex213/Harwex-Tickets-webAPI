using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Controllers.Abstract;
using api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Models.Ticket;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ExceptionHandlerController
    {
        private readonly ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        // GET: api/Tickets
        [HttpGet]
        public ActionResult<IEnumerable<TicketResponseModel>> GetTickets()
        {
            try
            {
                return Ok(_ticketsService.GetAll());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // GET: api/Tickets/5
        [HttpGet("{id:long}")]
        public ActionResult<TicketResponseModel> GetTicket(long id)
        {
            try
            {
                var ticketResponseModel = _ticketsService.Get(id);
                if (ticketResponseModel == null)
                {
                    return NotFound(new ErrorResponse("Not Found"));
                }

                return Ok(ticketResponseModel);
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // POST: api/Tickets
        [Authorize(Roles = "user, admin")]
        [HttpPost]
        public async Task<ActionResult<CreateTicketResponseModel>> PostTicket(
            [FromBody] CreateTicketModel createTicketModel)
        {
            try
            {
                var ticketResponseModel = await _ticketsService.AddAsync(createTicketModel);
                return CreatedAtAction(nameof(GetTicket), new {id = ticketResponseModel.Id},
                    ticketResponseModel);
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // PUT: api/Tickets/5
        [Authorize(Roles = "user, admin")]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<SuccessResponse>> PutTicket(long id,
            [FromBody] UpdateTicketModel updateTicketModel)
        {
            try
            {
                if (id != updateTicketModel.Id)
                {
                    return BadRequest(new ErrorResponse("Id must match"));
                }

                await _ticketsService.UpdateAsync(updateTicketModel);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // DELETE: api/Tickets/5
        [Authorize(Roles = "user, admin")]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<SuccessResponse>> DeleteTicket(long id)
        {
            try
            {
                await _ticketsService.DeleteAsync(id);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }
    }
}