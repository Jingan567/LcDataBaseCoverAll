using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServer_EFCore.Dao.DbContexts
{
    ///// <summary>
    ///// 设计时工厂比dbtest1DbContext优先
    ///// </summary>
    //public class dbtest1DbContextFactory : IDesignTimeDbContextFactory<dbtest1DbContext>
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="args">命令行参数</param>
    //    /// <returns></returns>
    //    public dbtest1DbContext CreateDbContext(string[] args)
    //    {
    //        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
    //        {
    //            DataSource = ".",
    //            InitialCatalog = "dbtest1",//数据库名
    //            UserID = "xiaoliu",
    //            Password = "123",
    //            IntegratedSecurity = true,//是否使用windows登录相同的用户名和密码
    //            MultipleActiveResultSets = true,//是否允许一次连接多次查询
    //            ApplicationName = "SQLServer_EFCore"//标识应用程序名称，可以自定义，供在SQL跟踪调试时用
    //        };

    //        //builder["ConnectTimeout"] = 30;//这个是因为实现字典接口,这个写法报错了
    //        //正确写法
    //        builder["Connect Timeout"] = 1000;
    //        var optionsBuilder = new DbContextOptionsBuilder<dbtest1DbContext>();
    //        optionsBuilder.UseSqlServer(builder.ToString());

    //        return new dbtest1DbContext(optionsBuilder.Options);
    //    }
    //}
}
