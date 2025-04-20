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
    public class CommentEntityCofig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("T_Comments");
            builder.Property(c=>c.Message).IsRequired().IsUnicode();
            //将表之间关系绑定好
            builder.HasOne(c => c.Article).WithMany(a => a.Comments).IsRequired();//这里的C是Comment类型的对象，a是Article类型的对象。
        }
    }
}
