using Application.Common.Interface;
using Application.Common.Model;
using Application.Implements.Employee.Command.AddEmployee;
using Application.Implements.Employee.Command.DeleteEmployee;
using Application.Implements.Employee.Command.DeleteFromBin;
using Application.Implements.Employee.Command.RecycleEmployee;
using Application.Implements.Employee.Command.UpdateEmployee;
using Application.Implements.Employee.Query.GetDetailEmployee.GetById;
using Application.Implements.Employee.Query.GetListByUser;
using Application.Implements.Employee.Query.GetListEmployee;
using Application.Implements.Employee.Query.ListDeleted;
using Domain.Enum;
using Infrastructure.Authentications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class EmployeeController : ApiControllerBase
    {
        
        private readonly IJwtTokenGenerator _jwt;
        
        public EmployeeController( IJwtTokenGenerator jwt)
        {                              
            _jwt = jwt;
        }

        //[HttpPost]
        //public async Task<IActionResult> AutoPost()
        //{
        //    for (int i = 0; i <= 50; i++)
        //    {

        //        await _manageWriteDbContext.Employees.AddAsync(new Domain.Entities.Employees
        //        {
        //            EmployeeName = "Auto User" + i,
        //            Gender = Domain.Enum.Gender.Nam,
        //            DateOfBirth = DateTime.Now,
        //            PhoneNumber = "012345678" + i,
        //            Address = "Auto Adress" + i,
        //            Email = $"autouser{i}@gmail.com",
        //            Password = BCrypt.Net.BCrypt.HashPassword(i + "auto1234"),
        //            IsActive = true,
        //            DepartmentId = Guid.Parse("3518d828-f959-47e6-b95e-2523f8060373"),

        //        });

        //    }
        //    await _manageWriteDbContext.SaveChangesAsync();
        //    return Ok();
        //}

        [Authorize]
        [HasPermission(Permission.ReadMember)]
        [HttpGet("list-employee")]
        public async Task<IActionResult> GetListEmployee([FromQuery] ListEmployeeRequest request)
        {
            string auth = Request.Headers.Authorization.ToString();
            Task<string> taskstring = _jwt.DeGenerate(auth, CustomClaims.DepartmentId);
            string stringId = await taskstring;
            Task<string> roles = _jwt.DeGenerate(auth, CustomClaims.ClaimRole);
            string role = await roles;
            var departmentId = Guid.Parse(stringId); 
            if (role == "Admin" | role == "Manager")
            {
                var data = await Mediator.Send(new GetListEmployeeQuery(request.searchTerm, request.sortGender, request.sortActive, request.sortDepartment, request.page, request.pageSize));
                return Ok(data);
            }
            else
            {
                var result = await Mediator.Send(new GetListByUserQuery(request.searchTerm, request.sortGender, request.sortActive, departmentId, request.page, request.pageSize));
                return Ok(result);
            }            
        }        

        [Authorize(Roles = "Manager, Admin")]
        [HasPermission(Permission.ReadMember)]
        [HttpGet("employee-id={id}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {                                  
            var employee = await Mediator.Send(new GetById(id));                       
            return Ok(employee);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("employee-deleted")]
        public async Task<IActionResult> GetEmployeeDeleted([FromQuery]ListEmployeeDeletedQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("recycle-employee-id={id}")]
        public async Task<IActionResult> RecycleEmployee([FromRoute]Guid id, [FromBody]RecycleEmployeeCommand command)
        {
            command.SetId(id);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HasPermission(Permission.CreateMember)]
        [HttpPost("creating")]
        public async Task<IActionResult> CreateEmployee (AddEmployeeCommand command)
        {            
            var creating = await Mediator.Send(command);            
            return Ok(creating);
        }

        [Authorize]
        [HasPermission(Permission.UpdateMember)]
        [HttpPut("updating-id={id}")]
        public async Task<IActionResult> UpdateInfo([FromRoute] Guid id, [FromBody]UpdateEmployeeCommand command)
        {            
            command.SetId(id);            
            var update = await Mediator.Send(command);            
            return Ok(update);
        }

        [Authorize]
        [HasPermission(Permission.DeleteMember)]
        [HttpDelete("deleting-id={id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {            
            var delete = await Mediator.Send(new DeleteEmployeeCommand(id));            
            return Ok(delete);
        }

        [Authorize]
        [HasPermission(Permission.DeleteMember)]
        [HttpDelete("deleting-from-bin-employee-id={id}")]
        public async Task<IActionResult> DeleteFromDb([FromRoute] DeleteEmployeeFromBinCommand command)
        {
            var delete = await Mediator.Send(command);
            return Ok(delete);
        }
    }
}
