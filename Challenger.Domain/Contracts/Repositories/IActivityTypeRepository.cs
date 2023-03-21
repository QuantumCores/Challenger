using Challenger.Domain.DbModels;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IActivityTypeRepository
    {
        ValueTask<ActivityType> Get(long id);

        Task<ActivityType[]> GetAll();
    }
}
