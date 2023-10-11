using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("T_Books");
            builder.Property(b => b.TiTle).HasMaxLength(50).IsRequired();
            builder.Property(b => b.AuthorName).HasMaxLength(20).IsRequired();
            builder.Ignore(b => b.Age2);

            
        }
    }
}
