using Ma7aliDashBoard.Shared.Exceptions;
using Ma7aliDashBoard.Shared.Responses;
using System.Net;
using System.Text.Json;

namespace Ma7aliDashBoard.Api.Middlewares
{
  
        public class GlobalExceptionMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<GlobalExceptionMiddleware> _logger;

            public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception occurred");
                    await HandleExceptionAsync(context, ex);
                }
            }

            private static Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                ErrorResponse errorResponse;
                HttpStatusCode statusCode;

                switch (exception)
                {
                    case ApiException apiEx:
                        statusCode = HttpStatusCode.BadRequest;
                        errorResponse = new ErrorResponse(
                            errorCode: "BAD_REQUEST",
                            message: apiEx.Message,
                            statusCode: (int)statusCode
                        );
                        break;

                    case KeyNotFoundException notFoundEx:
                        statusCode = HttpStatusCode.NotFound;
                        errorResponse = new ErrorResponse(
                            errorCode: "NOT_FOUND",
                            message: notFoundEx.Message,
                            statusCode: (int)statusCode
                        );
                        break;

                    case UnauthorizedAccessException unauthorizedEx:
                        statusCode = HttpStatusCode.Unauthorized;
                        errorResponse = new ErrorResponse(
                            errorCode: "UNAUTHORIZED",
                            message: unauthorizedEx.Message,
                            statusCode: (int)statusCode
                        );
                        break;

                    default:
                        statusCode = HttpStatusCode.InternalServerError;
                        errorResponse = new ErrorResponse(
                            errorCode: "INTERNAL_SERVER_ERROR",
                            message: "An unexpected error occurred.",
                            statusCode: (int)statusCode,
                            details: new[] { exception.Message }
                        );
                        break;
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                var result = JsonSerializer.Serialize(errorResponse);
                return context.Response.WriteAsync(result);
            }
        }

    }

