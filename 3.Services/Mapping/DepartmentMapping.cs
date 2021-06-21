using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace Services
{
    public interface IDepartmentMapping : IMapping<DepartmentExtension, DepartmentModel, DepartmentFilterModel>
    {

    }

    public class DepartmentMapping : BaseMapping<DepartmentExtension, DepartmentModel, DepartmentFilterModel>, IDepartmentMapping
    {
        public DepartmentModel Entity2Model(DepartmentExtension entity)
        {
            DepartmentModel model = new DepartmentModel();

            model.DepartmentID = entity.DepartmentID;
            model.LeaderID = entity.LeaderID;
            model.LeaderName = entity.LeaderName;
            model.Name = entity.Name;
            model.ParentID = entity.ParentID;
            model.IsDelete = entity.IsDelete;
            model.CreateUserID = entity.CreateUserID;
            model.CreateDate = entity.CreateDate;
            model.UpdateUserID = entity.UpdateUserID;
            model.UpdateDate = entity.UpdateDate;
            model.CreateUserName = entity.CreateUserName;
            model.UpdateUserName = entity.UpdateUserName;
            return model;
        }

        public IEnumerable<DepartmentModel> Entity2Model(IEnumerable<DepartmentExtension> entities)
        {
            List<DepartmentModel> result = new List<DepartmentModel>();

            foreach (var item in entities)
            {
                result.Add(Entity2Model(item));
            }

            return result;

        }

        public DepartmentExtension Model2Entity(DepartmentModel model)
        {
            DepartmentExtension entity = new DepartmentExtension();

            entity.DepartmentID = model.DepartmentID;
            entity.LeaderID = model.LeaderID;
            entity.LeaderName = model.LeaderName;
            entity.Name = model.Name;
            entity.ParentID = model.ParentID;
            entity.IsDelete = model.IsDelete;
            entity.CreateUserID = model.CreateUserID;
            entity.CreateDate = model.CreateDate;
            entity.UpdateUserID = model.UpdateUserID;
            entity.UpdateDate = model.UpdateDate;

            return entity;
        }

        public IEnumerable<DepartmentExtension> Model2Entity(IEnumerable<DepartmentModel> models)
        {
            List<DepartmentExtension> result = new List<DepartmentExtension>();

            foreach (var item in models)
            {
                result.Add(Model2Entity(item));
            }

            return result;
        }

        public override Expressionable<DepartmentExtension> FilterModel2Where(DepartmentFilterModel filterModel)
        {
            var expr = base.FilterModel2Where(filterModel);

            if (string.IsNullOrEmpty(filterModel._SearchInput))
            {
                expr = expr.And(x => x.Name.Contains(filterModel._SearchInput) || x.LeaderName.Contains(filterModel._SearchInput));
            }

            if (filterModel._LeaderID.HasValue)
            {
                expr = expr.And(x => x.LeaderID == filterModel._LeaderID.Value);
            }
            if (filterModel._ParentID.HasValue)
            {
                expr = expr.And(x => x.ParentID == filterModel._ParentID.Value);
            }
            if (string.IsNullOrEmpty(filterModel._Name))
            {
                expr = expr.And(x => x.Name == filterModel._Name);
            }
            if (filterModel._DepartmentIDs != null && filterModel._DepartmentIDs.Count > 0)
            {
                expr = expr.And(x => filterModel._DepartmentIDs.Contains(x.DepartmentID));
            }

            return expr;
        }
    }
}
