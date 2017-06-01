using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Documentz.Models;

namespace Documentz.Services
{
    public interface IDbService
    {
        Task<IStoredItem> CreateStoredItemAsync(IStoredItem item);
        Task DeleteStoredItemAsync(string id);
        Task<IStoredItem> GetStoredItemAsync(string id);
        Task<IEnumerable<IStoredItem>> GetStoredItemsAsync(Expression<Func<IStoredItem, bool>> predicate = null);
        Task<IStoredItem> UpdateStoredItemAsync(string id, IStoredItem item);
    }
}