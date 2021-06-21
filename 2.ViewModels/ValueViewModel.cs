using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    /// <summary>
    /// 特定的返回对象，对于处理结果中不需要返回模型数据，仅返回数据标识的，建议使用int类型的Data属性统一进行返回
    /// 0表示无业务返回值的成功，>0代表具体的业务返回值，小于0代表失败，可在业务中对值的具体含义进行规定
    /// </summary>
    public class ValueViewModel : BaseViewModel
    {
        public int Data { get; set; }
    }
}
