﻿using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Roles.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
    {
        IRoleService _roleService;

        public CreateRoleCommandHandler(IRoleService roleService)
        {
            _roleService=roleService;
        }

        public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
         var result=  await _roleService.CreateRole(request.Rolename);
            return new()
            {
                Succeeded = result,
            };
        }
    }
}
