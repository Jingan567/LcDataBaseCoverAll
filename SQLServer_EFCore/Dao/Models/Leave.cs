using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServer_EFCore.Dao.Models
{
    public class Leave
    {
        public long Id  { get; set; }
        public User Requester {  get; set; }
        public User? Approver { get; set; }//可能没有审批者
        public string Remarks {  get; set; }    
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Status {  get; set; }
    }
}
