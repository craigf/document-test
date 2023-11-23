using DocumentManagement.Core.Data;
using System.Linq.Expressions;

namespace DocumentManagement.Core.Repository
{
    public interface IAsyncRepository<T, in TId> where T : IEntityBase<TId>
    {
        Task<T> GetByIdAsync(int id);

        Task<T> InsertAsync(T entity);

        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
