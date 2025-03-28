using Amazon.SQS;
using Newtonsoft.Json;
using Serilog;
using Amazon.Runtime;
using Amazon.SQS.Model;
using Domain.ValueObject;
using Application.Common.Interface;
using Amazon;
using Microsoft.Extensions.Options;
using Domain.Helper;

namespace Infrastructure.AWS.SQS
{
    public class SQSService : ISQSService
    {
        private readonly ILogger _log = Log.ForContext<SQSService>();
        private readonly SQSSettings _sqs;
        public SQSService(IOptions<SQSSettings> options)
        {
            _sqs = options.Value;
        }
        public async Task<bool> SendMessageAsync(string message)
        {
            var credentials = new BasicAWSCredentials(_sqs.AccessKey, _sqs.SecretKey);
            using var sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast1);
            var request = new SendMessageRequest
            {
                QueueUrl = _sqs.QueueUrl,
                MessageBody = System.Text.Json.JsonSerializer.Serialize(message),                
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                    {
                        StringConst.TraceIdentifier, new MessageAttributeValue
                        {
                            DataType = "String", 
                            StringValue = TraceIdLogging.TraceId
                        } 
                    }
                }
            };
            var dataLog = "SendMessageAsync: \n" + message;
            try
            {
                var result = await sqsClient.SendMessageAsync(request);
                dataLog += "\nresult: \n" + JsonConvert.SerializeObject(result);
                _log.Information(dataLog);
                return result.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error: " + ex.Message + Environment.NewLine + ex.StackTrace);
                return false;
            }
        }
    //    public async Task<ReceiveMessageResponse> ReceiveMessageAsync(string queueUrl, string accessKey, string secretKey, string roleArn)
    //    {
    //        using var sqsClient = new AmazonSQSClient(new BasicAWSCredentials(accessKey, secretKey));
    //        ReceiveMessageRequest request = new ReceiveMessageRequest()
    //        {
    //            QueueUrl = queueUrl,
    //            WaitTimeSeconds = 20,
    //            MaxNumberOfMessages = 10,
    //            MessageAttributeNames = new List<string>() { StringConst.TraceIdentifier }
    //        };

    //        try
    //        {
    //            ReceiveMessageResponse result = await sqsClient.ReceiveMessageAsync(request);

    //            if (result.Messages != null && result.Messages.Count > 0)
    //            {
    //                var dataLog = "ReceiveMessageAsync:";
    //                dataLog += "\nresult: \n" + JsonConvert.SerializeObject(JsonConvert.SerializeObject(result));
    //                _log.Information(dataLog);
    //            }

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            _log.Error(ex, "Lỗi: " + ex.Message + Environment.NewLine + ex.StackTrace);
    //            return null;
    //        }
    //    }
    }
}
