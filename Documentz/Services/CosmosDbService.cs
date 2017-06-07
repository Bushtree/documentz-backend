using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using AutoMapper;
using Documentz.Models;
using Documentz.Utils;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;

namespace Documentz.Services
{
    public class CosmosDbService : IDbService
    {
        private DocumentClient client;
        private readonly IOptionsSnapshot<CosmosConfig> configSnapshot;

        private CosmosConfig Config => configSnapshot.Value;

        public CosmosDbService(IOptionsSnapshot<CosmosConfig> config)
        {
            configSnapshot = config;
        }

        public async Task<IStoredItem> CreateStoredItemAsync(IStoredItem item)
        {
            await EnsureInitializedAsync();
            var response = await client.CreateDocumentAsync(CreateDocumentCollectionUri(), item);
            return (StoredItem)(dynamic)response.Resource;
        }

        public async Task DeleteStoredItemAsync(string id)
        {
            await EnsureInitializedAsync();
            await client.DeleteDocumentAsync(CreateDocumentUri(id));
        }

        public async Task<IEnumerable<IStoredItem>> GetStoredItemsAsync()
        {
            await EnsureInitializedAsync();
            return await GetItemsAsync<StoredItem>(a => true);
        }

        public async Task<IStoredItem> GetStoredItemAsync(string id)
        {
            await EnsureInitializedAsync();
            return await GetItemAsync<StoredItem>(id);
        }

        public async Task<IStoredItem> UpdateStoredItemAsync(string id, IStoredItem item)
        {
            await EnsureInitializedAsync();
            var response = await client.ReplaceDocumentAsync(CreateDocumentUri(id), item);
            return (StoredItem)(dynamic)response.Resource;
        }

        public async Task<IEnumerable<dynamic>> GetAttachmentsAsync(string id)
        {
            await EnsureInitializedAsync();
            Document document = await client.ReadDocumentAsync(CreateDocumentUri(id));
            var result = await client.ReadAttachmentFeedAsync(document.AttachmentsLink);
            var list = result.ToList();
            return result.AsEnumerable();
        }
        
        private async Task<T> GetItemAsync<T>(string id) where T : class
        {
            try
            {
                Document document = await client.ReadDocumentAsync(CreateDocumentUri(id));
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> predicate) where T: class
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                    CreateDocumentCollectionUri(),
                    new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();
            
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        private Uri CreateDocumentCollectionUri()
        {
            return UriFactory.CreateDocumentCollectionUri(Config.DatabaseId, Config.CollectionId);
        }

        private Uri CreateDocumentUri(string id)
        {
            return UriFactory.CreateDocumentUri(Config.DatabaseId, Config.CollectionId, id);
        }

        #region Initialization logic

        private async Task EnsureInitializedAsync()
        {
            client = new DocumentClient(new Uri(Config.Endpoint), Config.Key, new ConnectionPolicy { EnableEndpointDiscovery = false });
            await CreateDatabaseIfNotExistsAsync();
            await CreateCollectionIfNotExistsAsync();
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(Config.DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = Config.DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(Config.DatabaseId, Config.CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(Config.DatabaseId),
                        new DocumentCollection { Id = Config.CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        } 
        #endregion
    }

    public interface IAttachment
    {
    }
}
