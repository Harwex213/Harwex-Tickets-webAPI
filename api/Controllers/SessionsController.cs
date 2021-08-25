using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Controllers.Abstract;
using api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Service.Models.Seat;
using Service.Models.Session;
using Service.Services;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ExceptionHandlerController
    {
         private readonly ISessionsService _sessionsService;

        public SessionsController(ISessionsService sessionsService)
        {
            _sessionsService = sessionsService;
        }

        // GET: api/Sessions
        [HttpGet]
        public ActionResult<IEnumerable<SessionResponseModel>> GetSessions(long? cinemaId, long? movieId)
        {
            try
            {
                return Ok(_sessionsService.GetAll(cinemaId, movieId));
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }
        
        // GET: api/Sessions/5
        [HttpGet("{id:long}")]
        public ActionResult<SessionResponseModel> GetSession(long id)
        {
            try
            {
                var sessionResponseModel = _sessionsService.Get(id);
                if (sessionResponseModel == null)
                {
                    return NotFound(new ErrorResponse("Not Found"));
                }

                return Ok(sessionResponseModel);
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }
        
        // GET: api/Sessions/5/Seats
        [HttpGet("{id:long}/seats")]
        public ActionResult<IEnumerable<SeatResponseModel>> GetSessionFreeSeats(long id)
        {
            try
            {
                return Ok(_sessionsService.GetFreeSeats(id));
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // POST: api/Sessions
        // [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<CreateSessionResponseModel>> PostSession(
            [FromBody] CreateSessionModel createSessionModel)
        {
            try
            {
                var sessionResponseModel = await _sessionsService.AddAsync(createSessionModel);
                return CreatedAtAction(nameof(GetSession), new {id = sessionResponseModel.Id},
                    sessionResponseModel);
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // PUT: api/Sessions/5
        // [Authorize(Roles = "admin")]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<SuccessResponse>> PutSession(long id,
            [FromBody] UpdateSessionModel updateSessionModel)
        {
            try
            {
                if (id != updateSessionModel.Id)
                {
                    return BadRequest(new ErrorResponse("Id must match"));
                }

                await _sessionsService.UpdateAsync(updateSessionModel);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // DELETE: api/Sessions/5
        // [Authorize(Roles = "admin")]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<SuccessResponse>> DeleteSession(long id)
        {
            try
            {
                await _sessionsService.DeleteAsync(id);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }
    }
}