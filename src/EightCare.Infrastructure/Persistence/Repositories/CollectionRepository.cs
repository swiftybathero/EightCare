﻿using System;
using System.Threading.Tasks;
using EightCare.Application.Common.Interfaces;
using EightCare.Domain.Entities;

namespace EightCare.Infrastructure.Persistence.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly CollectionContext _collectionContext;

        public CollectionRepository(CollectionContext collectionContext)
        {
            _collectionContext = collectionContext;
        }

        public IUnitOfWork UnitOfWork => _collectionContext;

        public async Task<Collection> GetByIdAsync(Guid collectionId)
        {
            return await _collectionContext.Collections.FindAsync(collectionId);
        }

        public async Task AddAsync(Collection collection)
        {
            await _collectionContext.AddAsync(collection);
        }

        public Task UpdateAsync(Collection collection)
        {
            _collectionContext.Update(collection);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Collection collection)
        {
            _collectionContext.Collections.Remove(collection);

            return Task.CompletedTask;
        }
    }
}
