using DocumentManagement.Domain.Documents.Model;
using MediatR;

namespace DocumentManagement.Domain.Documents.Command
{
    public sealed class CreateDocumentCommand : IRequest<Document>
    {
        public CreateDocumentCommand(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
