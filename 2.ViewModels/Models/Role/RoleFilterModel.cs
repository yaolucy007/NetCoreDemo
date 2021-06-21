using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class RoleFilterModel : BaseFilterModel
    {
        public string _Name { get; set; }

        public bool? _IsAllData { get; set; }

        public List<int> _RoleIDs { get; set; }
    }
}
