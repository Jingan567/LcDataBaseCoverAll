using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteTemplate.SqlCommand
{
    public class Insert : Command
    {
        public static Insert InsertCommand => new Insert();
        private string t1 = "INSERT INTO";
        private string t2 = "VALUES";
        public string CreateSql(string tableName, Dictionary<string, object> dicNameValues)
        {
            string result;
            string columnName="";
            string values = "";
            foreach (var key in dicNameValues.Keys)
            {
                columnName = key + ",";
            }
            columnName=columnName.Trim().Remove(columnName.Length - 1);
            foreach (var value in dicNameValues.Values)
            {
                values = value + ",";
            }
            values = values.Trim().Remove(values.Length - 1);
            result = t1 + " " + tableName + " " +"["+ columnName + "]" +t2+"["+ values + "]";
            return result;
        }
    }
}
