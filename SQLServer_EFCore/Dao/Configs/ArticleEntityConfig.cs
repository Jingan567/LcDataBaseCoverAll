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
    public class ArticleEntityConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("T_Articles");
            builder.Property(a=>a.Content).IsRequired().IsUnicode();
            builder.Property(a=>a.Title).IsRequired().IsUnicode().HasMaxLength(255);

            #region 一对多和多对一可以反着来配置
            //builder.HasMany(a => a.Comments).WithOne(c => c.Article);//两个地方只需要配置一个
            #endregion
        }
    }
}
