using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using documentz_backend.Models;

namespace documentz_backend.Data
{
    public interface IDbContext
    {
        Task<IEnumerable<Document>> GetDocumentsAsync();
        Task<Document> GetDocumentAsync(Guid id);
        Task<Guid> AddDocumentAsync(Document document);
        Task UpdateDocumentAsync(Document document);
        Task DeleteDocumentAsync(Guid id);
        Task<IEnumerable<string>> GetCategoriesAsync();
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task AddTagAsync(Tag tag);
        Task<IEnumerable<Tag>> GetAttachments(Guid documentId);
        Task AddAttachmentAsync(Guid documentId, Attachment attachment);
    }
}
