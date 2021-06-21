using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [SugarTable("dbo.Department")]
    public class Department : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int DepartmentID { get; set; }
        public int LeaderID { get; set; }
        public string LeaderName { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        public bool IsDelete { get; set; }
        public int CreateUserID { get; set; }
        public DateTime CreateDate { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
