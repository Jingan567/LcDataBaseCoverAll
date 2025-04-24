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
    internal class DoubleKeyTableEntityConfig : IEntityTypeConfiguration<DoubleKeyTable>
    {
        public void Configure(EntityTypeBuilder<DoubleKeyTable> builder)
        {
            builder.ToTable("T_DoubleKeyTable").HasKey(d => d.t2Id);
            //builder.HasKey(s => new { s.Id, s.t2Id })//这里也可以
            builder.Property(d=>d.t2Id).IsRequired().HasMaxLength(256).IsUnicode(true);
        }
    }
}
