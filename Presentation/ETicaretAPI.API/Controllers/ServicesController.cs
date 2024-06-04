using ETicaretAPI.Application.Abstractions.Services.Configurations;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ServicesController : ControllerBase
    {
        readonly IService _service;

        public ServicesController(IService service)
        {
            _service=service;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu ="Get Authorize Definition Endpoints", ActionType = ActionType.Reading, Definition = "Application Services ")]

        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
           var datas= _service.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
