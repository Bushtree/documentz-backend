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
        public async Task<IStoredItem> AddItemAsync(IStoredItem item)
        {
            return Mapper.Map<StoredItemDTO>(await DocumentDbRepository<StoredItem>.CreateItemAsync(Mapper.Map<StoredItem>(item)));
        }

        public async Task DeleteItemAsync(string id)
        {
            await DocumentDbRepository<StoredItem>.DeleteItemAsync(id);
        }

        public async Task<IEnumerable<IStoredItem>> GetAllItemsAsync()
        {
            return Mapper.Map<IEnumerable<StoredItemDTO>>(await DocumentDbRepository<StoredItem>.GetItemsAsync(a => true));
        }

        public async Task<IStoredItem> GetItemAsync(string id)
        {
            return Mapper.Map<StoredItemDTO>(await DocumentDbRepository<StoredItem>.GetItemAsync(id));
        }

        public async Task UpdateItemAsync(string id, IStoredItem item)
        {
            await DocumentDbRepository<StoredItem>.UpdateItemAsync(id, Mapper.Map<StoredItem>(item));
        }
    }
}
