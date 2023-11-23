namespace DocumentManagement.Core.Data
{
    public interface IEntityBase<TId>
    {
        TId Id { get; }
    }
}
