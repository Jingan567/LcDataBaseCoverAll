

using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

namespace ADO02
{
    public partial class SQLServerTestForm : Form
    {
        public SQLServerTestForm()
        {
            InitializeComponent();

        }

        #region 属性
        private SqlConnectionStringBuilder ConnectStr = new SqlConnectionStringBuilder()
        {
            Pooling = true,
            DataSource = ".",
            InitialCatalog = "02ADO",
            IntegratedSecurity = true,
            TrustServerCertificate = true,
            ConnectTimeout = 30//连接超时
        };
        #endregion

        private void btn_Connection_Click(object sender, EventArgs e)
        {
            //这个连接本地的string connectionString = "Data Source=.;Initial Catalog=DB;Integrated Security=true;Trust Server Certificate=True;";
            //SqlConnection conn = new SqlConnection(connectionString);
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            #region 成功连接的数据库配置
            //builder.DataSource = ".";
            //builder.DataSource = "tcp:1116-SL,1433";//这个字符串可以连上
            //builder.DataSource = "tcp:(local)";//这个也可以，之前不行可能是未配置防火墙的原因
            //builder.DataSource = "np:(local)";//OK
            //builder.DataSource = @"1116-SL";//OK
            //builder.DataSource = "1116-SL,1433";//OK
            //builder.DataSource = "tcp:1116-SL";//OK
            //builder.DataSource = @"tcp:127.0.0.1,1433";//OK
            //builder.DataSource = "127.0.0.1,1433\\MSSQLSERVER";//OK,那还是相当于我被主机名给误导了
            //主机名要么是IP地址，要么是主机名
            #endregion
            //builder.DataSource = "tcp:1116-SL\\MSSQLSERVER";//NG，原因主机名无法被正确解析为IP地址
            builder.DataSource = "(local)";
            builder.InitialCatalog = "02ADO";
            builder.UserID = "xiaoliu";
            builder.Password = "123";
            builder.TrustServerCertificate = true;
            builder.ConnectTimeout = 1000;
            SqlConnection conn = new SqlConnection(builder.ConnectionString);
            conn.Open();
            //这里使用数据库连接进行操作
            conn.Close();

            SqlConnectionStringBuilder builder1 = new SqlConnectionStringBuilder();
            builder1.IntegratedSecurity = true;
            builder1.InitialCatalog = "02ADO";
            builder1.TrustServerCertificate = true;

            //也可以使用using
            using (SqlConnection conn1 = new SqlConnection(builder1.ConnectionString))
            {
                try
                {
                    conn1.Open();
                    string cmdText = "SELECT COUNT(1) FROM [02ADO].[dbo].[Aticle]";

                    SqlCommand command = new SqlCommand(cmdText, conn1);//也要释放
                    object res = command.ExecuteScalar();//执行查询，并返回查询所返回的结果集中第一行的第一列。 忽略其他列或行。这里的返回结果可能为空。
                    if (res == DBNull.Value || res == null)
                        MessageBox.Show("查询结果为空");
                    else
                        MessageBox.Show(res.ToString());
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        //使用属性注入和构造注入没啥区别
                        command.Connection = conn;
                        command.CommandText = "INSERT INTO [dbo].[Aticle]([Title])VALUES(@title)";
                        SqlParameter param = new SqlParameter("@title", SqlDbType.NChar, 250);//长度也可以不指定,参数名都以@开头
                        param.Direction = ParameterDirection.Input;//输入参数，默认是Input
                        param.Value = "这是新的标题" + DateTime.Now;
                        command.Parameters.Add(param);
                        command.CommandTimeout = 10;//默认是30秒
                        int res = command.ExecuteNonQuery();//非查询，返回受影响的行数。
                        MessageBox.Show(res.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void bt_SetCommandTimeOut_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    //throw new Exception("sdsd");
                    if (conn.State != ConnectionState.Open) conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        //使用属性注入和构造注入没啥区别
                        command.Connection = conn;
                        command.CommandText = "select 1";
                        command.CommandTimeout = 10;//默认是30秒
                        object obj = command.ExecuteScalar();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void bt_SetCommandParas_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        //使用属性注入和构造注入没啥区别
                        command.Connection = conn;
                        command.CommandText = "SELECT [Id], [Title], [AddTime] FROM [dbo].[Aticle] WHERE id = @id";
                        SqlParameter param = new SqlParameter("@id", SqlDbType.Int, 16);//长度也可以不指定,参数名都以@id开头
                        param.Direction = ParameterDirection.Input;//输入参数，默认是Input
                        param.Value = 11;
                        command.Parameters.Add(param);
                        command.CommandTimeout = 10;//默认是30秒
                        object res = command.ExecuteScalar();
                        if (res == DBNull.Value || res == null)
                            MessageBox.Show("查询结果为空");
                        else
                            MessageBox.Show(res.ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        //使用属性注入和构造注入没啥区别
                        command.Connection = conn;
                        command.CommandText = "UPDATE [dbo].[Aticle]SET [Title] = '新标题'\r\n WHERE Title='新标题1'";//换行符不影响Sql正常执行
                        command.CommandTimeout = 10;//默认是30秒
                        int res = command.ExecuteNonQuery();//即使更新内容前后相同，也会返回受影响行数为1
                        MessageBox.Show(res.ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int UpdateCommand<T>(T instance, string DbName = "") where T : class
        {

            // 获取泛型类的类型
            Type type = typeof(T);

            //PropertyInfo idProperty =;
            //if (idProperty == null)
            //{
            //    throw new InvalidOperationException("未找到主键属性 'Id'。类中必须要实现带有Id的主键");
            //}

            // 获取类的所有属性
            PropertyInfo[] PIs = type.GetProperties();

            // 假设类名即为表名
            string tableName = DbName ?? type.Name;

            // 构建 SET 子句
            string setClause = string.Join(", ", PIs
               .Where(pi => pi.Name != "Id" || pi.GetValue(instance) != null) // 假设主键名为 Id，不更新主键
               .Select(pi => $"{pi.Name} = @{pi.Name}"));
            SqlParameterCollection sqlParameters = 
            string whereClause = $"Id = @Id";

            // 构建完整的 SQL 语句
            string sql = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        //使用属性注入和构造注入没啥区别
                        command.Connection = conn;
                        command.CommandText = sql;
                        command.CommandTimeout = 10;//默认是30秒
                        command.Parameters.Add();

                        int resCount = command.ExecuteNonQuery();
                        MessageBox.Show(resCount.ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
