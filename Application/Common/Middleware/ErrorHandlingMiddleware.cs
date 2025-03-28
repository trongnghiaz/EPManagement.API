using Domain.ValueObject;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Common.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            var code = 500;
            var message = "Đã xảy ra lỗi trong quá trình xử lý";
            switch (exception)
            {
                case UnauthorizedAccessException ex:
                    code = (int)HttpStatusCode.Unauthorized;
                    message = ex.Message;
                    break;
                case BadHttpRequestException ex:
                    code = (int)HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;
                case TimeoutException ex:
                    code = (int)HttpStatusCode.RequestTimeout;
                    message = ex.Message;
                    break;
                case HttpStatusException ex:
                    code = ex.StatusCode;
                    message = ex.Message;
                    break;
            }
            _logger.LogError(exception, exception.Message);
            context.Response.ContentType = "text/plan";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(message);
        }
    }
}
