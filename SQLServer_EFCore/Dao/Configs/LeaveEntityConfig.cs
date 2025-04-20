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
    /// 人员和申请单是一对多的关系
    /// </summary>
    public class LeaveEntityConfig : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.ToTable("T_Leaves");
            builder.HasOne(l => l.Requester).WithMany();//单向导航，只需要WithMany()不配置属性。在一的那端不需要访问多端的属性。如果确实需要也可以配置，但是不建议。
            builder.HasOne(l => l.Approver).WithMany();//单向导航一般使用HasOne().WithMany()这种结构。
        }
    }
}
