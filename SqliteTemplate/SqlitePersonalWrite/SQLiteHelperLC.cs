using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteTemplate.SqlitePersonalWrite
{
    internal class SQLiteHelperLC
    {
        SQLiteConnection liteConnection;
        public SQLiteHelperLC(string dbPath)
        {
            liteConnection = new SQLiteConnection("Data Source=" + dbPath);
            liteConnection.Open();
            
        }
    }
}
