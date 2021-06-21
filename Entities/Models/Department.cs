using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class DepartmentExtension : Department
    {
        [SugarColumn(IsIgnore = true)]
        public string CreateUserName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string UpdateUserName { get; set; }
    }
}
