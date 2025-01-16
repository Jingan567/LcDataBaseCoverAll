using MySql.Data.MySqlClient;

namespace MysqlTemplate2_Mysql_data_dll
{
    public class MysqlHelper
    {
        public void GetConnectionString(string databaseName)
        {
            //与数据库连接的信息
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            //用户名
            builder.UserID = "root";
            //密码
            builder.Password = "Aa123456";
            //服务器地址
            builder.Server = "localhost";
            //连接时的数据库
            builder.Database = "test";
            //定义与数据连接的链接
            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            //打开这个链接
            connection.Open();
        }
        string connectstring = "data source=localhost;database=test1;user id=root;password=Aa123456;pooling=true;charset=utf8;";
        string connectstring1 = "server=localhost;user id=root;password=Aa123456;";

        public void CreateDatabase(string databaseName)
        {
            using (var connection = new MySqlConnection(connectstring1))
            {
                connection.Open();
                string query = $"CREATE DATABASE IF NOT EXISTS `{databaseName}`;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
