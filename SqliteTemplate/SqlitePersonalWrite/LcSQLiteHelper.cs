using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteTemplate.SqlitePersonalWrite
{
    /// <summary>
    /// 参考链接：https://mp.weixin.qq.com/s?__biz=MzkzOTY0MTcyOA==&mid=2247485070&idx=1&sn=91c49357d6910d3ae974073e25ad42a8&chksm=c300e91e1d1e9ff3c6abd7b1996b08dd55bdd321cb665fca01a4255cec69e308633a4843330b#rd
    /// AI回答中的C#如何使用SQLite数据库
    /// </summary>
    public class LcSQLiteHelper
    {
        private string sqlitePath = Environment.CurrentDirectory + @"\SqliteDefaultFilePath\test.db";
        public string SqlitePath
        {
            get
            {
                return sqlitePath;
            }
            private set
            {
                sqlitePath = value;
            }
        }

        /// <summary>
        /// 内部连接对象，这个类里面都使用这一个连接对象
        /// </summary>
        private SQLiteConnection conn;

        private SQLiteCommand command;


        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbPath">数据库文件路径</param>
        public LcSQLiteHelper(string dbPath)
        {
            SqlitePath = dbPath;
            conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            command = new SQLiteCommand(conn);
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        public void CreateDbFile()
        {
            if (!File.Exists(SqlitePath))
            {
                SQLiteConnection.CreateFile(SqlitePath);  // 方法一[1](@ref)
                                                          // 或 File.Create(dbPath); 使用Create要自己关闭流           // 方法二（需关闭流）[1](@ref)
            }
        }
        /// <summary>
        /// 连接数据库，不使用密码
        /// </summary>
        /// <returns></returns>
        public bool ConnectDb(out string Msg)
        {
            try
            {
                conn.Open();
                Msg = "打开数据库连接成功。";
                return true;
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 连接数据库使用密码
        /// </summary>
        /// <returns></returns>
        public bool ConnectDb(string password, out string Msg)
        {
            try
            {
                conn.Open();
                conn.ChangePassword(password);
                Msg = "打开数据库连接成功。";
                return true;
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                return false;
            }
        }

        public bool InsertData<T>(T model, out string Msg) where T : IModel
        {
            try
            {
                var properties = typeof(T).GetProperties();
                if (!(properties.Length > 0))
                {
                    Msg = $"{nameof(T)}的属性个数为0，或小于0";
                    return false;
                }

                SQLiteCommandBuilder builder = new SQLiteCommandBuilder();
                builder.

                string sql = "INSERT INTO Users (Name, Code, Password) VALUES (@name, @code, @pwd)";
                command.Parameters.AddWithValue("@name", "管理员");
                command.Parameters.AddWithValue("@code", "admin");
                command.Parameters.AddWithValue("@pwd", "pwd123");

                Msg = "新增数据成功";
                return true;
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 析构函数，释放资源
        /// </summary>
        ~LcSQLiteHelper()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }

    }

    /// <summary>
    /// 约束模型
    /// </summary>
    public interface IModel
    {
    }

    /// <summary>
    /// 参考链接：https://mp.weixin.qq.com/s?__biz=MzkzOTY0MTcyOA==&mid=2247485070&idx=1&sn=91c49357d6910d3ae974073e25ad42a8&chksm=c300e91e1d1e9ff3c6abd7b1996b08dd55bdd321cb665fca01a4255cec69e308633a4843330b#rd
    /// 这个里面展示出来的类库。
    /// </summary>
    public class Sample
    {
        static readonly string dbFilename = string.Format("{0}db{1}{2}", AppDomain.CurrentDomain.BaseDirectory, Path.PathSeparator, "test.db");
        public static void CreateDB_1()
        {

            // 创建数据库 方式一
            if (!File.Exists(dbFilename))
            {
                // 创建数据库文件
                FileStream fileStream = File.Create(dbFilename);
            }
        }
        public static void CreateDB_2()
        {
            // 创建数据库 方式二
            if (!File.Exists(dbFilename))
            {
                // 创建数据库文件
                SQLiteConnection.CreateFile(dbFilename);
            }
        }

        public static void Connection()
        {
            // 数据库未设置密码
            string connectionString = string.Format("Data Source={0}; Version=3; ", dbFilename);
            // 连接数据库
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // 打开数据库连接
                connection.Open();
            }
        }

        public static void ConnectionWithPassword()
        {
            // 数据库设置了密码
            string connectionString = string.Format("Data Source={0}; Version=3; Password={1};", dbFilename, "123456");
            // 连接数据库
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // 打开数据库连接
                connection.Open();
            }
        }

        public static void SetDbPassword()
        {
            // 数据库未设置密码
            string connectionString = string.Format("Data Source={0};Version=3;", dbFilename);
            // 连接数据库
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // 打开数据库连接
                connection.Open();
                // 设置密码
                connection.ChangePassword("123456");
            }
        }

        public static void CreateDataTable()
        {
            // 数据库未设置密码
            string connectionString = string.Format("Data Source={0};Version=3;", dbFilename);
            // 连接数据库
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // 打开数据库连接
                connection.Open();
                // 执行SQL的语句
                string commandText = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name VARCHAR(100), Code VARCHAR(100),Password VARCHAR(100))";
                // 创建 SQLiteCommand 
                using (SQLiteCommand command = new SQLiteCommand(commandText, connection))
                {
                    // 执行语句
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void InsertData()
        {
            // 数据库未设置密码
            string connectionString = string.Format("Data Source={0};Version=3;", dbFilename);
            // 连接数据库
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // 打开数据库连接
                connection.Open();
                // 执行SQL的语句
                string commandText = "insert into Users (Name, Code,Password) values (@name, @code,@password)";
                // 创建 SQLiteCommand 
                using (SQLiteCommand command = new SQLiteCommand(commandText, connection))
                {
                    // 设置参数值
                    command.Parameters.AddWithValue("@name", "管理员");
                    command.Parameters.AddWithValue("@code", "admin");
                    command.Parameters.AddWithValue("@password", "pwd123456");
                    // 执行语句
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateData()
        {

            // 数据库未设置密码
            string connectionString = string.Format("Data Source={0};Version=3;", dbFilename);
            // 连接数据库
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // 打开数据库连接
                connection.Open();
                // 执行SQL的语句
                string commandText = "update Users SET Password=@password WHERE Code = @code";
                // 创建 SQLiteCommand 
                using (SQLiteCommand command = new SQLiteCommand(commandText, connection))
                {
                    // 设置参数值
                    command.Parameters.AddWithValue("@code", "admin");
                    command.Parameters.AddWithValue("@password", "admin123456");
                    // 执行语句
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteData()
        {

            // 数据库未设置密码
            string connectionString = string.Format("Data Source={0};Version=3;", dbFilename);
            // 连接数据库
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // 打开数据库连接
                connection.Open();
                // 执行SQL的语句
                string commandText = "update Users SET Password=@password WHERE Code = @code";
                // 创建 SQLiteCommand 
                using (SQLiteCommand command = new SQLiteCommand(commandText, connection))
                {
                    // 设置参数值
                    command.Parameters.AddWithValue("@code", "admin");
                    command.Parameters.AddWithValue("@password", "admin123456");
                    // 执行语句
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void QueryData()
        {
            // 数据库未设置密码
            string connectionString = string.Format("Data Source={0};Version=3;", dbFilename);
            // 连接数据库
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // 打开数据库连接
                connection.Open();
                // 执行SQL的语句
                string commandText = "select * from Users";
                // 创建 SQLiteCommand 
                using (SQLiteCommand command = new SQLiteCommand(commandText, connection))
                {
                    // 执行语句 返回查询数据
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // 输出数据
                        while (reader.Read())
                        {
                            // 
                            Console.WriteLine($"ID: {reader["Id"]}, 名称: {reader["Name"]}, 编码: {reader["Code"]}");
                        }
                    }
                }
            }
        }
    }
}
