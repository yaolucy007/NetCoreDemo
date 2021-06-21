using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemService.Controllers
{
    [Authorize]
    public class AuthorizeBaseController : BaseController
    {
        public int CurrentUserID
        {
            get
            {
                try
                {
                    return Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "UserID").Value);
                }
                catch (Exception)
                {

                    throw new Exception("无法获取当前登录用户信息");
                }
            }
        }

        public int ClientID
        {
            get
            {
                try
                {
                    return Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "ClientID").Value);
                }
                catch (Exception)
                {

                    throw new Exception("无法获取当前登录客户信息");
                }
            }
        }

        public string CurrentUserName
        {
            get
            {
                try
                {
                    return HttpContext.User.Claims.First(x => x.Type == "UserName").Value;
                }
                catch (Exception)
                {

                    throw new Exception("无法获取当前登录客户信息");
                }
            }
        }

        public List<int> RoleIDs
        {
            get
            {
                try
                {
                    List<int> result = new List<int>();
                    var temp = HttpContext.User.Claims.First(x => x.Type == "RoleIDs").Value.Split(',');
                    foreach (var item in temp)
                    {
                        result.Add(Convert.ToInt32(item));
                    }
                    return result;
                }
                catch (Exception)
                {

                    throw new Exception("无法获取当前角色集合信息");
                }
            }
        }

        public List<int> DepartmentIDs
        {
            get
            {
                try
                {
                    List<int> result = new List<int>();
                    var temp = HttpContext.User.Claims.First(x => x.Type == "DepartmentIDs").Value.Split(',');
                    foreach (var item in temp)
                    {
                        result.Add(Convert.ToInt32(item));
                    }
                    return result;
                }
                catch (Exception)
                {

                    throw new Exception("无法获取当前角色集合信息");
                }
            }
        }
    }
}
