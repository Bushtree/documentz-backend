using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using documentz_backend.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace documentz_backend.Data
{
    public class MongoDbContext : IDbContext
    {
        const string DbName = "documents";
        private readonly IMongoDatabase db;
        private readonly ILogger logger;

        public MongoDbContext(IOptions<MongoConfig> config, ILogger<MongoDbContext> logger)
        {
            this.logger = logger;
            logger.LogDebug(LoggingEvents.Initialize, "Initialized with config {0}", JsonConvert.SerializeObject(config.Value));
            db = new MongoClient(config.Value.ConnectionString).GetDatabase(DbName);
        }

        public async Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            logger.LogDebug(LoggingEvents.ListItems, "Get Documents");
            var collection = await GetDocumentsCollection();
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Document> GetDocumentAsync(Guid id)
        {
            logger.LogTrace(LoggingEvents.GetItem, $"Get document {id}");
            var collection = await GetDocumentsCollection();
            return await collection.Find(CreateIdFilter(id)).SingleOrDefaultAsync();
        }

        public async Task<Guid> AddDocumentAsync(Document document)
        {
            var collection = await GetDocumentsCollection();
            await collection.InsertOneAsync(document);
            return document.Id;
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            var collection = await GetDocumentsCollection();
            await collection.FindOneAndReplaceAsync(CreateIdFilter(document.Id), document);
        }

        public async Task DeleteDocumentAsync(Guid id)
        {
            var collection = await GetDocumentsCollection();
            await collection.FindOneAndDeleteAsync(CreateIdFilter(id));
        }

        public Task<IEnumerable<string>> GetCategoriesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Models.Tag>> GetTagsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task AddTagAsync(Models.Tag tag)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Models.Tag>> GetAttachments(Guid documentId)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAttachmentAsync(Guid documentId, Attachment attachment)
        {
            throw new System.NotImplementedException();
        }

        private async Task<IMongoCollection<Document>> GetDocumentsCollection()
        {
            await EnsureCollection<Document>("documents");
            return db.GetCollection<Document>("documents");
        }

        private async Task EnsureCollection<T>(string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            if (collection == null)
            {
                logger.LogDebug(LoggingEvents.Initialize, $"Collection {collectionName} does not exist.");
                await db.CreateCollectionAsync(collectionName);
                logger.LogDebug(LoggingEvents.Initialize, $"Created {collectionName}.");
            }
        }

        private static FilterDefinition<Document> CreateIdFilter(Guid id)
        {
            return Builders<Document>.Filter.Eq("Id", id);
        }

    }
}