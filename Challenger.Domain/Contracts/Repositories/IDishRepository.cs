﻿using Challenger.Domain.DbModels;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IDishRepository
    {
        ValueTask<Dish> Get(long id);

        Task<List<Dish>> GetAll();

        Task<List<Dish>> GetAllForUser(long userId);

        void Add(Dish record);

        Task Update(Dish record);

        void Remove(Dish record);

        Task SaveChanges();
    }
}
