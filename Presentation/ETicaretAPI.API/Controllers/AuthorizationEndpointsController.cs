using ETicaretAPI.Application.Features.Commands.AuthorizationEndpoints;
using ETicaretAPI.Application.Features.Queries.AuthorizationEndpoints.GetRolesToEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignRoleEndpointController : ControllerBase
    {
        readonly IMediator _mediator;

        public AssignRoleEndpointController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpPost("action")]

        public async Task<IActionResult> GetRolesToEndpoint([FromRoute]GetRolesToEndpointQueryRequest getRolesToEndpointrequest )
        {
          GetRolesToEndpointQueryResponse response=  await _mediator.Send(getRolesToEndpointrequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest assignRoleEndpointCommandrequest)
        {
            assignRoleEndpointCommandrequest.Type=typeof(Program);
           AssignRoleEndpointCommandResponse response= await _mediator.Send(assignRoleEndpointCommandrequest);
            return Ok(response);
        }

    }
}
