using DocumentManagement.Domain.Documents.Command;
using MediatR;

namespace DocumentManagement.Domain.Documents.Handler
{
    internal class DeleteDocumentHandler : IRequestHandler<DeleteDocumentCommand>
    {
        private readonly IDocumentRepository _repository;

        public DeleteDocumentHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _repository.GetByIdAsync(request.Id);

            await _repository.DeleteAsync(document);
        }
    }
}
