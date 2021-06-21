using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace Services
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        void Init(string clientID);

        int Insert(TEntity entity);
        void BatchInsert(IEnumerable<TEntity> entities);

        int Update(TEntity entity);
        void BatchUpdate(IEnumerable<TEntity> entities);

        int Delete(int id);
        void BatchDelete(IEnumerable<int> ids);

        TEntity GetModel(Expressionable<TEntity> expressionable, string orderBy = null);
        TEntity GetModelByPrimaryKey(int id);
        int GetCount(Expressionable<TEntity> expressionable);
        ISugarQueryable<TEntity> GetAll(string orderBy = null);
        ISugarQueryable<TEntity> GetSearch(Expressionable<TEntity> expressionable, string orderby = null);
        ISugarQueryable<TEntity> GetPage(Expressionable<TEntity> expressionable, int pageIndex, int pageSize, string orderby = null);

        TEntity GetModelBySQL(string Sql, IEnumerable<SugarParameter> values = null);
        List<TEntity> GetSearchBySQL(string Sql, IEnumerable<SugarParameter> values = null);
        int GetCountBySQL(string Sql, IEnumerable<SugarParameter> values = null);
        int ExecuteNonQueryBySQL(string Sql, IEnumerable<SugarParameter> values = null);
        int ExecuteScalarBySQL(string Sql, IEnumerable<SugarParameter> values = null);
    }
}
