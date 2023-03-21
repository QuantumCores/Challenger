using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class ActivityTypeRepository: IActivityTypeRepository
    {
        private readonly ChallengerContext _context;

        public ActivityTypeRepository(ChallengerContext context)
        {
            _context = context;
        }

        public ValueTask<ActivityType> Get(long id)
        {
            return _context.ActivityTypes.FindAsync(id);
        }

        public Task<ActivityType[]> GetAll()
        {
            return _context.ActivityTypes.ToArrayAsync();
        }
    }
}
