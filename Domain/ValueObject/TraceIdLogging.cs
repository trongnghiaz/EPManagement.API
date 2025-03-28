namespace Domain.ValueObject
{
    public class TraceIdLogging
    {
        private static AsyncLocal<string> _traceId = new();
        public static string TraceId => _traceId.Value!;
        public static bool TrySetTraceId(string traceId)
        {
            if (traceId == null) return false;
            _traceId.Value = traceId;
            return true;
        }
    }
}
