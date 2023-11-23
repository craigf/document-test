namespace DocumentManagement.Core.Data
{
    public abstract class BaseEntity : IEntityBase<int>, IAuditable
    {
        public int Id { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
