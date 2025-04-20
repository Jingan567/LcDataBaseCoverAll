using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQLServer_EFCore.Dao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServer_EFCore.Dao.Configs
{
    public class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_Users").HasKey(t => t.Id);//这个应该是设置主键的方法

            builder.Property(u => u.Name).IsRequired().HasMaxLength(100).IsUnicode();

        }
    }
}
