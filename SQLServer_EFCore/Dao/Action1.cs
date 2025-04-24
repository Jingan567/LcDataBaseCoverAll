using SQLServer_EFCore.Dao.DbContexts;
using SQLServer_EFCore.Dao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServer_EFCore.Dao
{
    internal static class Action1
    {
        public static void Test1()
        {
            dbtest1DbContext ctx = new dbtest1DbContext();
            Customer c1 = new Customer();
            c1.Name = "A";
            //c1.Id = 13;//主键不需要赋值，自增的
            ctx.Customers.Add(c1);
            ctx.SaveChanges();
        }

        public static void Delete()
        {
            dbtest1DbContext ctx = new dbtest1DbContext();
            //var b = ctx.Customers.Single(s => s.Id == 3||s.Name=="A");//如果没查到，还会抛异常. //超过一个也会报异常

            var b = ctx.Customers.Where(s => s.Name == "A");
            ctx.Remove(b);
            ctx.SaveChanges();
        }
    }
}
