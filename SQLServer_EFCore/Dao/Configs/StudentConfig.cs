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
    /// <summary>
    /// 这样的配置类，不设成公开也是可以的
    /// </summary>
    internal class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("T_Students");
            builder.Property(s => s.Name).IsUnicode().HasMaxLength(20);
            builder.HasMany(s => s.Teachers).WithMany(s => s.Students).UsingEntity(j => j.ToTable("T_Students_Teachers"));
        }
    }

    internal class TeacherConfig : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            //builder.ToTable("T_Teachers");//这个注释掉看一下生成的表名，默认表名是类名Teacher
            builder.ToTable("T_Teachers");
            builder.Property(s => s.Name).IsUnicode().HasMaxLength(20);

            //设置主键玩一下
            builder.HasKey(s => s.TeacherId);//在已有主键的情况下，不会将默认以id结尾的属性转成主键
            //builder.HasKey(t=>t.Id);//不支持设置两个主键
            //builder.HasNoKey();
        }
    }
}
