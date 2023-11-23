using DocumentManagement.Domain.Documents;
using DocumentManagement.Domain.Documents.Model;
namespace DocumentManagement.Infrastructure.Repository
{
    public class DocumentRepository : AsyncRepository<Document, int>, IDocumentRepository
    {
        public DocumentRepository(DocumentContext dataContext) : base(dataContext)
        {
        }
    }
}
