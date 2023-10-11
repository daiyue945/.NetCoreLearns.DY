using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core充血模型
{
    internal class CommodityEntityConfig : IEntityTypeConfiguration<CommodityEntity>
    {
        public void Configure(EntityTypeBuilder<CommodityEntity> builder)
        {
            //货币改为文字存储
            builder.Property(e => e.currencyName).HasConversion<string>().HasMaxLength(5);
        }
    }
}
