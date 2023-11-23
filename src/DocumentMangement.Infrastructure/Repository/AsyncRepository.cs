using System.Linq.Expressions;
using DocumentManagement.Core.Data;
using DocumentManagement.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Infrastructure.Repository
{
    public abstract class AsyncRepository<T, TId> : IAsyncRepository<T, TId> where T : class, IEntityBase<TId>
    {
        protected DbContext DataContext;

        private DbSet<T> _dataSet;

        protected AsyncRepository(DbContext dataContext)
        {
            DataContext = dataContext;
        }

        private DbSet<T> Entities => _dataSet ??= DataContext.Set<T>();

        public async Task<T> GetByIdAsync(int id)
        {
            return (await DataContext
                .Set<T>()
                .FindAsync(id))!;
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            await Entities.AddAsync(entity);
            await DataContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            Entities.Remove(entity);
            await DataContext.SaveChangesAsync();
        }


        public virtual async Task UpdateAsync(T entity)
        {
            DataContext.Entry(entity).State = EntityState.Modified;
            DataContext.Update(entity);
            await DataContext.SaveChangesAsync();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Entities.AnyAsync(predicate);
        }
    }
}
