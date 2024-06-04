using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Roles.CreateRole
{
    public class CreateRoleCommandRequest:IRequest<CreateRoleCommandResponse>
    {
        public string Rolename { get; set; }
    }
}