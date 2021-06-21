using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Utilities;
using ViewModels;

namespace Services
{
    public class BaseMapping<TEntity, TModel, TFilterModel>
        where TEntity : BaseEntity, new()
        where TModel : BaseViewModel
        where TFilterModel : BaseFilterModel, new()
    {
        public virtual Expressionable<TEntity> FilterModel2Where(TFilterModel filterModel)
        {
            if (filterModel == null || Gx.ObjectEqual(filterModel, new TFilterModel()))
            {
                return null;
            }

            return Expressionable.Create<TEntity>();
        }
    }
}
