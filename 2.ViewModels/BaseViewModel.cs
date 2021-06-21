using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class BaseViewModel
    {
        public bool ServiceIdentity { get; set; }

        public virtual bool IsCheckModelDataValidateWithoutID()
        {
            return true;
        }

        public virtual bool IsCheckModelDataValidate()
        {
            return true;
        }
    }
}
