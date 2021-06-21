using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace Services
{
    public interface IMapping<TEntity, TModel, TFilterModel>
        where TEntity : BaseEntity, new()
        where TModel : BaseViewModel
        where TFilterModel : BaseFilterModel, new()
    {
        TModel Entity2Model(TEntity entity);


        //Model实体转Entity实体，用于写操作
        TEntity Model2Entity(TModel model);


        //Entity实体转Model实体，用于读操作
        IEnumerable<TModel> Entity2Model(IEnumerable<TEntity> entities);


        //Model实体转Entity实体，用于写操作
        IEnumerable<TEntity> Model2Entity(IEnumerable<TModel> models);


        //查询条件转换
        Expressionable<TEntity> FilterModel2Where(TFilterModel filterModel);
    }
}
