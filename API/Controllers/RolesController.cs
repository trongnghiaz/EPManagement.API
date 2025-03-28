using Application.Implements.Role.RoleCommand.AddRole;
using Application.Implements.Role.RoleCommand.AssignPermission;
using Application.Implements.Role.RoleCommand.ChangeRole;
using Application.Implements.Role.RoleCommand.RemoveAccess;
using Application.Implements.Role.RoleCommand.RemoveRolePermission;
using Application.Implements.Role.RoleQuery.GetEmployeeRoles;
using Application.Implements.Role.RoleQuery.GetRolePermission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]        
    public class RolesController : ApiControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("list-employee-role")]
        public async Task<IActionResult> GetListEmployeeRoles([FromQuery]EmployeeRolesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        //[Authorize(Roles = "Admin")]
        //[HttpGet("list-role")]
        //public async Task<IActionResult> GetListRoles([FromQuery]ListRoleQuery query)
        //{
        //    var result = await Mediator.Send(query);
        //    return Ok(result);
        //}
        [Authorize(Roles = "Admin")]
        [HttpGet("list-role-permission")]
        public async Task<IActionResult> GetListPermission([FromQuery] GetRolePermissionQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("grant-access-user")]
        public async Task<IActionResult> SeedRoles([FromBody]RolesCommand command)
        {            
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("change-role={id}")]
        public async Task<IActionResult> ChangeRole([FromRoute]Guid id, [FromBody] UpdateRoleCommand update)
        {
            update.SetId(id);
            var result = await Mediator.Send(update);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("assign-permission-role={id}")]
        public async Task<IActionResult> AssignPermission([FromRoute]int id, [FromBody] AssignPermissionCommand command)
        {
            command.SetId(id);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("remove-permission-role={id}")]
        public async Task<IActionResult> RemoveRolePermission ([FromRoute]int id, [FromBody]RemoveRolePermissionCommand remove)
        {
            remove.SetId(id);
            var result = await Mediator.Send(remove);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("remove-access={id}")]
        public async Task<IActionResult> RemoveAccess([FromRoute] Guid id)
        {            
            var result = await Mediator.Send(new RemoveAccessCommand(id));
            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]
        //[HttpGet("permission-list")]
        //public async Task<IActionResult> ListPermissions()
        //{
        //    var result = await Mediator.Send(new GetListPermissionQuery());
        //    return Ok(result);
        //}
    }
}
