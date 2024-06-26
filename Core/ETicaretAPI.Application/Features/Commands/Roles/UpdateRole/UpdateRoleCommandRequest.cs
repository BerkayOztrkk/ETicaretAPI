﻿using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Roles.UpdateRole
{
    public class UpdateRoleCommandRequest : IRequest<UpdateRoleCommandResponse>
    {
        public string Id { get; set; }
        public string Rolename { get; set; }
    }
}