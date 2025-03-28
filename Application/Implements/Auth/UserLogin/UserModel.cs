
using Domain.Enum;

namespace Application.Implements.Auth.UserLogin
{
    public record UserModel(Guid id,
                            string? name,
                            string? avatarUrl,
                            Gender gender,
                            DateTime dateOfBirth,
                            string? phoneNumber,
                            string? address,
                            string? email,
                            string? department,
                            string role);    
}
