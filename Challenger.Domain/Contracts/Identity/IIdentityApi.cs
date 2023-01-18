using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Identity
{
    public interface IIdentityApi
    {
        Task<List<IdentityUser>> SearchUsersByName(string name, Guid userId);
    }
}
