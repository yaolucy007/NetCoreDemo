using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [SugarTable("dbo.User")]
    public class User : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int UserID { get; set; }
        public string LoginName { get; set; }
        public string LoginPassword { get; set; }
        public string JobCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar_Img { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string RegistrationID { get; set; }
    }
}
