using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Presentation.Data;

namespace Presistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        public readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // Get Type Name
            var typeName = typeof(TEntity).Name;
            // Dictionary<string , Object> => string Key [Name Of Type] -- Object From Generic Repository
            // Check if the type is already registered
            //if (_repositories.ContainsKey(typeName))
            //    return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
            if(_repositories.TryGetValue(typeName,out object? repo))
                return (IGenericRepository<TEntity, TKey>) repo;
            else
            {
                // Create Object
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
                // Store Object in Dictionary
                _repositories[typeName] = Repo;
                // Return Object
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
