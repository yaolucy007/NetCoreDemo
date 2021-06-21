using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using ViewModels;

namespace Services
{
    public interface IUserLogic : ILogic<UserModel, UserFilterModel>
    {
        ResultModel<List<int>> GetPermissionIds(List<int> roleIDs);

        ResultModel<Me> GetMe(int userID, List<int> roleIDs, List<int> departmentIDs);
    }

    public class UserLogic : IUserLogic
    {
        private readonly IUserMapping _mapping;
        private readonly IUserRepository _repository;

        private readonly IRoleMapping _IRoleMapping;
        private readonly IRoleRepository _IRoleRepository;

        private readonly IDepartmentMapping _IDepartmentMapping;
        private readonly IDepartmentRepository _IDepartmentRepository;

        public UserLogic(IUserMapping __IUserMapping, IUserRepository __IUserRepository,
            IRoleMapping __IRoleMapping, IRoleRepository __IRoleRepository,
            IDepartmentMapping __IDepartmentMapping, IDepartmentRepository __IDepartmentRepository)
        {
            this._mapping = __IUserMapping;
            this._repository = __IUserRepository;

            this._IRoleMapping = __IRoleMapping;
            this._IRoleRepository = __IRoleRepository;

            this._IDepartmentMapping = __IDepartmentMapping;
            this._IDepartmentRepository = __IDepartmentRepository;
        }

        public ResultModel<Me> GetMe(int userID, List<int> roleIDs, List<int> departmentIDs)
        {
            ResultModel<Me> result = new ResultModel<Me>();
            try
            {
                Me me = (Me)_mapping.Entity2Model(_repository.GetModelByPrimaryKey(userID));

                me.Role_List = _IRoleRepository.GetSearch(_IRoleMapping.FilterModel2Where(new RoleFilterModel { _RoleIDs = roleIDs }))
                    .Select(x => _IRoleMapping.Entity2Model(x)).ToList();
                me.Department_List = _IDepartmentRepository.GetSearch(_IDepartmentMapping.FilterModel2Where(new DepartmentFilterModel() { _DepartmentIDs = departmentIDs }))
                    .Select(x => _IDepartmentMapping.Entity2Model(x)).ToList();

                result.Code = 0;
                result.Data = me;
                result.Msg = "success";
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<List<int>> GetPermissionIds(List<int> roleIDs)
        {
            ResultModel<List<int>> result = new ResultModel<List<int>>();

            try
            {
                result.Code = 0;
                result.Data = _repository.GetMePermissionIDs(roleIDs);
                result.Msg = "success";
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<ValueViewModel> Delete(int id)
        {
            ResultModel<ValueViewModel> result = new ResultModel<ValueViewModel>();
            try
            {
                ValueViewModel model = new ValueViewModel();
                int flag = _repository.Delete(id);
                if (flag > 0)
                {
                    model.ServiceIdentity = true;
                    model.Data = flag;
                }
                else
                {
                    model.ServiceIdentity = false;
                }

                result.Code = 0;
                result.Data = model;
                result.Msg = "success";
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }
            return result;
        }

        public ResultModel<ValueViewModel> Delete(List<int> ids)
        {
            ResultModel<ValueViewModel> result = new ResultModel<ValueViewModel>();
            try
            {
                ValueViewModel model = new ValueViewModel();
                _repository.BatchDelete(ids);

                model.ServiceIdentity = true;
                model.Data = 0;

                result.Code = 0;
                result.Data = model;
                result.Msg = "success";
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }
            return result;
        }

        public ResultModel<IEnumerable<UserModel>> GetAll(string orderBy = null)
        {
            ResultModel<IEnumerable<UserModel>> result = new ResultModel<IEnumerable<UserModel>>();
            try
            {
                var list = _repository.GetAll().ToList().Select(x => _mapping.Entity2Model(x));

                result.Code = 0;
                result.Msg = "success";
                result.Data = list;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<UserModel> GetByID(int id)
        {
            ResultModel<UserModel> result = new ResultModel<UserModel>();
            try
            {
                var model = _mapping.Entity2Model(_repository.GetModelByPrimaryKey(id));

                result.Code = 0;
                result.Msg = "success";
                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<ValueViewModel> GetCount(UserFilterModel filterModel)
        {
            ResultModel<ValueViewModel> result = new ResultModel<ValueViewModel>();
            try
            {
                ValueViewModel model = new ValueViewModel();
                int iCount = _repository.GetCount(_mapping.FilterModel2Where(filterModel));

                model.ServiceIdentity = true;
                model.Data = iCount;

                result.Code = 0;
                result.Data = model;
                result.Msg = "success";
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }
            return result;
        }

        public ResultModel<UserModel> GetModel(UserFilterModel filterModel, string orderBy = null)
        {
            ResultModel<UserModel> result = new ResultModel<UserModel>();
            try
            {
                var model = _mapping.Entity2Model(_repository.GetModel(_mapping.FilterModel2Where(filterModel), orderBy));

                result.Code = 0;
                result.Msg = "success";
                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<PageModel<UserModel>> GetPage(UserFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null)
        {
            ResultModel<PageModel<UserModel>> result = new ResultModel<PageModel<UserModel>>();
            try
            {
                PageModel<UserModel> pageResult = new PageModel<UserModel>();


                var expr = _mapping.FilterModel2Where(filterModel);
                pageResult.TotalCount = _repository.GetCount(expr);

                var list = _repository.GetPage(_mapping.FilterModel2Where(filterModel), pageIndex, pageSize, orderBy).ToList().Select(x => _mapping.Entity2Model(x));
                pageResult.Items = list;

                result.Code = 0;
                result.Msg = "success";
                result.Data = pageResult;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<IEnumerable<UserModel>> GetSearch(UserFilterModel filterModel, string orderBy = null, int? top = null)
        {
            ResultModel<IEnumerable<UserModel>> result = new ResultModel<IEnumerable<UserModel>>();
            try
            {
                var list = _repository.GetSearch(_mapping.FilterModel2Where(filterModel), orderBy).ToList().Select(x => _mapping.Entity2Model(x));

                result.Code = 0;
                result.Msg = "success";
                result.Data = list;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<UserModel> Insert(UserModel model)
        {
            ResultModel<UserModel> result = new ResultModel<UserModel>();
            try
            {
                var flag = _repository.Insert(_mapping.Model2Entity(model));
                model.UserID = flag;

                result.Code = 0;
                result.Msg = "success";
                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<UserModel> Insert(List<UserModel> models)
        {
            throw new NotImplementedException();
        }

        public ResultModel<UserModel> Update(UserModel model)
        {
            ResultModel<UserModel> result = new ResultModel<UserModel>();
            try
            {
                var flag = _repository.Update(_mapping.Model2Entity(model));

                result.Code = 0;
                result.Msg = "success";
                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }

        public ResultModel<UserModel> Update(List<UserModel> models)
        {
            throw new NotImplementedException();
        }

        #region 内置方法，原则上是不能这么玩儿的
        public bool _Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void _Delete(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public List<UserModel> _GetAll(string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public UserModel _GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public int _GetCount(UserFilterModel filterModel)
        {
            throw new NotImplementedException();
        }

        public UserModel _GetModel(UserFilterModel filterModel, string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public PageModel<UserModel> _GetPage(UserFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public List<UserModel> _GetSearch(UserFilterModel filterModel, string orderBy = null, int? top = null)
        {
            throw new NotImplementedException();
        }

        public int _Insert(UserModel model)
        {
            throw new NotImplementedException();
        }

        public List<int> _Insert(List<UserModel> models)
        {
            throw new NotImplementedException();
        }

        public bool _Update(UserModel model)
        {
            throw new NotImplementedException();
        }

        public void _Update(List<UserModel> models)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
