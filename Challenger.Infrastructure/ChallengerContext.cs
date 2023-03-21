using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Challenger.Infrastructure
{
    public class ChallengerContext : DbContext
    {
        public ChallengerContext(DbContextOptions<ChallengerContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<GymRecord> GymRecords { get; set; }

        public DbSet<FitRecord> FitRecords { get; set; }

        public DbSet<Measurement> Measurements { get; set; }

        public DbSet<Challenge> Challenges { get; set; }

        public DbSet<UserChallenge> UserChallenges { get; set; }

        public DbSet<ActivityType> ActivityTypes { get; set; }
    }
}