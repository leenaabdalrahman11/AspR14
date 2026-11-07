using AspR14.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspR14.DAL.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.HasMany(p => p.Translations)
                .WithOne(t => t.Product);
        }
    }
}