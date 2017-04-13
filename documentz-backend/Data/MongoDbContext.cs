using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using documentz_backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace documentz_backend.Data
{
    public class MongoDbContext : IDbContext
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase db;

        public MongoDbContext()
        {
            client = new MongoClient("mongodb://documentz-mongo:27017");
            db = client.GetDatabase("documents");
        }

        public async Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            await EnsureCollection<Document>("Documents");
            return await db.GetCollection<Document>("Documents").Find(new BsonDocument()).ToListAsync();
        }

        public Task<Document> GetDocumentAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> AddDocumentAsync(Document document)
        {
            await EnsureCollection<Document>("Documents");
            document.Id = new Random().Next();
            await db.GetCollection<Document>("Documents").InsertOneAsync(document);
            return document.Id;
        }

        public Task UpdateDocumentAsync(Document document)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteDocumentAsync(int id)
        {
            throw new System.NotImplementedException();
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

        public Task<IEnumerable<Models.Tag>> GetAttachments(int documentId)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAttachmentAsync(int documentId, Attachment attachment)
        {
            throw new System.NotImplementedException();
        }

        private async Task EnsureCollection<T>(string collectionName)
        {
            var collections = db.ListCollections();
            var collection = db.GetCollection<T>(collectionName);
            if (collection == null)
            {
                await db.CreateCollectionAsync(collectionName);
            }
        }
    }
}