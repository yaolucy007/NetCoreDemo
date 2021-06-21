using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [SugarTable("dbo.UserRole")]
    public class UserRole
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int UserRoleID { get; set; }

        public int UserID { get; set; }

        public int RoleID { get; set; }
    }
}
