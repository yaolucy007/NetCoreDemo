using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using ViewModels;

namespace Services
{
    public interface IRoleLogic : ILogic<RoleModel, RoleFilterModel>
    {

    }

    public class RoleLogic : IRoleLogic
    {
        private readonly IRoleMapping _mapping;
        private readonly IRoleRepository _repository;

        public RoleLogic(RoleMapping __RoleMapping, IRoleRepository __IRoleRepository)
        {
            this._mapping = __RoleMapping;
            this._repository = __IRoleRepository;
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

        public ResultModel<IEnumerable<RoleModel>> GetAll(string orderBy = null)
        {
            ResultModel<IEnumerable<RoleModel>> result = new ResultModel<IEnumerable<RoleModel>>();
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

        public ResultModel<RoleModel> GetByID(int id)
        {
            ResultModel<RoleModel> result = new ResultModel<RoleModel>();
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

        public ResultModel<ValueViewModel> GetCount(RoleFilterModel filterModel)
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

        public ResultModel<RoleModel> GetModel(RoleFilterModel filterModel, string orderBy = null)
        {
            ResultModel<RoleModel> result = new ResultModel<RoleModel>();
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

        public ResultModel<PageModel<RoleModel>> GetPage(RoleFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null)
        {
            ResultModel<PageModel<RoleModel>> result = new ResultModel<PageModel<RoleModel>>();
            try
            {
                PageModel<RoleModel> pageResult = new PageModel<RoleModel>();


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

        public ResultModel<IEnumerable<RoleModel>> GetSearch(RoleFilterModel filterModel, string orderBy = null, int? top = null)
        {
            ResultModel<IEnumerable<RoleModel>> result = new ResultModel<IEnumerable<RoleModel>>();
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

        public ResultModel<RoleModel> Insert(RoleModel model)
        {
            ResultModel<RoleModel> result = new ResultModel<RoleModel>();
            try
            {
                var flag = _repository.Insert(_mapping.Model2Entity(model));
                model.RoleID = flag;

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

        public ResultModel<RoleModel> Insert(List<RoleModel> models)
        {
            throw new NotImplementedException();
        }

        public ResultModel<RoleModel> Update(RoleModel model)
        {
            ResultModel<RoleModel> result = new ResultModel<RoleModel>();
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

        public ResultModel<RoleModel> Update(List<RoleModel> models)
        {
            throw new NotImplementedException();
        }

        #region 内置方法，原则上不允许这么玩儿
        public bool _Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void _Delete(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public List<RoleModel> _GetAll(string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public RoleModel _GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public int _GetCount(RoleFilterModel filterModel)
        {
            throw new NotImplementedException();
        }

        public RoleModel _GetModel(RoleFilterModel filterModel, string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public PageModel<RoleModel> _GetPage(RoleFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public List<RoleModel> _GetSearch(RoleFilterModel filterModel, string orderBy = null, int? top = null)
        {
            throw new NotImplementedException();
        }

        public int _Insert(RoleModel model)
        {
            throw new NotImplementedException();
        }

        public List<int> _Insert(List<RoleModel> models)
        {
            throw new NotImplementedException();
        }

        public bool _Update(RoleModel model)
        {
            throw new NotImplementedException();
        }

        public void _Update(List<RoleModel> models)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
