using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using SQLServer_EFCore.Dao.Models;
using System.Configuration;

namespace SQLServer_EFCore.Dao.DbContexts
{
    /// <summary>
    /// 这个数据上下文只能有一个
    /// </summary>
    public class dbtest1DbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Leave> Leaves { get; set; }

        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// 如果无法从应用程序服务提供程序获得 DbContext，则这些工具将查找项目中的派生 DbContext 类型。
        /// 然后，它们尝试使用不带参数的构造函数创建实例。 
        /// 如果 DbContext 是使用 OnConfiguring 方法配置的，则这可以是默认构造函数。
        /// 配置数据库连接
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = "dbtest1",//数据库名
                UserID = "xiaoliu",
                Password = "123",
                IntegratedSecurity = true,//是否使用windows登录相同的用户名和密码
                MultipleActiveResultSets = true,//是否允许一次连接多次查询
                ApplicationName = "SQLServer_EFCore",//标识应用程序名称，可以自定义，供在SQL跟踪调试时用
                TrustServerCertificate = true
                //Encrypt = false
            };

            //builder["ConnectTimeout"] = 30;//这个是因为实现字典接口,这个写法报错了
            //正确写法
            builder["Connect Timeout"] = 1000;

            //builder.ConnectionString = "server=(local);user id=ab;" +"password= a!Pass113;initial catalog=AdventureWorks";//这样直接赋值也可以

            options.UseSqlServer(builder.ToString());


            options.LogTo(Console.WriteLine);//输出数据库脚本日志
            //options.LogTo(WriteLog);//日志只会在一个地方输出，以后配的为准
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //从程序集中获取所有实现IEntityTypeConfiguration的配置
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<DoubleKeyTable>().HasKey(s => new { s.Id, s.t2Id });//设置双主键
        }


        /// <summary>
        /// Unable to create a 'DbContext' of type 'dbtest1DbContext'. 
        /// The exception 'The method or operation is not implemented.' 
        /// was thrown while attempting to create an instance. 
        /// For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728
        /// 这个报错发生和有无无参构造函数没有关系
        /// </summary>
        public dbtest1DbContext()
        {

        }

        public dbtest1DbContext(DbContextOptions options) : base(options)
        {
        }


        private void WriteLog(string sql)
        {
            string sqlStrLog = $"[{DateTime.Now}] start: " + Environment.NewLine + sql + Environment.NewLine + " end;" + Environment.NewLine;
            string sqlPath = Environment.CurrentDirectory + "\\logms";
            string sqlLogName = DateTime.Now.ToString("yyyyMMdd") + ".log";
            string fullPath = Path.Combine(sqlPath, sqlLogName);
            if (!Directory.Exists(sqlPath))Directory.CreateDirectory(sqlPath);
            if(!File.Exists(fullPath)) File.Create(fullPath).Close();
            File.AppendAllText(fullPath, sqlStrLog);
        }
    }

}
