using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class UserModel : BaseViewModel
    {
        public int UserID { get; set; }
        public string LoginName { get; set; }
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


        public override bool IsCheckModelDataValidate()
        {
            if (UserID <= 0)
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

    public class Me : UserModel
    {
        public List<RoleModel> Role_List { get; set; }

        public List<DepartmentModel> Department_List { get; set; }


    }
}
