using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using ViewModels;

namespace Services
{
    public interface IPermissionLogic : ILogic<PermissionModel, PermissionFilterModel>
    {
        /// <summary>
        /// 按照树形结构获取权限数据情况
        /// </summary>
        /// <returns></returns>
        ResultModel<IEnumerable<PermissionModel>> GetPermissionTree();

    }

    public class PermissionLogic : IPermissionLogic
    {
        private readonly IPermissionMapping _mapping;
        private readonly IPermissionRepository _repository;

        #region 按照树形结构获取权限数据情况
        public ResultModel<IEnumerable<PermissionModel>> GetPermissionTree()
        {
            ResultModel<IEnumerable<PermissionModel>> result = new ResultModel<IEnumerable<PermissionModel>>();
            try
            {
                List<PermissionModel> items = new List<PermissionModel>();

                var list = _repository.GetAll().ToList().Select(x => _mapping.Entity2Model(x)).ToList();

                var firstLevelList = list.Where(x => x.ParentID == 0).ToList();
                foreach (var item in firstLevelList)
                {
                    PermissionModel model = GetPermissionModelWithChildren(item, list);

                    items.Add(model);
                }

                result.Code = 0;
                result.Msg = "success";
                result.Data = items;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        public PermissionLogic(IPermissionMapping __IPermissionMapping, IPermissionRepository __IPermissionRepository)
        {
            this._mapping = __IPermissionMapping;
            this._repository = __IPermissionRepository;
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

        public ResultModel<IEnumerable<PermissionModel>> GetAll(string orderBy = null)
        {
            ResultModel<IEnumerable<PermissionModel>> result = new ResultModel<IEnumerable<PermissionModel>>();
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

        public ResultModel<PermissionModel> GetByID(int id)
        {
            ResultModel<PermissionModel> result = new ResultModel<PermissionModel>();
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

        public ResultModel<ValueViewModel> GetCount(PermissionFilterModel filterModel)
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

        public ResultModel<PermissionModel> GetModel(PermissionFilterModel filterModel, string orderBy = null)
        {
            ResultModel<PermissionModel> result = new ResultModel<PermissionModel>();
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

        public ResultModel<PageModel<PermissionModel>> GetPage(PermissionFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null)
        {
            ResultModel<PageModel<PermissionModel>> result = new ResultModel<PageModel<PermissionModel>>();
            try
            {
                PageModel<PermissionModel> pageResult = new PageModel<PermissionModel>();


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

        public ResultModel<IEnumerable<PermissionModel>> GetSearch(PermissionFilterModel filterModel, string orderBy = null, int? top = null)
        {
            ResultModel<IEnumerable<PermissionModel>> result = new ResultModel<IEnumerable<PermissionModel>>();
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

        public ResultModel<PermissionModel> Insert(PermissionModel model)
        {
            ResultModel<PermissionModel> result = new ResultModel<PermissionModel>();
            try
            {
                var flag = _repository.Insert(_mapping.Model2Entity(model));
                model.PermissionID = flag;

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

        public ResultModel<PermissionModel> Insert(List<PermissionModel> models)
        {
            throw new NotImplementedException();
        }

        public ResultModel<PermissionModel> Update(PermissionModel model)
        {
            ResultModel<PermissionModel> result = new ResultModel<PermissionModel>();
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

        public ResultModel<PermissionModel> Update(List<PermissionModel> models)
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

        public List<PermissionModel> _GetAll(string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public PermissionModel _GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public int _GetCount(PermissionFilterModel filterModel)
        {
            throw new NotImplementedException();
        }

        public PermissionModel _GetModel(PermissionFilterModel filterModel, string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public PageModel<PermissionModel> _GetPage(PermissionFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public List<PermissionModel> _GetSearch(PermissionFilterModel filterModel, string orderBy = null, int? top = null)
        {
            throw new NotImplementedException();
        }

        public int _Insert(PermissionModel model)
        {
            throw new NotImplementedException();
        }

        public List<int> _Insert(List<PermissionModel> models)
        {
            throw new NotImplementedException();
        }

        public bool _Update(PermissionModel model)
        {
            throw new NotImplementedException();
        }

        public void _Update(List<PermissionModel> models)
        {
            throw new NotImplementedException();
        }
        #endregion

        private PermissionModel GetPermissionModelWithChildren(PermissionModel curModel, List<PermissionModel> allList)
        {
            if (curModel != null)
            {
                var children = allList.Where(x => x.ParentID == x.PerID).ToList();
                foreach (var item in children)
                {
                    PermissionModel model = GetPermissionModelWithChildren(item, allList);
                    curModel.Children_List.Add(model);
                }
            }

            return curModel;
        }
    }
}
