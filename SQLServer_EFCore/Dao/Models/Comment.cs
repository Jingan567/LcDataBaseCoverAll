namespace SQLServer_EFCore.Dao.Models
{
    /// <summary>
    ///  一片文章对应多个评论，双向导航
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// EF Core会默认按约定将Id作为主键 
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 没有这个属性，EF Core 也会自动创建，有这个外键方便查询
        /// </summary>
        public long ArticleId {  get; set; }
        public Article Article { get; set; }
        public string Message { get; set; }
    }
}