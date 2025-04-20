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
    public class BookEntityConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //throw new NotImplementedException();//这个位置千万不能让他抛异常
            builder.ToTable("T_Books");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BookName).IsRequired();
        }
    }
}
