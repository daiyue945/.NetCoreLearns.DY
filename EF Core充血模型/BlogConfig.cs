using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core充血模型
{
    internal class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.OwnsOne(x => x.Title, p =>
            {
                p.Property(e => e.Chinese).HasMaxLength(255);
                p.Property(e => e.English).HasMaxLength(100).HasColumnType("varchar");
            });
            builder.OwnsOne(x => x.Body, p =>
            {
                p.Property(e => e.English).HasMaxLength(255).HasColumnType("varchar");
            });
        }
    }
}
