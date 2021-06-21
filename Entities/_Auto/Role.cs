using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [SugarTable("dbo.Role")]
    public class Role : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RoleID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAllData { get; set; }
        public int? CreateUserID { get; set; }
        public DateTime? CreateDate { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
