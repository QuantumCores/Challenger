using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class GymRecordRepository : IGymRecordRepository
    {
        private readonly ChallengerContext _context;

        public GymRecordRepository(ChallengerContext context)
        {
            _context = context;
        }

        public void Add(GymRecord record)
        {
            _context.GymRecords.Add(record);
        }

        public async Task<List<GymRecord>> Dynamic()
        {
            var tuple = new List<(long userId, string muscle)>() {
            (1, "Back"),
            (3, "Back"),
            (1, "Chest"),
            (3, "Chest")
            };

            var predicate = CreatePredicate(tuple);
            var result = await _context.GymRecords.Where(predicate).ToListAsync();

            return result;
        }

        private static Expression<Func<GymRecord, bool>> CreatePredicate(List<(long userId, string muscle)> tuple)
        {
            var parameter = Expression.Parameter(typeof(GymRecord), "x");

            var propertyUser = Expression.Property(parameter, nameof(GymRecord.UserId));
            var propertyMuscle = Expression.Property(parameter, nameof(GymRecord.MuscleGroup));

            var expression = tuple.Skip(1).Aggregate(
                Compose(tuple[0], propertyUser, propertyMuscle), 
                (x, y) => Expression.Or(x, Compose(y, propertyUser, propertyMuscle)));

            //var expression = Compose(tuple[0], propertyUser, propertyMuscle);
            //foreach (var item in tuple)
            //{
            //    var and2 = Compose(item, propertyUser, propertyMuscle);
            //    expression = Expression.Or(expression, and2);
            //}

            var predicate = Expression.Lambda<Func<GymRecord, bool>>(expression, new[] { parameter });
            return predicate;
        }

        private static Expression Compose((long userId, string muscle) tuple, MemberExpression propertyUser, MemberExpression propertyMuscle)
        {
            var consUser1 = Expression.Constant(tuple.userId, typeof(long));
            var consMusc1 = Expression.Constant(tuple.muscle, typeof(string));

            var first = Expression.Equal(propertyUser, consUser1);
            var second = Expression.Equal(propertyMuscle, consMusc1);

            return Expression.And(first, second);
        }

        public ValueTask<GymRecord> Get(long id)
        {
            return _context.GymRecords.FindAsync(id);
        }

        public Task<List<GymRecord>> GetAll()
        {
            return _context.GymRecords.Include(x => x.User).ToListAsync();
        }

        public Task<List<GymRecord>> GetAllByTimeRange(DateTime startDate, DateTime endDate)
        {
            var records = (IQueryable<GymRecord>)_context.GymRecords;
            if (startDate != default(DateTime))
            {
                records = records.Where(x => x.RecordDate >= startDate);
            }

            if (endDate != default(DateTime))
            {
                records = records.Where(y => y.RecordDate <= endDate);
            }

            return records.Include(x => x.User).ToListAsync();
        }

        public Task<List<GymRecord>> GetAllForUser(long userId)
        {
            return _context.GymRecords.Where(x => x.UserId == userId).ToListAsync();
        }

        public void Remove(GymRecord record)
        {
            _context.GymRecords.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(GymRecord record)
        {
            var dbRecord = await _context.GymRecords.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
