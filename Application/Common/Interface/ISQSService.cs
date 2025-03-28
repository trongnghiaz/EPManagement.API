
namespace Application.Common.Interface
{
    public interface ISQSService
    {
        Task<bool> SendMessageAsync(string message);
    }
}
