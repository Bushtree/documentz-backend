using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using documentz_backend.Errors;
using documentz_backend.Models;

namespace documentz_backend.Data
{
    public class InMemoryDbContext : IDbContext
    {
        private static int documentId = 3;
        private object obj = new object();
        private readonly List<Document> documents = new List<Document>
        {
            new Document
            {
                Id = Guid.NewGuid(),
                Name = "Doc 1",
                Category = "invoice"
            },
            new Document
            {
                Id = Guid.NewGuid(),
                Name = "Doc 2",
                Category = "invoice"
            },
            new Document
            {
                Id = Guid.NewGuid(),
                Name = "Doc 3",
                Category = "general"
            }
        };
        private readonly List<string> categories = new List<string>
        {
            "invoice",
            "general",
            "bill"
        };
        private readonly List<Tag> tags = new List<Tag>();

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            return documents.AsEnumerable();
        }

        public async Task<Document> GetDocumentAsync(Guid id)
        {
            return documents.SingleOrDefault(doc => doc.Id == id);
        }

        public async Task<Guid> AddDocumentAsync(Document document)
        {
            lock (obj)
            {
                document.Id = Guid.NewGuid();
            }
            documents.Add(document);
            return document.Id;
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            var existingIndex = documents.FindIndex(doc => doc.Id == document.Id);
            if (existingIndex >= 0)
            {
                documents[existingIndex] = document;
            }
            else
            {
                throw new ItemNotFoundException();
            }
        }

        public async Task DeleteDocumentAsync(Guid id)
        {
            var document = await GetDocumentAsync(id);
            if (document == null)
            {
                throw new ItemNotFoundException();
            }
            documents.Remove(document);
        }

        public Task<IEnumerable<string>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> GetTagsAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddTagAsync(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> GetAttachments(Guid documentId)
        {
            throw new NotImplementedException();
        }

        public Task AddAttachmentAsync(Guid documentId, Attachment attachment)
        {
            throw new NotImplementedException();
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}