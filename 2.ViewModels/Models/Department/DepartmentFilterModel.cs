using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class DepartmentFilterModel : BaseFilterModel
    {
        public int? _LeaderID { get; set; }
        public string _Name { get; set; }
        public int? _ParentID { get; set; }

        public List<int> _DepartmentIDs { get; set; }
    }
}
