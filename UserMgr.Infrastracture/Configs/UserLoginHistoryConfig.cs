﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgr.Domain.Entities;

namespace UserMgr.Infrastracture.Configs
{
    public class UserLoginHistoryConfig : IEntityTypeConfiguration<UserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
        {
            builder.ToTable("T_UserLoginHistorys");
            builder.OwnsOne(x => x.PhoneNumber, nb =>
            {
                nb.Property(p => p.PhoneNumbers).HasMaxLength(20).IsUnicode(false);
            });
        }
    }
}
