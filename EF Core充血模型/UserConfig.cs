using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core充血模型
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //成员变量没有对应属性，但是需要映射到数据库表中的列
            builder.Property("PasswordHash");
            //属性只读不能改
            builder.Property(e => e.Remark).HasField("remark");
            //属性仅作运行时用，不需要映射到数据库
            builder.Ignore(e => e.Tag);

        }
    }
}
