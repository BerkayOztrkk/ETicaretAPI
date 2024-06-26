﻿using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Roles.DeleteRole
{
    public class DeleteRoleCommandRequest:IRequest<DeleteRoleCommandResponse>
    {
        public string Rolename { get; set; }
    }
}