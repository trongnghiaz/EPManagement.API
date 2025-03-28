using Domain.Helper;
using Domain.ValueObject;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Middleware
{
    public class TracingMiddleware
    {
        private const string _traceIdHeader = StringConst.TraceIdentifier;
        private readonly RequestDelegate _next;
        public TracingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(_traceIdHeader, out var traceId))
            {
                context.TraceIdentifier = traceId!;
                TraceIdLogging.TrySetTraceId(traceId!);
            }
            else
            {
                TraceIdLogging.TrySetTraceId(context.TraceIdentifier);
            }                
            context.Response.Headers.Add(_traceIdHeader, TraceIdLogging.TraceId);
            await _next(context);
        }
    }
}
