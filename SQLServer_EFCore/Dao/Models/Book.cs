using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServer_EFCore.Dao.Models
{
    public class Book
    {
        public long Id { get; set; }

        public string BookName { get; set; }

        public string Author { get; set; }
    }
}
