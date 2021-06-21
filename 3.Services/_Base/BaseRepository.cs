using Entities;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace Services
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        #region DB对象
        public SqlSugarClient DB
        {
            get
            {
                return new SqlSugarClient(new ConnectionConfig()
                {
                    //数据库类型
                    DbType = DbType.SqlServer,

                    //连接字符串
                    ConnectionString = Gx.Configuration["ConnectionStrings:DefaultConnectionString"],

                    //ORM读取自增列和主键的方式
                    InitKeyType = InitKeyType.Attribute,

                    //自动释放和关闭数据库连接，如果有事务事务结束时关闭，否则每次操作后关闭
                    IsAutoCloseConnection = true,

                    //事件
                    AopEvents = new AopEvents
                    {
                        //SQL执行前
                        OnLogExecuting = (sql, param) =>
                        {
                            var paramList = param.Select(x => string.Format("{0} = {1}", x.ParameterName, x.Value));
                            var con = sql + "\r\n" + string.Join("\r\n", paramList);
                            //Gx.Log(con);
                        },

                        //SQL执行出错
                        OnError = (expr) =>
                        {
                            var param = (SugarParameter[])expr.Parametres;
                            var paramList = param.Select(x => string.Format("{0} = {1}", x.ParameterName, x.Value));
                            var con = expr.Sql + "\r\n" + string.Join("\r\n", paramList);
                            //Gx.Log(con, "Sql-Error-");
                        },

                        //SQL执行完成，执行出错时不会运行到这里
                        OnLogExecuted = (sql, param) =>
                        {
                            if (DB.Ado.SqlExecutionTime.TotalSeconds > 1)
                            {
                                var paramList = param.Select(x => string.Format("{0} = {1}", x.ParameterName, x.Value));
                                var con = sql + "\r\n" + string.Join("\r\n", paramList);
                                //Gx.Log(con, "Sql-Time-");
                            }
                        }
                    }
                });
            }
        }
        #endregion

        public string ClientID { get; set; }

        public IUnitOfWork _unitOfWork;
        public IQuery<TEntity> _query = new BaseQuery<TEntity>();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseRepository(IUnitOfWork __unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = __unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            if (httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "ClientID") != null)
            {
                string clientID = httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "ClientID").Value;
                Init(clientID);
            }

        }

        #region 批量删除
        public virtual void BatchDelete(IEnumerable<int> ids)
        {
            try
            {
                DB.Ado.BeginTran();
                foreach (var item in ids)
                {
                    DB.Deleteable<TEntity>(item).ExecuteCommand();
                }
                DB.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                DB.Ado.RollbackTran();
                throw new Exception("事务提交失败：" + ex.Message);
            }

        }
        #endregion

        #region 批量添加
        public virtual void BatchInsert(IEnumerable<TEntity> entities)
        {
            try
            {
                DB.Ado.BeginTran();
                foreach (var item in entities)
                {
                    _unitOfWork.RegisterAdd(item, () => DB.Insertable<TEntity>(item).ExecuteReturnIdentity());
                }
                DB.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                DB.Ado.RollbackTran();
                throw new Exception("事务提交失败：" + ex.Message);
            }
        }
        #endregion

        #region 批量更新
        public virtual void BatchUpdate(IEnumerable<TEntity> entities)
        {
            try
            {
                DB.Ado.BeginTran();
                foreach (var item in entities)
                {
                    _unitOfWork.RegisterUpdate(item, () => DB.Updateable<TEntity>(item).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommand());
                }
                DB.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                DB.Ado.RollbackTran();
                throw new Exception("事务提交失败：" + ex.Message);
            }
        }
        #endregion

        #region 物理删除
        public virtual int Delete(int id)
        {
            int result = 0;
            result = DB.Deleteable<TEntity>(id).ExecuteCommand();
            return result;
        }
        #endregion

        #region 执行SQL语句，返回受影响的数据行数
        public virtual int ExecuteNonQueryBySQL(string Sql, IEnumerable<SugarParameter> values = null)
        {
            if (!Sql.ToUpper().Contains("CLIENTID"))
            {
                throw new ArgumentException("SQL语句不包含Client数据隔离条件，不允许查询");
            }
            else
            {
                return DB.Ado.ExecuteCommand(Sql, values);
            }
        }
        #endregion

        #region 执行SQL语句，返回第一行第一列的结果
        public virtual int ExecuteScalarBySQL(string Sql, IEnumerable<SugarParameter> values = null)
        {
            if (!Sql.ToUpper().Contains("CLIENTID"))
            {
                throw new ArgumentException("SQL语句不包含Client数据隔离条件，不允许查询");
            }
            else
            {
                return (int)DB.Ado.GetScalar(Sql, values);
            }
        }
        #endregion

        public virtual ISugarQueryable<TEntity> GetAll(string orderBy = null)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return _query.GetAll(DB.Queryable<TEntity>());
            }
            else
            {
                return _query.GetAll(DB.Queryable<TEntity>().OrderBy(orderBy));
            }
        }

        public virtual int GetCount(Expressionable<TEntity> expressionable)
        {
            return _query.GetCount(DB.Queryable<TEntity>().Where(expressionable.ToExpression()));
        }

        public virtual int GetCountBySQL(string Sql, IEnumerable<SugarParameter> values = null)
        {
            if (!Sql.ToUpper().Contains("CLIENTID"))
            {
                throw new ArgumentException("SQL语句不包含Client数据隔离条件，不允许查询");
            }
            else
            {
                return (int)DB.Ado.GetScalar(Sql, values);
            }
        }

        public virtual TEntity GetModel(Expressionable<TEntity> expressionable, string orderBy = null)
        {
            return _query.GetModel(DB.Queryable<TEntity>().Where(expressionable.ToExpression()), orderBy);
        }

        public virtual TEntity GetModelByPrimaryKey(int id)
        {
            return DB.Queryable<TEntity>().InSingle(id);
        }

        public virtual TEntity GetModelBySQL(string Sql, IEnumerable<SugarParameter> values = null)
        {
            return _query.GetModelBySQL(Sql, values);
        }

        public virtual ISugarQueryable<TEntity> GetPage(Expressionable<TEntity> expressionable, int pageIndex, int pageSize, string orderby)
        {
            return _query.GetPage(DB.Queryable<TEntity>().Where(expressionable.ToExpression()), pageIndex, pageSize, orderby);
        }

        public virtual ISugarQueryable<TEntity> GetSearch(Expressionable<TEntity> expressionable, string orderby)
        {
            return _query.GetSearch(DB.Queryable<TEntity>().Where(expressionable.ToExpression()), orderby);
        }

        public virtual List<TEntity> GetSearchBySQL(string Sql, IEnumerable<SugarParameter> values = null)
        {
            return _query.GetSearchBySQL(Sql, values);
        }

        public virtual void Init(string clientID)
        {
            ClientID = clientID;
            _unitOfWork.ClientID = clientID;
            _query.ClientID = clientID;
        }

        #region 添加
        public virtual int Insert(TEntity entity)
        {
            int res = 0;
            _unitOfWork.RegisterAdd(entity, () => res = DB.Insertable<TEntity>(entity).ExecuteReturnIdentity());
            _unitOfWork.Commit();
            return res;
        }
        #endregion

        #region 更新
        public virtual int Update(TEntity entity)
        {
            var temp = DB.Updateable<TEntity>(entity).IgnoreColumns(ignoreAllNullColumns: true).ToSql();

            int res = 0;
            _unitOfWork.RegisterUpdate(entity, () => res = DB.Updateable<TEntity>(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommand());
            _unitOfWork.Commit();
            return res;
        }
        #endregion
    }
}
