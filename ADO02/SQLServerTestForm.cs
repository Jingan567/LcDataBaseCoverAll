

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
                        command.CommandText = "UPDATE [dbo].[Aticle] SET [Title] = @title\r\n WHERE id=@id";
                        //换行符不影响Sql正常执行,正常字符串需要分号，但是变量@Title不需要。
                        command.CommandTimeout = 10;//默认是30秒

                        SqlParameter param = new SqlParameter("@Title", SqlDbType.NChar, 250);//@title变量名不严格区分大小写
                        param.Direction = ParameterDirection.Input;
                        param.Value = "这是新‘；''的标题" + DateTime.Now.ToString();//这样拼接Sql不需要转义
                        command.Parameters.Add(param);

                        //command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt).Value = 4);//这个北盟网校也太low逼了
                        SqlParameter param1 = new SqlParameter("@Id", SqlDbType.BigInt, 250);
                        param.Direction = ParameterDirection.Input;
                        param1.Value = 4;
                        command.Parameters.Add(param1);

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

        private void bt_Transaction_Click(object sender, EventArgs e)
        {
            string result = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        using (SqlTransaction st = conn.BeginTransaction())
                        {

                            SqlParameter param = new SqlParameter("@Title", SqlDbType.NChar, 250);//同一个命令中变量不能多次声明
                            param.Direction = ParameterDirection.Input;
                            command.Parameters.Add(param);
                            command.Transaction = st;//将事务对象赋值过去。
                            try
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    string temp = string.Empty;
                                    //if (i == 5)
                                    //{
                                    //    temp = "shgfdsyhgfhjsd";//制造报错
                                    //}
                                    command.Connection = conn;
                                    command.CommandText = temp + "INSERT INTO [dbo].[Aticle]([Title])VALUES(@title)";
                                    param.Value = i + "这是新‘；''的标题" + DateTime.Now.ToString();

                                    int res = command.ExecuteNonQuery();
                                    result += $"第{i}次受影响行数：{res}\r\n";
                                }
                                st.Commit();
                                MessageBox.Show(result);
                            }
                            catch (Exception ex)
                            {
                                st.Rollback();
                                string msg = $"事务插入失败，数据库已经回滚！！！异常类型{ex.GetType().FullName}，异常信息:{ex.Message}";
                                MessageBox.Show(msg);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void bt_SqlDataReader_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT * FROM [02ADO].[dbo].[Aticle]";
                    command.Connection = conn;
                    SqlParameter param = new SqlParameter("@Title", SqlDbType.NChar, 250);//同一个命令中变量不能多次声明
                    param.Direction = ParameterDirection.Input;
                    param.Value = "这是新‘；''的标题" + DateTime.Now.ToString();
                    //command.Parameters.Add(param);
                    var res = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (res.Read())//将记录前进到下一个结果。
                                      //res.NextResult()当读取批处理 Transact-SQL 语句的结果时，使数据读取器前进到下一个结果。
                    {
                        MessageBox.Show(string.Format("{0},{1},{2}", res[0], res[1], res[2]));
                    }
                    res.Close();
                }
            }
        }

        private void bt_SqlDataAdapter_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter sd = new SqlDataAdapter("select * FROM [02ADO].[dbo].[Aticle]", conn);
                sd.Fill(ds, "newtable");//newtable是DataTable的表名。
                //this.dataGridView1.DataSource = ds.Tables[0];//可以
                DataTable? dataTable = ds.Tables["newtable"];
                if (dataTable != null)
                {
                    this.dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void bt_存储过程_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "myProc";//myProc是存储过程的名字
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;//存储过程需要配置这个
                    SqlParameter param = new SqlParameter("@id", SqlDbType.Int, 8);
                    param.Direction = ParameterDirection.Input;
                    param.Value = 4;
                    command.Parameters.Add(param);

                    var res = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (res.Read())
                    {
                        MessageBox.Show(string.Format("{0},{1},{2}", res[0], res[1], res[2]));
                    }
                    res.Close();
                }
            }
        }

        private void bt_存储过程_DataSet_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "myProc";//myProc是存储过程的名字
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;//存储过程需要配置这个
                    SqlParameter param = new SqlParameter("@id", SqlDbType.Int, 8);
                    param.Direction = ParameterDirection.Input;
                    param.Value = 4;
                    command.Parameters.Add(param);

                    DataSet ds = new DataSet();
                    SqlDataAdapter sd = new SqlDataAdapter();
                    sd.SelectCommand = command;

                    sd.Fill(ds, "newtable");//newtable是DataTable的表名。
                                            //this.dataGridView1.DataSource = ds.Tables[0];//可以
                    DataTable? dataTable = ds.Tables["newtable"];
                    if (dataTable != null)
                    {
                        this.dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void bt_存储过程_影响行数_Click(object sender, EventArgs e)
        {

        }

        private int UpdateCommand<T>(T instance, string DbName = "") where T : class
        {
            return 0;
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
                        //PIs.Select(pi => command.Parameters.Add($"@{pi.Name}",)) ;

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
