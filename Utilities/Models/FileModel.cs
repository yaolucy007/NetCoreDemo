using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// 文件信息记录类，上传标准结构
    /// </summary>
    public class FileModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Sn { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Url { get; set; }
    }
}
