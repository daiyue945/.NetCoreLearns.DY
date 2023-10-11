using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWithMany
{
    class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("T_Articles");
            builder.Property(p => p.Title).HasMaxLength(100).IsUnicode().IsRequired();
            builder.Property(p => p.Message).IsUnicode().IsRequired();
            builder.HasMany(c => c.Comments).WithOne(a => a.Articles).HasForeignKey(a => a.ArticlesId).IsRequired();
        }
    }
}
