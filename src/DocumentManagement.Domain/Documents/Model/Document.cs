using DocumentManagement.Core.Data;

namespace DocumentManagement.Domain.Documents.Model
{
    public sealed class Document : BaseEntity
    {
        public Document(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
