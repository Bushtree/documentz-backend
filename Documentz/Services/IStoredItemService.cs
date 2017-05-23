using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Documentz.DTOs;
using Documentz.Models;

namespace Documentz.Services
{
    public interface IStoredItemService
    {
        Task<IEnumerable<StoredItemDTO>> GetItemsAsync(Expression<Func<StoredItem, bool>> predicate = null);
    }
}