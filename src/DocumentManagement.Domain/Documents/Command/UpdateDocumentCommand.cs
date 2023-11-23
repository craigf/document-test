using MediatR;
using DocumentManagement.Domain.Documents.Model;

namespace DocumentManagement.Domain.Documents.Command
{
    public class UpdateDocumentCommand : IRequest<Document>
    {
        public UpdateDocumentCommand(string title, string content, int id)
        {
            Title = title;
            Content = content;
            Id = id;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
