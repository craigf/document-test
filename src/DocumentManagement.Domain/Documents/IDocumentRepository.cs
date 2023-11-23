using DocumentManagement.Core.Repository;
using DocumentManagement.Domain.Documents.Model;

namespace DocumentManagement.Domain.Documents
{
    public interface IDocumentRepository : IAsyncRepository<Document, int>
    {
    }
}
