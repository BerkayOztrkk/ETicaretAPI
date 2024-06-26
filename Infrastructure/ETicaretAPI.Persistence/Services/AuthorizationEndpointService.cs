﻿
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Services.Configurations;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using ETicaretAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IService _service;
       readonly IEndpointReadRepository _endpointReadRepository;
       readonly IEndpointWriteRepository _endpointWriteRepository;
      readonly  IMenuReadRepository _menuReadRepository;
      readonly  IMenuWriteRepository _menuWriteRepository;
        readonly RoleManager<AppRole>_roleManager;

        public AuthorizationEndpointService(IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointWriteRepository, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository, RoleManager<AppRole> roleManager)
        {
            _endpointReadRepository=endpointReadRepository;
            _endpointWriteRepository=endpointWriteRepository;
            _menuReadRepository=menuReadRepository;
            _menuWriteRepository=menuWriteRepository;
            _roleManager=roleManager;
        }

        public AuthorizationEndpointService(IService service)
        {
            _service=service;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
        {
            {
                Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
                if (_menu == null)
                {
                    _menu = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = menu
                    };
                    await _menuWriteRepository.AddAsync(_menu);

                    await _menuWriteRepository.SaveAsync();
                }
                Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

                if (endpoint == null)
                {
                    var action = _service.GetAuthorizeDefinitionEndpoints(type)
                            .FirstOrDefault(m => m.Name == menu)
                            ?.Actions.FirstOrDefault(e => e.Code == code);

                    endpoint = new()
                    {
                        Code = action.Code,
                        ActionType = action.ActionType,
                        HttpType = action.HttpType,
                        Definition = action.Definition,
                        Id = Guid.NewGuid(),
                        Menu = _menu
                    };

                    await _endpointWriteRepository.AddAsync(endpoint);
                    await _endpointWriteRepository.SaveAsync();
                }

                foreach (var role in endpoint.Roles)
                    endpoint.Roles.Remove(role);

                var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

                foreach (var role in appRoles)
                    endpoint.Roles.Add(role);

                await _endpointWriteRepository.SaveAsync();
            }

        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
        {
           Endpoint? endpoint=  await  _endpointReadRepository.Table.Include(e=>e.Roles).Include(e=>e.Menu).FirstOrDefaultAsync(e=>e.Code==code && e.Menu.Name==menu);
            return endpoint.Roles.Select(r=> r.Name).ToList();
        }
    }
}