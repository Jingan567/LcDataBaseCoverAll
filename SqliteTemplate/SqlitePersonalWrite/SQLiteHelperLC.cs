using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteTemplate.SqlitePersonalWrite
{
    public class SQLiteHelperLC
    {
        public string SqlitePath { get; private set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbPath">数据库地址</param>
        public SQLiteHelperLC(string dbPath)
        {
            SqlitePath = dbPath;

        }

        public void CreateDbFile()
        {
            string dbPath = "test.db";
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);  // 方法一[1](@ref)
                                                  // 或 File.Create(dbPath); 使用Create要自己关闭流           // 方法二（需关闭流）[1](@ref)
            }
        }
    }
}
