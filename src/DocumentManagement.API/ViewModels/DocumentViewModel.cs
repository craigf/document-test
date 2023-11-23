namespace DocumentManagement.API.ViewModels
{
    public class DocumentViewModel : IAudit
    {
        public DocumentViewModel(string title, string content, DateTime createdTime, DateTime lastModifiedTime, string createdBy, string updatedBy, int id)
        {
            Title = title;
            Content = content;
            CreatedTime = createdTime;
            LastModifiedTime = lastModifiedTime;
            CreatedBy = createdBy;
            UpdatedBy = updatedBy;
            Id = id;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        public DateTime CreatedTime { get; }
        public DateTime LastModifiedTime { get; }
        public string CreatedBy { get; }
        public string UpdatedBy { get; }
    }
}
