using AutoMapper;
using Documentz.DTOs;
using Documentz.Models;
using Documentz.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Documentz.Services
{
    public class StoredItemService : IStoredItemService
    {
        public async Task<IEnumerable<StoredItemDTO>> GetItemsAsync(Expression<Func<StoredItem, bool>> predicate = null)
        {
            return Mapper.Map<IEnumerable<StoredItemDTO>>(await DocumentDbRepository<StoredItem>.GetItemsAsync(predicate));
        }
    }
}
