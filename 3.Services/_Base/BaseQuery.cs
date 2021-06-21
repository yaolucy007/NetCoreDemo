using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace Services
{
    public class BaseQuery<TEntity> : IQuery<TEntity> where TEntity : BaseEntity
    {
        #region DB对象
        private SqlSugarClient DB
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

        public ISugarQueryable<TEntity> AddClientIDCondition(ISugarQueryable<TEntity> query)
        {
            return query.Where(x => x.ClientID == ClientID);
        }

        public ISugarQueryable<TEntity> GetAll(ISugarQueryable<TEntity> query)
        {
            return query.Where(x => x.ClientID == ClientID);
        }

        public int GetCount(ISugarQueryable<TEntity> query)
        {
            return query.Where(x => x.ClientID == ClientID).Count();
        }

        public int GetCountBySQL(string sql, IEnumerable<SugarParameter> values = null)
        {
            if (!sql.ToUpper().Contains("CLIENTID"))
            {
                throw new ArgumentException("SQL语句不包含Client数据隔离条件，不允许查询");
            }
            else
            {
                return (int)DB.Ado.GetScalar(sql, values);
            }
        }

        public TEntity GetModel(ISugarQueryable<TEntity> where, string orderBy = null)
        {
            var temp = where.Where(x => x.ClientID == ClientID);
            if (!string.IsNullOrEmpty(orderBy))
            {
                temp = temp.OrderBy(orderBy);
            }
            return temp.First();
        }

        public TEntity GetModelBySQL(string sql, IEnumerable<SugarParameter> values = null)
        {
            if (!sql.ToUpper().Contains("CLIENTID"))
            {
                throw new ArgumentException("SQL语句不包含Client数据隔离条件，不允许查询");
            }
            else
            {
                return DB.Ado.SqlQuerySingle<TEntity>(sql, values);
            }
        }

        public ISugarQueryable<TEntity> GetPage(ISugarQueryable<TEntity> where, int pageIndex, int pageSize = 20, string orderBy = null)
        {
            var temp = where.Where(x => x.ClientID == ClientID);
            if (!string.IsNullOrEmpty(orderBy))
            {
                temp = temp.OrderBy(orderBy);
            }
            return temp.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public ISugarQueryable<TEntity> GetSearch(ISugarQueryable<TEntity> where, string orderBy = null, int? top = null)
        {
            var temp = where.Where(x => x.ClientID == ClientID);
            if (!string.IsNullOrEmpty(orderBy))
            {
                temp = temp.OrderBy(orderBy);
            }
            if (top.HasValue && top.Value > 0)
            {
                temp = temp.Take(top.Value);
            }
            return temp;
        }

        public List<TEntity> GetSearchBySQL(string sql, IEnumerable<SugarParameter> values = null)
        {
            if (!sql.ToUpper().Contains("CLIENTID"))
            {
                throw new ArgumentException("SQL语句不包含Client数据隔离条件，不允许查询");
            }
            else
            {
                return DB.Ado.SqlQuery<TEntity>(sql, values);
            }
        }


    }
}
