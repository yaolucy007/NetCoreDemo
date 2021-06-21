using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [SugarTable("dbo.RolePermission")]
    public class RolePermission : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RolePermissionID { get; set; }
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
    }
}
