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
        Task<IStoredItem> AddItemAsync(IStoredItem item);
        Task DeleteItemAsync(string id);
        Task<IStoredItem> GetItemAsync(string id);
        Task<IEnumerable<IStoredItem>> GetAllItemsAsync();
        Task<IStoredItem> UpdateItemAsync(string id, IStoredItem item);
    }
}