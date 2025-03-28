using Application.Common.Interface;
using Domain.Entities;
using Domain.ValueObject;
using Infrastructure.Authentications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Persistence.JwtGenerator
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _settings;
        private readonly IPermissionService _permissionService;
        private readonly IManageReadDbContext _readDbContext;        
        public JwtTokenGenerator(IOptions<JwtSettings> options ,IPermissionService permissionService, IManageReadDbContext readDbContext)
        {            
            _readDbContext = readDbContext;
            _settings = options.Value;
            _permissionService = permissionService;
        }
        public async Task<string> GenerateJwtToken(Employees employees)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            HashSet<string> permissions = await _permissionService.GetPermissionsAsync(employees.EmployeeId);
            var roles = await (from r in _readDbContext.Roles
                       join er in _readDbContext.EmployeeRoles on r.Id equals er.RolesId
                       join e in _readDbContext.Employees on er.EmployeesEmployeeId equals employees.EmployeeId
                       select r.Name).FirstOrDefaultAsync();

            var userClaim = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, employees.EmployeeId.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, employees.EmployeeName),
                new Claim(JwtRegisteredClaimNames.Email, employees.Email),                
                new Claim("departmentId", employees.DepartmentId.ToString()),
                new Claim("role", roles)
            };         
            foreach (string permission in permissions)
            {
                userClaim.Add(new Claim(CustomClaims.Permissions, permission));
            }            
            var token = new JwtSecurityToken
                (
                    issuer: _settings.Issuer,
                    audience: _settings.Audience,
                    claims: userClaim,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<string> DeGenerate(string auth, string claimType)
        {
            var jwt = auth.Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var jwttoken = handler.ReadJwtToken(jwt);
            var type = jwttoken.Claims.First(claim => claim.Type == claimType).Value;
            return type;
        }
    }
}
