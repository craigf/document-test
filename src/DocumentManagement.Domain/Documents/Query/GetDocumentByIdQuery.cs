using DocumentManagement.Domain.Documents.Model;
using MediatR;

namespace DocumentManagement.Domain.Documents.Query
{
    public class GetDocumentByIdQuery : IRequest<Document>
    {
        public GetDocumentByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
