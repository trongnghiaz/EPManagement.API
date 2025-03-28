
namespace Domain.ValueObject
{
    public class SQSSettings
    {
        public string? QueueUrl { get; set; }
        public string? AccessKey { get; set; }
        public string? SecretKey { get; set; }
    }
}
