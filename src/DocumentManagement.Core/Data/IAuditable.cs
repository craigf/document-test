namespace DocumentManagement.Core.Data
{
    public interface IAuditable
    {
        DateTime CreatedTime { get; set; }
        DateTime LastModifiedTime { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
