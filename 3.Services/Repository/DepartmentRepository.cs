using Entities;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IDepartmentRepository : IRepository<DepartmentExtension>
    {

    }

    public class DepartmentRepository : BaseRepository<DepartmentExtension>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {
        }

        public override ISugarQueryable<DepartmentExtension> GetAll(string orderBy = null)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return _query.GetAll(DB.Queryable<Department, User, User>((x, b, c) => new JoinQueryInfos(
                        JoinType.Left, x.CreateUserID == b.UserID,
                        JoinType.Left, x.UpdateUserID == c.UserID
                    )
                ).Select((x, b, c) => new DepartmentExtension
                {
                    DepartmentID = SqlFunc.GetSelfAndAutoFill(x.DepartmentID),
                    CreateUserName = b.Name,
                    UpdateUserName = c.Name
                }));
            }
            else
            {
                return _query.GetAll(DB.Queryable<Department, User, User>((x, b, c) => new JoinQueryInfos(
                        JoinType.Left, x.CreateUserID == b.UserID,
                        JoinType.Left, x.UpdateUserID == c.UserID
                    )
                ).Select((x, b, c) => new DepartmentExtension
                {
                    DepartmentID = SqlFunc.GetSelfAndAutoFill(x.DepartmentID),
                    CreateUserName = b.Name,
                    UpdateUserName = c.Name
                }).OrderBy(orderBy));
            }
        }

        public override ISugarQueryable<DepartmentExtension> GetSearch(Expressionable<DepartmentExtension> expressionable, string orderby)
        {
            return _query.GetSearch(DB.Queryable<Department, User, User>((x, b, c) => new JoinQueryInfos(
                        JoinType.Left, x.CreateUserID == b.UserID,
                        JoinType.Left, x.UpdateUserID == c.UserID
                    )
                ).Select((x, b, c) => new DepartmentExtension
                {
                    DepartmentID = SqlFunc.GetSelfAndAutoFill(x.DepartmentID),
                    CreateUserName = b.Name,
                    UpdateUserName = c.Name
                }).Where(expressionable.ToExpression()), orderby);
        }

        public override ISugarQueryable<DepartmentExtension> GetPage(Expressionable<DepartmentExtension> expressionable, int pageIndex, int pageSize, string orderby)
        {
            return _query.GetPage(DB.Queryable<Department, User, User>((x, b, c) => new JoinQueryInfos(
                        JoinType.Left, x.CreateUserID == b.UserID,
                        JoinType.Left, x.UpdateUserID == c.UserID
                    )
                ).Select((x, b, c) => new DepartmentExtension
                {
                    DepartmentID = SqlFunc.GetSelfAndAutoFill(x.DepartmentID),
                    CreateUserName = b.Name,
                    UpdateUserName = c.Name
                }).Where(expressionable.ToExpression()), pageIndex, pageSize, orderby);
        }

        public override DepartmentExtension GetModelByPrimaryKey(int id)
        {
            return DB.Queryable<Department, User, User>((x, b, c) => new JoinQueryInfos(
                        JoinType.Left, x.CreateUserID == b.UserID,
                        JoinType.Left, x.UpdateUserID == c.UserID
                    )
                ).Select((x, b, c) => new DepartmentExtension
                {
                    DepartmentID = SqlFunc.GetSelfAndAutoFill(x.DepartmentID),
                    CreateUserName = b.Name,
                    UpdateUserName = c.Name
                }).InSingle(id);
        }

        public override DepartmentExtension GetModel(Expressionable<DepartmentExtension> expressionable, string orderBy = null)
        {
            return _query.GetModel(DB.Queryable<Department, User, User>((x, b, c) => new JoinQueryInfos(
                        JoinType.Left, x.CreateUserID == b.UserID,
                        JoinType.Left, x.UpdateUserID == c.UserID
                    )
                ).Select((x, b, c) => new DepartmentExtension
                {
                    DepartmentID = SqlFunc.GetSelfAndAutoFill(x.DepartmentID),
                    CreateUserName = b.Name,
                    UpdateUserName = c.Name
                }).Where(expressionable.ToExpression()), orderBy);
        }
    }

}
