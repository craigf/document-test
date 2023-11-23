using DocumentManagement.Domain.Documents.Command;
using DocumentManagement.Domain.Documents.Model;
using MediatR;

namespace DocumentManagement.Domain.Documents.Handler
{
    public sealed class UpdateDocumentHandler : IRequestHandler<UpdateDocumentCommand, Document>
    {
        private readonly IDocumentRepository _repository;

        public UpdateDocumentHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Document> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _repository.GetByIdAsync(request.Id);

            document.Update(request.Title, request.Content);
            await _repository.UpdateAsync(document);

            return document;
        }
    }
}
