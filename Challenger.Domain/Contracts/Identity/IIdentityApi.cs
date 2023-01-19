using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Identity
{
    public interface IIdentityApi
    {
        Task<List<ApplicationUser>> SearchUsersByName(string name, Guid userId);

        Task<List<ApplicationUser>> GetUsers(Guid[] userIds);
    }
}
