using System.Collections.Generic;

namespace api.ViewModel
{
    public class SuccessResponse
    {
        public string Message { get; set; } = "Success";
    }
    public class ErrorResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; }

        public ErrorResponse(string errorMessage = "Internal error.") : this(new List<string> { errorMessage }) { }

        public ErrorResponse(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}