using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 自引用的数组结构树
{
    class OrgUnitConfig : IEntityTypeConfiguration<OrgUnit>
    {
        public void Configure(EntityTypeBuilder<OrgUnit> builder)
        {
            builder.ToTable("T_OrgUnits");
            builder.Property(p => p.Name).IsUnicode().IsRequired().HasMaxLength(50);
            builder.HasOne<OrgUnit>(o => o.Parent).WithMany(o => o.Children);//根节点没有Parent，因此这个关系不能修饰为IsRequired
        }
    }
}
