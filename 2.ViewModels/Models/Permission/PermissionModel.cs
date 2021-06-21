using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class PermissionModel : BaseViewModel
    {
        public int PermissionID { get; set; }
        public int PerID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        public int SortNo { get; set; }
        public int GroupNum { get; set; }

        public List<PermissionModel> Children_List { get; set; }

        public override bool IsCheckModelDataValidateWithoutID()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }
            return true;
        }

        public override bool IsCheckModelDataValidate()
        {
            if (PermissionID <= 0)
            {
                return false;
            }

            return IsCheckModelDataValidateWithoutID();
        }
    }
}
