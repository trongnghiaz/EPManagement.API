using Application.Implements.Departments.Command.AddDepartment;
using Application.Implements.Departments.Command.DeleteDepartment;
using Application.Implements.Departments.Command.DeleteFromBin;
using Application.Implements.Departments.Command.RecycleDepartment;
using Application.Implements.Departments.Command.UpdateDepartment;
using Application.Implements.Departments.Query.GetDepartmentOptions;
using Application.Implements.Departments.Query.GetDetailDepartment.GetByIdQuery;
using Application.Implements.Departments.Query.GetListDepartment;
using Application.Implements.Departments.Query.GetListDepartmentDeleted;
using Domain.Enum;
using Infrastructure.Authentications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DepartmentController : ApiControllerBase
    {
        [Authorize]
        [HasPermission(Permission.ReadMember)]
        [HttpGet("list-options")]
        public async Task<IActionResult> ListOptions([FromQuery] GetDepartmentOptionsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [Authorize]
        [HasPermission(Permission.ReadMember)]
        [HttpGet("list-department")]
        public async Task<IActionResult> ListAll([FromQuery] GetListDepartmentQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles ="Admin, Manager")]
        [HasPermission(Permission.ReadMember)]
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await Mediator.Send(new GetByIdQuery(id));
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize]
        [HasPermission(Permission.CreateMember)]
        [HttpPost("creating")]
        public async Task<IActionResult> AddDepartment(AddDepartmentCommand command)
        {
            var result = await Mediator.Send(command);            
            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("list-department-deleted")]
        public async Task<IActionResult> ListDeletedDepartment([FromQuery]GetListDepartmentDeletedQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("recycle-department={id}")]
        public async Task<IActionResult> RecycleDepartment([FromRoute]Guid id)
        {
            var result = await Mediator.Send(new RecycleDepartmentCommand(id));
            return Ok(result);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HasPermission(Permission.UpdateMember)]
        [HttpPut("updating-id={id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateDepartmentCommand command)
        {            
            command.SetId(id);            
            var update = await Mediator.Send(command);            
            return Ok(update);
        }

        [Authorize]
        [HasPermission(Permission.DeleteMember)]
        [HttpDelete("deleting-id={id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var delete = await Mediator.Send(new DeleteDepartmentCommand(id));            
            return Ok(delete);
        }
        [Authorize(Roles ="Admin")]
        [HasPermission(Permission.DeleteMember)]
        [HttpDelete("deleting-from-bin-department-id={id}")]
        public async Task<IActionResult> DeleteFromBin([FromRoute] DeleteDepertmentFromBinCommand command)
        {
            var delete = await Mediator.Send(command);
            return Ok(delete);
        }

    }
}
