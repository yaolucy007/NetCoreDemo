using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// 分页查询结果结构体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageModel<T> where T : class
    {
        /// <summary>
        /// 数据总量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前页的数据集合
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}
