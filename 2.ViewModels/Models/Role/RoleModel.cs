using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class RoleModel : BaseViewModel
    {
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

        public override bool IsCheckModelDataValidate()
        {

            if (RoleID <= 0)
            {
                return false;
            }
            return IsCheckModelDataValidateWithoutID();
        }

        public override bool IsCheckModelDataValidateWithoutID()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }
            return true;
        }
    }
}
