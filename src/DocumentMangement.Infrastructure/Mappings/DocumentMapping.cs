using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DocumentManagement.Domain.Documents.Model;

namespace DocumentManagement.Infrastructure.Mappings
{
    public static class DocumentMapping
    {
        public static void Configure(this EntityTypeBuilder<Document> modelBuilder)
        {
            modelBuilder.Property(x => x.Title).IsRequired();
            modelBuilder.Property(x => x.Content).IsRequired();
            modelBuilder.Property(x => x.CreatedBy).IsRequired();
            modelBuilder.Property(x => x.UpdatedBy).IsRequired();
            modelBuilder.Property(x => x.CreatedTime).IsRequired();
            modelBuilder.Property(x => x.LastModifiedTime).IsRequired();
        }
    }
}
