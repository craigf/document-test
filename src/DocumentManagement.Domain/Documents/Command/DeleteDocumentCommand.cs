using MediatR;

namespace DocumentManagement.Domain.Documents.Command
{
    public class DeleteDocumentCommand : IRequest
    {
        public DeleteDocumentCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
