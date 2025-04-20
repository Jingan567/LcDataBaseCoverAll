using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServer_EFCore.Dao.Models
{
    /// <summary>
    /// 一片文章对应多个评论，双向导航
    /// </summary>
    public class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
