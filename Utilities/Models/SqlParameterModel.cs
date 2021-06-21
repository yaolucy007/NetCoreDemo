using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// 自定义SQL参数用到的数据模型
    /// </summary>
    public class SqlParameterModel
    {
        public SqlParameterModel()
        { }

        public SqlParameterModel(string name, DbType type, object value)
        {
            this.Name = name;
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// 参数名称，用@做为标识前缀
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数的数据类型
        /// </summary>
        public DbType Type { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }
    }
}
