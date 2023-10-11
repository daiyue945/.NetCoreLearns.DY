using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多对多
{
    class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("T_Students");

            builder.HasMany<Teacher>(s => s.Teachers).WithMany(s => s.Students)
                .UsingEntity(j => j.ToTable("T_Students_Teachers"));
        }
    }
}
