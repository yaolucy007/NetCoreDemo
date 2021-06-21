using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace Services
{
    public interface IRoleMapping : IMapping<RoleExtension, RoleModel, RoleFilterModel>
    {

    }

    public class RoleMapping : BaseMapping<RoleExtension, RoleModel, RoleFilterModel>, IRoleMapping
    {
        public RoleModel Entity2Model(RoleExtension entity)
        {
            RoleModel model = new RoleModel();

            model.RoleID = entity.RoleID;
            model.Name = entity.Name;
            model.Description = entity.Description;
            model.Remark = entity.Remark;
            model.IsDelete = entity.IsDelete;
            model.IsAllData = entity.IsAllData;
            model.CreateUserID = entity.CreateUserID;
            model.CreateDate = entity.CreateDate;
            model.UpdateUserID = entity.UpdateUserID;
            model.UpdateDate = entity.UpdateDate;

            return model;
        }

        public IEnumerable<RoleModel> Entity2Model(IEnumerable<RoleExtension> entities)
        {
            List<RoleModel> result = new List<RoleModel>();

            foreach (var item in entities)
            {
                result.Add(Entity2Model(item));
            }

            return result;
        }

        public RoleExtension Model2Entity(RoleModel model)
        {
            RoleExtension entity = new RoleExtension();

            entity.RoleID = model.RoleID;
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Remark = model.Remark;
            entity.IsDelete = model.IsDelete;
            entity.IsAllData = model.IsAllData;
            entity.CreateUserID = model.CreateUserID;
            entity.CreateDate = model.CreateDate;
            entity.UpdateUserID = model.UpdateUserID;
            entity.UpdateDate = model.UpdateDate;

            return entity;

        }

        public IEnumerable<RoleExtension> Model2Entity(IEnumerable<RoleModel> models)
        {
            List<RoleExtension> result = new List<RoleExtension>();

            foreach (var item in models)
            {
                result.Add(Model2Entity(item));
            }

            return result;
        }

        public override Expressionable<RoleExtension> FilterModel2Where(RoleFilterModel filterModel)
        {
            var expr = base.FilterModel2Where(filterModel);

            if (!string.IsNullOrEmpty(filterModel._SearchInput))
            {
                expr = expr.And(x => x.Name.Contains(filterModel._SearchInput));
            }

            if (!string.IsNullOrEmpty(filterModel._Name))
            {
                expr = expr.And(x => x.Name == filterModel._Name);
            }

            if (filterModel._IsAllData.HasValue)
            {
                expr = expr.And(x => x.IsAllData == filterModel._IsAllData.Value);
            }

            if (filterModel._RoleIDs != null && filterModel._RoleIDs.Count > 0)
            {
                expr = expr.And(x => filterModel._RoleIDs.Contains(x.RoleID));
            }

            return expr;
        }
    }
}
