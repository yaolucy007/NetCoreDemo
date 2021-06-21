using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using ViewModels;

namespace Services
{
    public interface IDepartmentLogic : ILogic<DepartmentModel, DepartmentFilterModel>
    {

    }

    public class DepartmentLogic : IDepartmentLogic
    {
        private readonly IDepartmentMapping _mapping;
        private readonly IDepartmentRepository _repository;

        public DepartmentLogic(IDepartmentMapping __IDepartmentMapping, IDepartmentRepository __IDepartmentRepository)
        {
            this._mapping = __IDepartmentMapping;
            this._repository = __IDepartmentRepository;
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

        public ResultModel<IEnumerable<DepartmentModel>> GetAll(string orderBy = null)
        {
            ResultModel<IEnumerable<DepartmentModel>> result = new ResultModel<IEnumerable<DepartmentModel>>();
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

        public ResultModel<DepartmentModel> GetByID(int id)
        {
            ResultModel<DepartmentModel> result = new ResultModel<DepartmentModel>();
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

        public ResultModel<ValueViewModel> GetCount(DepartmentFilterModel filterModel)
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

        public ResultModel<DepartmentModel> GetModel(DepartmentFilterModel filterModel, string orderBy = null)
        {
            ResultModel<DepartmentModel> result = new ResultModel<DepartmentModel>();
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

        public ResultModel<PageModel<DepartmentModel>> GetPage(DepartmentFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null)
        {
            ResultModel<PageModel<DepartmentModel>> result = new ResultModel<PageModel<DepartmentModel>>();
            try
            {
                PageModel<DepartmentModel> pageResult = new PageModel<DepartmentModel>();


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

        public ResultModel<IEnumerable<DepartmentModel>> GetSearch(DepartmentFilterModel filterModel, string orderBy = null, int? top = null)
        {
            ResultModel<IEnumerable<DepartmentModel>> result = new ResultModel<IEnumerable<DepartmentModel>>();
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

        public ResultModel<DepartmentModel> Insert(DepartmentModel model)
        {
            ResultModel<DepartmentModel> result = new ResultModel<DepartmentModel>();
            try
            {
                var flag = _repository.Insert(_mapping.Model2Entity(model));
                model.DepartmentID = flag;

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

        public ResultModel<DepartmentModel> Insert(List<DepartmentModel> models)
        {
            throw new NotImplementedException();
        }

        public ResultModel<DepartmentModel> Update(DepartmentModel model)
        {
            ResultModel<DepartmentModel> result = new ResultModel<DepartmentModel>();
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

        public ResultModel<DepartmentModel> Update(List<DepartmentModel> models)
        {
            throw new NotImplementedException();
        }

        #region 内部方法，原则上不允许这么玩儿
        public bool _Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void _Delete(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public List<DepartmentModel> _GetAll(string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public DepartmentModel _GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public int _GetCount(DepartmentFilterModel filterModel)
        {
            throw new NotImplementedException();
        }

        public DepartmentModel _GetModel(DepartmentFilterModel filterModel, string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public PageModel<DepartmentModel> _GetPage(DepartmentFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public List<DepartmentModel> _GetSearch(DepartmentFilterModel filterModel, string orderBy = null, int? top = null)
        {
            throw new NotImplementedException();
        }

        public int _Insert(DepartmentModel model)
        {
            throw new NotImplementedException();
        }

        public List<int> _Insert(List<DepartmentModel> models)
        {
            throw new NotImplementedException();
        }

        public bool _Update(DepartmentModel model)
        {
            throw new NotImplementedException();
        }

        public void _Update(List<DepartmentModel> models)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
