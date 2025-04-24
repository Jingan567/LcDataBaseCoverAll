using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServer_EFCore.Dao.Models
{
    /// <summary>
    /// 多对多，需要一张外键表
    /// </summary>
    internal class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    }

    internal class Teacher
    {
        public long Id { get; set; }
        //测试新增主键
        public long TeacherId { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
