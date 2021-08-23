using System;
using api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;

namespace api.Controllers.Abstract
{
    public abstract class ExceptionHandlerController : ControllerBase
    {
        public ActionResult AnalyzeException(Exception exception)
        {
            return exception switch
            {
                NotFoundException => NotFound(new ErrorResponse(exception.Message)),
                ConflictException => Conflict(new ErrorResponse(exception.Message)),
                UnauthorizedException => Unauthorized(new ErrorResponse(exception.Message)),
                _ => BadRequest(new ErrorResponse())
            };
        }
    }
}