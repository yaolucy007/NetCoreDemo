using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace Services
{
    public interface IQuery<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// 客户标识
        /// </summary>
        string ClientID { get; set; }

        /// <summary>
        /// 追加客户标识的查询条件，用于特定的特殊查询，无法通过query进行查询实现时必须要添加
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ISugarQueryable<TEntity> AddClientIDCondition(ISugarQueryable<TEntity> query);
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ISugarQueryable<TEntity> GetAll(ISugarQueryable<TEntity> query);
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        int GetCount(ISugarQueryable<TEntity> query);
        /// <summary>
        /// 根据SQL语句与入参获取数量，参数中必须包含ClientID的参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        int GetCountBySQL(string sql, IEnumerable<SugarParameter> values = null);
        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        TEntity GetModel(ISugarQueryable<TEntity> where, string orderBy = null);
        /// <summary>
        /// 根据SQL语句获取单个对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        TEntity GetModelBySQL(string sql, IEnumerable<SugarParameter> values = null);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        ISugarQueryable<TEntity> GetPage(ISugarQueryable<TEntity> where, int pageIndex, int pageSize = 20, string orderBy = null);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        ISugarQueryable<TEntity> GetSearch(ISugarQueryable<TEntity> where, string orderBy = null, int? top = null);
        /// <summary>
        /// 根据SQL语句进行查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        List<TEntity> GetSearchBySQL(string sql, IEnumerable<SugarParameter> values = null);
    }
}
