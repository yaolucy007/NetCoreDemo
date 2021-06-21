using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// 排序类用数据模型
    /// </summary>
    public class OrderByModel
    {
        public OrderByModel()
        { }

        public OrderByModel(string orderByFieldName, SortType type)
        {
            this.OrderByFieldName = orderByFieldName;
            this.Type = type;
        }


        /// <summary>
        /// 字段名称
        /// </summary>
        public string OrderByFieldName { get; set; }

        /// <summary>
        /// 排列顺序
        /// </summary>
        public SortType Type { get; set; }
    }
}
