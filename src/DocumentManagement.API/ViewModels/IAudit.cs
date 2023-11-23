namespace DocumentManagement.API.ViewModels
{
    public interface IAudit
    {
        DateTime CreatedTime { get; }
        DateTime LastModifiedTime { get; }

        public string CreatedBy { get; }
        public string UpdatedBy { get; }
    }
}
