using System;
using System.Collections.Generic;
using System.Text;
using Utilities;
using ViewModels;

namespace Services
{
    public interface ILogic<TModel, TFilterModel>
        where TModel : BaseViewModel
        where TFilterModel : BaseFilterModel
    {
        //添加  Insert
        int _Insert(TModel model);
        ResultModel<TModel> Insert(TModel model);

        //批量添加  Insert
        List<int> _Insert(List<TModel> models);
        ResultModel<TModel> Insert(List<TModel> models);



        //修改  Update
        bool _Update(TModel model);
        ResultModel<TModel> Update(TModel model);

        //批量修改  Update
        void _Update(List<TModel> models);
        ResultModel<TModel> Update(List<TModel> models);



        //删除  Delete
        bool _Delete(int id);
        ResultModel<ValueViewModel> Delete(int id);

        //批量删除  Delete
        void _Delete(List<int> ids);
        ResultModel<ValueViewModel> Delete(List<int> ids);



        //根据ID得到对象  GetByID
        TModel _GetByID(int id);
        ResultModel<TModel> GetByID(int id);



        //自定义查询对象 GetModel
        TModel _GetModel(TFilterModel filterModel, string orderBy = null);
        ResultModel<TModel> GetModel(TFilterModel filterModel, string orderBy = null);



        //查询全部  GetAll
        List<TModel> _GetAll(string orderBy = null);
        ResultModel<IEnumerable<TModel>> GetAll(string orderBy = null);



        //自定义查询集合  GetSearch
        List<TModel> _GetSearch(TFilterModel filterModel, string orderBy = null, int? top = default(int?));
        ResultModel<IEnumerable<TModel>> GetSearch(TFilterModel filterModel, string orderBy = null, int? top = default(int?));



        //分页查询  GetPage
        PageModel<TModel> _GetPage(TFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null);
        ResultModel<PageModel<TModel>> GetPage(TFilterModel filterModel, int pageIndex, int pageSize, string orderBy = null);



        //自定义查询返回数量 GetCount
        int _GetCount(TFilterModel filterModel);
        ResultModel<ValueViewModel> GetCount(TFilterModel filterModel);
    }
}
