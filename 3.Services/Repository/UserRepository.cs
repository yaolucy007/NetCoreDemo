using Entities;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IUserRepository : IRepository<UserExtension>
    {
        /// <summary>
        /// 获取权限ID去重集合
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        List<int> GetMePermissionIDs(List<int> roleIds);
    }
    public class UserRepository : BaseRepository<UserExtension>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {

        }


        #region 获取权限ID去重集合
        public List<int> GetMePermissionIDs(List<int> roleIds)
        {
            return DB.Queryable<RolePermissionExtension>().Where(y => SqlFunc.Subqueryable<UserRole>().Where(z => roleIds.Contains(z.RoleID) && z.RoleID == y.RoleID).Any()).Select(x => x.PermissionID).Distinct().ToList();
        }
        #endregion
    }
}
