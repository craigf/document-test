using DocumentManagement.Domain.Documents.Command;
using DocumentManagement.Domain.Documents.Model;
using MediatR;

namespace DocumentManagement.Domain.Documents.Handler
{
    public sealed class CreateDocumentHandler : IRequestHandler<CreateDocumentCommand, Document>
    {
        private readonly IDocumentRepository _documentRepository;

        public CreateDocumentHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<Document> Handle(CreateDocumentCommand request, CancellationToken cancellationToken) =>
            await _documentRepository.InsertAsync(new Document(request.Title, request.Content));
    }
}
