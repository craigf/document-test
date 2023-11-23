using DocumentManagement.Domain.Documents.Query;
using MediatR;
using DocumentManagement.Domain.Documents.Model;

namespace DocumentManagement.Domain.Documents.Handler
{
    public sealed class GetDocumentByIdHandler : IRequestHandler<GetDocumentByIdQuery, Document>
    {
        private readonly IDocumentRepository _repository;

        public GetDocumentByIdHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Document> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken) =>
            await _repository.GetByIdAsync(request.Id);
    }
}
