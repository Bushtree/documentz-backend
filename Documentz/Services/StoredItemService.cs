using AutoMapper;
using Documentz.DTOs;
using Documentz.Models;
using Documentz.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Documentz.Services
{
    public class StoredItemService : IStoredItemService
    {
        private readonly IDbService dbService;
        public StoredItemService(IDbService _dbService) => (dbService) = (_dbService);

        public async Task<IStoredItem> AddItemAsync(IStoredItem item)
        {
            return Mapper.Map<StoredItemDTO>(await dbService.CreateStoredItemAsync(item));
        }

        public async Task DeleteItemAsync(string id)
        {
            await dbService.DeleteStoredItemAsync(id);
        }

        public async Task<IEnumerable<IStoredItem>> GetAllItemsAsync()
        {
            return Mapper.Map<IEnumerable<StoredItemDTO>>(await dbService.GetStoredItemsAsync());
        }

        public async Task<IStoredItem> GetItemAsync(string id)
        {
            return Mapper.Map<StoredItemDTO>(await dbService.GetStoredItemAsync(id));
        }

        public async Task<IStoredItem> UpdateItemAsync(string id, IStoredItem item)
        {
            return Mapper.Map<StoredItemDTO>(await dbService.UpdateStoredItemAsync(id, Mapper.Map<StoredItem>(item)));
        }

        public async Task<IEnumerable<dynamic>> GetAttachmentsAsync(string id)
        {
//            throw new NotImplementedException();
            return await dbService.GetAttachmentsAsync(id);
        }

        public Task AddAttachmentAsync(string id, Stream content)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAttachmentAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
