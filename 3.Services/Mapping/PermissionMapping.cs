using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace Services
{
    public interface IPermissionMapping : IMapping<PermissionExtension, PermissionModel, PermissionFilterModel>
    {

    }

    public class PermissionMapping : BaseMapping<PermissionExtension, PermissionModel, PermissionFilterModel>, IPermissionMapping
    {
        public PermissionModel Entity2Model(PermissionExtension entity)
        {
            PermissionModel model = new PermissionModel();

            model.PermissionID = entity.PermissionID;
            model.PerID = entity.PerID;
            model.Name = entity.Name;
            model.ParentID = entity.ParentID;
            model.SortNo = entity.SortNo;
            model.GroupNum = entity.GroupNum;

            return model;
        }

        public IEnumerable<PermissionModel> Entity2Model(IEnumerable<PermissionExtension> entities)
        {
            List<PermissionModel> result = new List<PermissionModel>();

            foreach (var item in entities)
            {
                result.Add(Entity2Model(item));
            }

            return result;
        }

        public PermissionExtension Model2Entity(PermissionModel model)
        {
            PermissionExtension entity = new PermissionExtension();

            entity.PermissionID = model.PermissionID;
            entity.PerID = model.PerID;
            entity.Name = model.Name;
            entity.ParentID = model.ParentID;
            entity.SortNo = model.SortNo;
            entity.GroupNum = model.GroupNum;

            return entity;
        }

        public IEnumerable<PermissionExtension> Model2Entity(IEnumerable<PermissionModel> models)
        {
            List<PermissionExtension> result = new List<PermissionExtension>();

            foreach (var item in models)
            {
                result.Add(Model2Entity(item));
            }

            return result;
        }

        public override Expressionable<PermissionExtension> FilterModel2Where(PermissionFilterModel filterModel)
        {
            var expr = base.FilterModel2Where(filterModel);

            if (string.IsNullOrEmpty(filterModel._Name))
            {
                expr = expr.And(x => x.Name == filterModel._Name);
            }

            return expr;
        }
    }
}
