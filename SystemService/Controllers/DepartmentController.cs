using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using ViewModels;

namespace SystemService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : EditingBaseController<DepartmentModel, DepartmentFilterModel>
    {
        private IDepartmentLogic _logic;

        public DepartmentController(IDepartmentLogic __IDepartmentLogic)
        {
            this._logic = __IDepartmentLogic;
        }

        public async override Task<ResultModel<ValueViewModel>> BatchDelete([FromBody] IEnumerable<int> autoIDs)
        {
            return await Task.Run(() => _logic.Delete(autoIDs.ToList()));
        }

        public async override Task<ResultModel<ValueViewModel>> BatchInsert([FromBody] IEnumerable<DepartmentModel> models)
        {
            throw new NotImplementedException();
        }

        public async override Task<ResultModel<ValueViewModel>> BatchUpdate([FromBody] IEnumerable<DepartmentModel> models)
        {
            throw new NotImplementedException();
        }

        public async override Task<ResultModel<ValueViewModel>> Delete(int id)
        {
            return await Task.Run(() => _logic.Delete(id));
        }

        public async override Task<ResultModel<IEnumerable<DepartmentModel>>> GetAll(string orderBy = null)
        {
            return await Task.Run(() => _logic.GetAll(orderBy));
        }

        public async override Task<ResultModel<DepartmentModel>> GetByID(int id)
        {
            return await Task.Run(() => _logic.GetByID(id));
        }

        public async override Task<ResultModel<ValueViewModel>> GetCount([FromBody] DepartmentFilterModel filterModel)
        {
            return await Task.Run(() => _logic.GetCount(filterModel));
        }

        public async override Task<ResultModel<DepartmentModel>> GetModel([FromBody] DepartmentFilterModel filterModel, string orderBy = null)
        {
            return await Task.Run(() => _logic.GetModel(filterModel, orderBy));
        }

        public async override Task<ResultModel<PageModel<DepartmentModel>>> GetPage([FromBody] DepartmentFilterModel filterModel, int pageIndex = 1, int pageSize = 20, string orderBy = null)
        {
            return await Task.Run(() => _logic.GetPage(filterModel, pageIndex, pageSize, orderBy));
        }

        public async override Task<ResultModel<IEnumerable<DepartmentModel>>> GetSearch([FromBody] DepartmentFilterModel filterModel, string orderBy = null, int? top = null)
        {
            return await Task.Run(() => _logic.GetSearch(filterModel, orderBy, top));
        }

        public async override Task<ResultModel<DepartmentModel>> Insert([FromBody] DepartmentModel model)
        {
            return await Task.Run(() => _logic.Insert(model));
        }

        public async override Task<ResultModel<DepartmentModel>> Update([FromBody] DepartmentModel model)
        {
            return await Task.Run(() => _logic.Update(model));
        }
    }
}
