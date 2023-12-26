using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolwnEcommerce.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(HolwnEcommerceConsts.DbTablePrefix + "Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Code)
                 .HasMaxLength(50)
                 .IsUnicode(false)
                 .IsRequired();

            builder.Property(x => x.Slug)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.SKU)
               .HasMaxLength(50)
               .IsUnicode(false)
               .IsRequired();


            builder.Property(x => x.ThumbnailPicture)
               .HasMaxLength(250);

            builder.Property(x => x.SeoMetaDescription)
               .HasMaxLength(250);

            builder.Property(x => x.CategoryName)
               .HasMaxLength(50);

            builder.Property(x => x.CategorySlug)
               .HasMaxLength(50);
        }
    }
}
