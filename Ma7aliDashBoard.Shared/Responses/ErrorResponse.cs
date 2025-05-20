using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7aliDashBoard.Shared.Responses
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string[] Details { get; set; }

        public ErrorResponse(string errorCode, string message, int statusCode, string[]? details = null)
        {
            ErrorCode = errorCode;
            Message = message;
            StatusCode = statusCode;
            Details = details ?? Array.Empty<string>();
        }
    }

}
