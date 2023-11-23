using DocumentManagement.Core.Data;
using DocumentManagement.Core.Extensions;
using DocumentManagement.Domain.Documents.Model;
using DocumentManagement.Infrastructure.Mappings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Infrastructure
{
    public sealed class DocumentContext : DbContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IContextConfiguration _config;

        public DocumentContext(IHttpContextAccessor contextAccessor, IContextConfiguration config)
        {
            _contextAccessor = contextAccessor;
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Document> Documents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().Configure();
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var currentTime = DateTime.UtcNow;
            foreach (var item in ChangeTracker.Entries()
                         .Where(e => e is { State: EntityState.Added, Entity: BaseEntity }))
            {
                var entity = item.Entity as BaseEntity;
                entity.CreatedTime = currentTime;
                entity.CreatedBy = _contextAccessor.HttpContext.User.GetUserEmail(); ;
                entity.LastModifiedTime = currentTime;
                entity.UpdatedBy = _contextAccessor.HttpContext.User.GetUserEmail(); ;
            }

            foreach (var item in ChangeTracker.Entries()
                         .Where(predicate: e => e is { State: EntityState.Modified, Entity: BaseEntity }))
            {
                var entity = item.Entity as BaseEntity;
                entity.LastModifiedTime = currentTime;
                entity.UpdatedBy = _contextAccessor.HttpContext.User.GetUserEmail(); ;
                item.Property(nameof(entity.CreatedTime)).IsModified = false;
                item.Property(nameof(entity.CreatedBy)).IsModified = false;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
