using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class DepartmentModel : BaseViewModel
    {
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

        public string CreateUserName { get; set; }
        public string UpdateUserName { get; set; }

        public override bool IsCheckModelDataValidate()
        {
            if (DepartmentID <= 0)
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
            if (ParentID <= 0)
            {
                return false;
            }
            if (LeaderID <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
