﻿using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class ChallengeRepository: IChallengeRepository
    {
        private const int MaxResultCount = 10;
        private readonly ChallengerContext _context;

        public ChallengeRepository(ChallengerContext context)
        {
            _context = context;
        }

        public void Add(Challenge record)
        {
            _context.Challenges.Add(record);
        }

        public ValueTask<Challenge> Get(long id)
        {
            return _context.Challenges.FindAsync(id);
        }

        public Task<List<Challenge>> GetAll()
        {
            return _context.Challenges.Include(x => x.User).ToListAsync();
        }

        public Task<List<Challenge>> GetAllForUser(Guid userId)
        {
            return _context.Challenges.Where(x => x.CreatorId == userId).ToListAsync();
        }

        public Task<List<Challenge>> Find(string search)
        {
            return _context.Challenges.Where(x => x.Name.Contains(search))
                .Take(MaxResultCount)
                .ToListAsync();
        }

        public void Remove(Challenge record)
        {
            _context.Challenges.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(Challenge record)
        {
            var dbRecord = await _context.Challenges.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
