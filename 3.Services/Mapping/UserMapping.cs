using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace Services
{
    public interface IUserMapping : IMapping<UserExtension, UserModel, UserFilterModel>
    {

    }
    public class UserMapping : BaseMapping<UserExtension, UserModel, UserFilterModel>, IUserMapping
    {
        public UserModel Entity2Model(UserExtension entity)
        {
            UserModel model = new UserModel();

            model.UserID = entity.UserID;
            model.LoginName = entity.LoginName;
            model.JobCode = entity.JobCode;
            model.Name = entity.Name;
            model.Email = entity.Email;
            model.Phone = entity.Phone;
            model.Avatar_Img = entity.Avatar_Img;
            model.Description = entity.Description;
            model.Remark = entity.Remark;
            model.IsDelete = entity.IsDelete;
            model.CreateDate = model.CreateDate;
            model.UpdateDate = model.UpdateDate;

            return model;
        }

        public IEnumerable<UserModel> Entity2Model(IEnumerable<UserExtension> entities)
        {
            List<UserModel> list = new List<UserModel>();
            foreach (var item in entities)
            {
                list.Add(Entity2Model(item));
            }
            return list;
        }

        public UserExtension Model2Entity(UserModel model)
        {
            UserExtension entity = new UserExtension();
            entity.UserID = model.UserID;
            entity.LoginName = model.LoginName;
            entity.LoginPassword = "123456";
            entity.UserID = model.UserID;
            entity.JobCode = model.JobCode;
            entity.Name = model.Name;
            entity.Email = model.Email;
            entity.Phone = model.Phone;
            entity.Avatar_Img = model.Avatar_Img;
            entity.Description = model.Description;
            entity.Remark = model.Remark;
            entity.IsDelete = model.IsDelete;
            entity.CreateDate = model.CreateDate;
            entity.UpdateDate = model.UpdateDate;

            return entity;
        }

        public IEnumerable<UserExtension> Model2Entity(IEnumerable<UserModel> models)
        {
            List<UserExtension> list = new List<UserExtension>();
            foreach (var item in models)
            {
                list.Add(Model2Entity(item));
            }
            return list;
        }

        public override Expressionable<UserExtension> FilterModel2Where(UserFilterModel filterModel)
        {
            var expr = base.FilterModel2Where(filterModel);

            if (!string.IsNullOrEmpty(filterModel._SearchInput))
            {
                expr = expr.And(x => x.LoginName.Contains(filterModel._SearchInput) || x.Name.Contains(filterModel._SearchInput));
            }

            if (!string.IsNullOrEmpty(filterModel._Name))
            {
                expr = expr.And(x => x.Name == filterModel._Name);
            }

            if (!string.IsNullOrEmpty(filterModel._LoginName))
            {
                expr = expr.And(x => x.LoginName == filterModel._LoginName);
            }

            if (!string.IsNullOrEmpty(filterModel._JobCode))
            {
                expr = expr.And(x => x.JobCode == filterModel._JobCode);
            }

            if (!string.IsNullOrEmpty(filterModel._Phone))
            {
                expr = expr.And(x => x.Phone == filterModel._Phone);
            }


            return expr;
        }
    }
}
