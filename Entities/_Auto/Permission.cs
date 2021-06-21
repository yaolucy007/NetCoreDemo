using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [SugarTable("dbo.Permission")]
    public class Permission : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int PermissionID { get; set; }

        public int PerID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        public int SortNo { get; set; }
        public int GroupNum { get; set; }
    }
}
