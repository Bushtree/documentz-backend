using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using documentz_backend.Models;

namespace documentz_backend.Data
{
    public interface IDbContext
    {
        Task<IEnumerable<Document>> GetDocumentsAsync();
        Task<Document> GetDocumentAsync(int id);
        Task<int> AddDocumentAsync(Document document);
        Task UpdateDocumentAsync(Document document);
        Task DeleteDocumentAsync(int id);
        Task<IEnumerable<string>> GetCategoriesAsync();
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task AddTagAsync(Tag tag);
        Task<IEnumerable<Tag>> GetAttachments(int documentId);
        Task AddAttachmentAsync(int documentId, Attachment attachment);
    }
}
