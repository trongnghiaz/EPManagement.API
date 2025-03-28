using Domain.Entities;

namespace Application.Common.Interface
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateJwtToken(Employees employees);
        Task<string> DeGenerate(string auth, string claimType);
    }
}
