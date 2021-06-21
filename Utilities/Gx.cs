
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Utilities
{
    public static class Gx
    {
        public static IConfiguration Configuration { get; set; }
        static Gx()
        {
            Olock = new object();

            var fileName = "appsettings.json";

            var directory = AppContext.BaseDirectory;
            //directory = directory.Replace(" ", "/");

            var filePath = $"{directory}/{fileName}";
            if (!File.Exists(filePath))
            {
                var length = directory.IndexOf("/bin");
                filePath = $"{directory.Substring(0, length)}/{fileName}";
            }

            var builder = new ConfigurationBuilder()
                .AddJsonFile(filePath, false, true);

            Configuration = builder.Build();

            //Configuration = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
        }

        //全局锁，用在log里
        private static readonly object Olock = new object();



        //public Gx() { }

        #region 静态变量
        //public static string _ClientID = Configuration["ClientID"];

        public static string _ClientID()
        {
            return Configuration["ClientID"];
        }

        ///// <summary>
        ///// 七牛--对象存储里的空间名称
        ///// </summary>
        //public static string _Qiniu_Bucket = Configuration["Qiniu_Bucket"];

        ///// <summary>
        ///// 七牛--当前空间下配置的域名
        ///// </summary>
        //public static string _Qiniu_Domain = Configuration["Qiniu_Domain"];

        ///// <summary>
        ///// 七牛--当前空间所在的存储区域
        ///// </summary>
        //public static string _Qiniu_BucketArea = Configuration["Qiniu_BucketArea"];

        ///// <summary>
        ///// 是否是生产环境
        ///// </summary>
        //public static bool _IsProduction = Configuration["IsProduction"] == "1";

        ///// <summary>
        ///// 是否记录请求日志
        ///// </summary>
        //public static bool _IsRequestLog = Configuration["IsRequestLog"] == "1";

        ///// <summary>
        ///// 是否记录SQL日志
        ///// </summary>
        //public static bool _IsSqlLog = Configuration["IsSqlLog"] == "1";

        ///// <summary>
        ///// 极光推送KEY
        ///// </summary>
        //public static string _JpushKey = Configuration["JpushKey"];

        ///// <summary>
        ///// 极光推送MASTERSECRET
        ///// </summary>
        //public static string _JpushMasterSecret = Configuration["JpushMasterSecret"];

        ///// <summary>
        ///// 质检确认权限的标识ID
        ///// </summary>
        //public static int _CheckPermissionPerID = CC.Obj2Int(Configuration["CheckPermissionPerID"]);

        #endregion

        #region 常量的定义
        /// <summary>
        /// Des加密用的key
        /// </summary>
        private const string _DES_KEY = "zyzzmes";

        /// <summary>
        /// 登录用户ID的Key
        /// </summary>
        public const string _USERID_KEY = "UserID";

        /// <summary>
        /// 登录用户名称的Key
        /// </summary>
        public const string _USERNAME_KEY = "UserName";

        /// <summary>
        /// 登录用户部门的ID的Key
        /// </summary>
        public const string _DEPARTMENTID_KEY = "DepartmentID";

        /// <summary>
        /// 登录用户部门的名称的Key
        /// </summary>
        public const string _DEPARTMENTNAME_KEY = "DepartmentName";

        /// <summary>
        /// 登录用户ClientID的Key
        /// </summary>
        public const string _CLIENTID_KEY = "ClientID";

        /// <summary>
        /// 分页时每页数据量
        /// </summary>
        public const int _PAGE_SIZE = 20;

        // <summary>
        /// 七牛 账户下的AccessKey
        /// </summary>
        public const string _QINIU_ACCESSKEY = "m7FcWgTFdwWJFebbBH80BIHNqb4zwDvgH97Aclff";

        /// <summary>
        /// 七牛 账户下的SecretKey
        /// </summary>
        public const string _QINIU_SECRETKEY = "_aT7mHs6d1Dn5Kk2r5P4gMg4o15g7068mWLpV8SJ";
        #endregion

        //#region DES加密  DesEncrypt
        ///// <summary>
        ///// DES加密
        ///// </summary>
        ///// <param name="str">明文</param>
        ///// <param name="key">密钥</param>
        ///// <returns></returns>
        //public static string DesEncrypt(string str, string key = _DES_KEY)
        //{
        //    return DESHelper.Encrypt(str, key);
        //}
        //#endregion

        //#region DES解密  DesDecrypt
        ///// <summary>
        ///// DES解密
        ///// </summary>
        ///// <param name="str">密文</param>
        ///// <param name="key">密钥</param>
        ///// <returns></returns>
        //public static string DesDecrypt(string str, string key = _DES_KEY)
        //{
        //    return DESHelper.Decrypt(str, key);
        //}
        //#endregion

        #region 不会报异常的序列化  Serialize
        /// <summary>
        /// 不会报异常的序列化，异常时返回空字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            string result = string.Empty;
            try
            {
                result = JsonConvert.SerializeObject(obj);
            }
            catch { }

            return result;
        }
        #endregion

        #region 不会报异常的反序列化  Deserialize
        /// <summary>
        /// 不会报异常的反序列化，异常时返回默认值，如int则返回0，对象返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonStr)
        {
            T t = default(T);
            try
            {
                t = JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch { }
            return t;
        }
        #endregion

        #region DataTable转换成对应的实体集合  DataTable2Json
        /// <summary>
        /// DataTable转换成对应的实体集合
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="dt">dataTable对应</param>
        /// <returns></returns>
        public static IEnumerable<T> DataTable2Json<T>(DataTable dt)
        {
            string jsonStr = JsonConvert.SerializeObject(dt, new DataTableConverter());
            return Deserialize<IEnumerable<T>>(jsonStr);
        }
        #endregion

        #region 记录本地日志  Log
        /// <summary>
        /// 记录本地日志
        /// </summary>
        /// <param name="con">日志记录的内容</param>
        public static void Log(string con, string fileNamePre = "")
        {
            ThreadPool.QueueUserWorkItem(delegate (object state)
            {
                object olock = Olock;
                lock (olock)
                {
                    try
                    {
                        string fileName = fileNamePre + DateTime.Now.ToString("yyyyMMdd-HH") + ".txt";
                        string path = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\";
                        DirectoryInfo info = new DirectoryInfo(path);
                        if (!info.Exists)
                        {
                            info.Create();
                        }

                        using (FileStream stream = new FileStream(path + fileName, FileMode.Append, FileAccess.Write))
                        {
                            StreamWriter writer = new StreamWriter(stream);
                            writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            writer.WriteLine(con);
                            writer.WriteLine();
                            writer.WriteLine("-----------------------------------------------------------------");
                            writer.Flush();
                            writer.Close();
                        }
                    }
                    catch
                    {
                    }
                }
            });
        }
        #endregion

        #region 返回枚举的描述信息  GetEnumDescription
        /// <summary>
        /// 返回枚举的描述信息
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumVal">枚举值</param>
        /// <param name="nullVal">如果为空或异常时返回的内容，默认为空串</param>
        /// <returns></returns>
        public static string GetEnumDescription<TEnum>(TEnum enumVal, string nullVal = "")
        {
            try
            {
                FieldInfo fieldInfo = enumVal.GetType().GetField(enumVal.ToString());
                DescriptionAttribute[] descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumVal.ToString();
            }
            catch (Exception)
            {
                return nullVal;
            }
        }
        #endregion

        #region 根据枚举类型得到定义的名称  GetEnumName
        /// <summary>
        /// 根据枚举类型得到定义的名称
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumName(Type type, int value)
        {
            string result = "-";
            result = Enum.IsDefined(type, value) ? Enum.GetName(type, value) : result;
            return result;
        }
        #endregion

        //#region 将枚举转换成数据集合
        ///// <summary>
        ///// 将枚举转换成数据集合
        ///// </summary>
        ///// <param name="type">传入typeof(枚举类型)</param>
        ///// <returns></returns>
        //public static List<EnumModel> GetEnumList(Type type)
        //{
        //    List<EnumModel> result = new List<EnumModel>();
        //    var strList = Enum.GetNames(type).ToList();
        //    foreach (string key in strList)
        //    {
        //        string val = Enum.Format(type, Enum.Parse(type, key), "d");
        //        result.Add(new EnumModel(key, CC.Obj2Int(val)));
        //    }
        //    return result;
        //}
        //#endregion

        //#region Hash256散列  SHA256
        ///// <summary>
        ///// Hash256散列
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static string SHA256(string str)
        //{
        //    return SHAHelper.SHA256(str);
        //}
        //#endregion

        //#region HMAC_SHA256加密  HMAC_SHA256
        ///// <summary>
        ///// HMAC_SHA256加密
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public static byte[] HMAC_SHA256(byte[] key, string msg)
        //{
        //    using (HMACSHA256 mac = new HMACSHA256(key))
        //    {
        //        return mac.ComputeHash(Encoding.UTF8.GetBytes(msg));
        //    }
        //}
        //#endregion

        //#region 获取IP地址  GetIp
        ///// <summary>
        ///// 获取IP地址
        ///// </summary>
        ///// <returns></returns>
        //public static string GetIp()
        //{
        //    if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.ServerVariables == null)
        //    {
        //        return "";
        //    }

        //    string userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    if (string.IsNullOrEmpty(userHostAddress))
        //    {
        //        if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
        //        {
        //            userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
        //        }
        //    }

        //    if (string.IsNullOrEmpty(userHostAddress))
        //    {
        //        userHostAddress = HttpContext.Current.Request.UserHostAddress;
        //    }

        //    //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
        //    if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
        //    {
        //        return userHostAddress;
        //    }

        //    return "127.0.0.1";
        //}
        //#endregion

        //#region 检查IP地址格式  IsIP
        ///// <summary>
        ///// 检查IP地址格式
        ///// </summary>
        ///// <param name="ip"></param>
        ///// <returns></returns>
        //public static bool IsIP(string ip)
        //{
        //    return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        //}
        //#endregion

        #region 得到一个6位数的随机数，用于验证码  GetCode
        /// <summary>
        /// 得到一个6位数的随机数，用于验证码
        /// </summary>
        /// <returns></returns>
        public static string GetCode()
        {
            Random ran = new Random();
            return ran.Next(100000, 1000000).ToString();
        }
        #endregion

        #region 对象的深拷贝
        /// <summary>
        /// 对象的深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(T obj)
        {
            return Deserialize<T>(Serialize(obj));
        }
        #endregion

        //#region 生成Code
        ///// <summary>
        ///// 生成Code，主体内容为yyyyMMddhhmmssfff
        ///// </summary>
        ///// <param name="left">前缀</param>
        ///// <param name="right">后缀</param>
        ///// <param name="isSplit">是否在前缀或后缀里加入分隔符</param>
        ///// <param name="splitStr">分隔符</param>
        ///// <returns></returns>
        //public static string GetCode(string left = null, string right = null, bool isSplit = true, string splitStr = "-")
        //{
        //    string body = CC.GetNowNoSplit();

        //    if (!CC.IsEmpty(left))
        //    {
        //        body = isSplit ? (left + splitStr + body) : (left + body);
        //    }
        //    if (!CC.IsEmpty(right))
        //    {
        //        body = isSplit ? (body + splitStr + right) : (body + right);
        //    }

        //    return body;
        //}
        //#endregion

        #region 数字前面补零
        /// <summary>
        /// 数字前面补零
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="n">位数</param>
        /// <returns></returns>
        public static string FormatNumber(int num, int n = 3)
        {
            return num.ToString("D" + n);
        }
        #endregion

        #region 对比2个对象是否相同
        public static bool ObjectEqual(object obj1, object obj2)
        {
            return Serialize(obj1).Equals(Serialize(obj2));
        }
        #endregion
    }
}
