using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWithMany
{
    class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("T_Comments");
            builder.Property(p => p.Message).IsUnicode().IsRequired();
            //builder.HasOne<Article>(c => c.Articles).WithMany(c=>c.Comments).HasForeignKey(t=>t.ArticlesId).IsRequired();

        }
    }
}
