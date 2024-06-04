using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Features.Commands.Roles.CreateRole;
using ETicaretAPI.Application.Features.Commands.Roles.DeleteRole;
using ETicaretAPI.Application.Features.Commands.Roles.UpdateRole;
using ETicaretAPI.Application.Features.Queries.Roles.GetRoleById;
using ETicaretAPI.Application.Features.Queries.Roles.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class RoleController : ControllerBase
    {
        readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Reading, Definition = "Get Roles")]
        public async Task<IActionResult> GetRoles([FromQuery]GetRolesQueryRequest getRolesQueryRequest)
        {
         GetRolesQueryResponse response=   await _mediator.Send(getRolesQueryRequest);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Reading, Definition = "Get Role By Id")]
        public async Task<IActionResult> GetRoleById([FromRoute]GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
           GetRoleByIdQueryResponse response=await _mediator.Send(getRoleByIdQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Writing, Definition = "Create Role")]
        public async Task<IActionResult> CreateRole([FromBody]CreateRoleCommandRequest createRoleCommandRequest)
        {
            CreateRoleCommandResponse response=await _mediator.Send(createRoleCommandRequest);
            return Ok(response);
        } 
        [HttpDelete("{name}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Deleting, Definition = "Delete Roles")]
        public async Task<IActionResult> DeleteRole([ FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            DeleteRoleCommandResponse response=await _mediator.Send(deleteRoleCommandRequest);
            return Ok(response);
        } 
        [HttpPut("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Updating, Definition = "Update Roles")]
        public async Task<IActionResult> UpdateRole([FromBody,FromRoute]UpdateRoleCommandRequest updateRoleCommandRequest)
        {
             UpdateRoleCommandResponse response=  await  _mediator.Send(updateRoleCommandRequest);
            return Ok(response);
        }
    }
}
