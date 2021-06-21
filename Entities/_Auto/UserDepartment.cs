using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [SugarTable("dbo.UserDepartment")]
    public class UserDepartment : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int UserDepartmentID { get; set; }
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
    }
}
