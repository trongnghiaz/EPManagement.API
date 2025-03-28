using Domain.Helper;
using Domain.ValueObject;

namespace Application.Common.DelegateHandler
{
    public class TracingHttpMessageHandler : DelegatingHandler
    {
        private const string _traceIdHeader = StringConst.TraceIdentifier;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.TryGetValues(_traceIdHeader, out var traceIdValue))
            {
                var traceId = TraceIdLogging.TraceId;
                request.Headers.Add(_traceIdHeader, traceId);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
