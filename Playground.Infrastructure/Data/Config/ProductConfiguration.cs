using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Core.Entities;

namespace Playground.Infrastructure.Data.Config
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            ////unique index
            //builder.HasIndex(h => h.Name).IsUnique();
            ////primary key
            //builder.HasKey(b => b.Name);
            //builder.HasCheckConstraint<Product>("CheckConstraints", "select * from products");
        }
    }
}
