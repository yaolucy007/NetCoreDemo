using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using ViewModels;
using ViewModels.Models;

namespace SystemService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : EditingBaseController<RoleModel, RoleFilterModel>
    {
        private readonly IRoleLogic _logic;

        public RoleController(IRoleLogic __IRoleLogic)
        {
            this._logic = __IRoleLogic;
        }

        public async override Task<ResultModel<ValueViewModel>> BatchDelete([FromBody] IEnumerable<int> autoIDs)
        {
            return await Task.Run(() => _logic.Delete(autoIDs.ToList()));
        }

        public async override Task<ResultModel<ValueViewModel>> BatchInsert([FromBody] IEnumerable<RoleModel> models)
        {
            throw new NotImplementedException();
        }

        public async override Task<ResultModel<ValueViewModel>> BatchUpdate([FromBody] IEnumerable<RoleModel> models)
        {
            throw new NotImplementedException();
        }

        public async override Task<ResultModel<ValueViewModel>> Delete(int id)
        {
            return await Task.Run(() => _logic.Delete(id));
        }

        public async override Task<ResultModel<IEnumerable<RoleModel>>> GetAll(string orderBy = null)
        {
            return await Task.Run(() => _logic.GetAll(orderBy));
        }

        public async override Task<ResultModel<RoleModel>> GetByID(int id)
        {
            return await Task.Run(() => _logic.GetByID(id));
        }

        public async override Task<ResultModel<ValueViewModel>> GetCount([FromBody] RoleFilterModel filterModel)
        {
            return await Task.Run(() => _logic.GetCount(filterModel));
        }

        public async override Task<ResultModel<RoleModel>> GetModel([FromBody] RoleFilterModel filterModel, string orderBy = null)
        {
            return await Task.Run(() => _logic.GetModel(filterModel, orderBy));
        }

        public async override Task<ResultModel<PageModel<RoleModel>>> GetPage([FromBody] RoleFilterModel filterModel, int pageIndex = 1, int pageSize = 20, string orderBy = null)
        {
            return await Task.Run(() => _logic.GetPage(filterModel, pageIndex, pageSize, orderBy));
        }

        public async override Task<ResultModel<IEnumerable<RoleModel>>> GetSearch([FromBody] RoleFilterModel filterModel, string orderBy = null, int? top = null)
        {
            return await Task.Run(() => _logic.GetSearch(filterModel, orderBy, top));
        }

        public async override Task<ResultModel<RoleModel>> Insert([FromBody] RoleModel model)
        {
            model.CreateDate = model.UpdateDate = DateTime.Now;

            return await Task.Run(() => _logic.Insert(model));
        }

        public async override Task<ResultModel<RoleModel>> Update([FromBody] RoleModel model)
        {
            model.UpdateDate = DateTime.Now;
            return await Task.Run(() => _logic.Update(model));
        }
    }
}
