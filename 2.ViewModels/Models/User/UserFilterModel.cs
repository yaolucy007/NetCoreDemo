using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class UserFilterModel : BaseFilterModel
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string _Name { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string _LoginName { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string _JobCode { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string _Phone { get; set; }

    }
}
