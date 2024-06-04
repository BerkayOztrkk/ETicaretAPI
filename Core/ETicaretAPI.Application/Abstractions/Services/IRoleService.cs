using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        (object, int) GetRoles(int page, int size);
        Task<(string id, string name)> GetRoleById(string id);
        Task<bool> CreateRole(string roleName);
        Task<bool> DeleteRole(string roleName);
        Task<bool> UpdateRole(string Id,string roleName);

    }
}
