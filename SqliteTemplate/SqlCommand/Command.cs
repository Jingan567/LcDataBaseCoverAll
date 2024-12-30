using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteTemplate.SqlCommand
{
    public abstract class Command
    {
		private string tableName;

		public string TableName
		{
			get { return tableName; }
			set { tableName = value; }
		}

    }
}
