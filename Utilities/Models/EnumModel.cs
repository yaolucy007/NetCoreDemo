using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// 用于保存枚举数据对象
    /// </summary>
    public class EnumModel
    {
        public EnumModel() { }

        public EnumModel(string key, int val)
        {
            Key = key;
            Value = val;
        }

        public string Key { get; set; }

        public int Value { get; set; }
    }
}
