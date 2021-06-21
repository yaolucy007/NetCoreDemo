using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace Services
{
    public class BaseUnitOfWork : IUnitOfWork
    {
        private Dictionary<BaseEntity, Action> addEntities;
        private Dictionary<BaseEntity, Action> updateEntities;
        private Dictionary<BaseEntity, Action> deleteEntities;

        public string ClientID { get; set; }

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

        public BaseUnitOfWork()
        {
            addEntities = new Dictionary<BaseEntity, Action>();
            updateEntities = new Dictionary<BaseEntity, Action>();
            deleteEntities = new Dictionary<BaseEntity, Action>();
        }

        public void Commit()
        {
            try
            {
                DB.Ado.BeginTran();


                foreach (var entity in deleteEntities.Keys)
                {
                    deleteEntities[entity]();
                }

                foreach (var entity in updateEntities.Keys)
                {
                    entity.ClientID = ClientID;
                    updateEntities[entity]();
                }

                foreach (var entity in addEntities.Keys)
                {
                    entity.ClientID = ClientID;
                    addEntities[entity]();
                }



                DB.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                DB.Ado.RollbackTran();

                throw new Exception("事务提交失败：" + ex.Message);
            }
        }

        #region RegisterAdd
        public void RegisterAdd(BaseEntity entity, Action act)
        {
            addEntities.Add(entity, act);
        }
        #endregion

        #region RegisterDelete
        public void RegisterDelete(BaseEntity entity, Action act)
        {
            deleteEntities.Add(entity, act);
        }
        #endregion

        #region RegisterUpdate
        public void RegisterUpdate(BaseEntity entity, Action act)
        {
            updateEntities.Add(entity, act);
        }
        #endregion

        #region ClearAdd
        public void ClearAdd()
        {
            addEntities.Clear();
        }
        #endregion

        #region ClearDelete
        public void ClearDelete()
        {
            deleteEntities.Clear();
        }
        #endregion

        #region ClearUpdate
        public void ClearUpdate()
        {
            updateEntities.Clear();
        }
        #endregion

        public string GetClientID()
        {
            return ClientID;
        }
    }
}
