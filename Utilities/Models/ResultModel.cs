using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// 统一返回结构体，适用于HTTP请求响应的统一结构
    /// </summary>
    /// <typeparam name="T">根据实际返回的ViewModel进行传输</typeparam>
    public class ResultModel<T> where T : class
    {
        /// <summary>
        /// 响应结果状态码
        /// 0-成功；500-内部错误；10X-数据操作失败；X-业务规则验证失败
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 响应结果信息描述
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 响应结果数据
        /// </summary>
        public T Data { get; set; }
    }
}
