using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlite.SqlCommand
{
    internal class Insert
    {
        public static Insert InsertCommand => new Insert();
        private string t1 = "INSERT INTO";
        string _tableName;
        //
        private string tableName
        {
            get=>_tableName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _tableName = value;
            }
        }

        public string s { get; set; }
    }
}
