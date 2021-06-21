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
    public class PermissionController : EditingBaseController<PermissionModel, PermissionFilterModel>
    {
        private readonly IPermissionLogic _logic;

        public PermissionController(IPermissionLogic __IPermissionLogic)
        {
            this._logic = __IPermissionLogic;
        }

        public async override Task<ResultModel<ValueViewModel>> BatchDelete([FromBody] IEnumerable<int> autoIDs)
        {
            return await Task.Run(() => _logic.Delete(autoIDs.ToList()));
        }

        public async override Task<ResultModel<ValueViewModel>> BatchInsert([FromBody] IEnumerable<PermissionModel> models)
        {
            throw new NotImplementedException();
        }

        public async override Task<ResultModel<ValueViewModel>> BatchUpdate([FromBody] IEnumerable<PermissionModel> models)
        {
            throw new NotImplementedException();
        }

        public async override Task<ResultModel<ValueViewModel>> Delete(int id)
        {
            return await Task.Run(() => _logic.Delete(id));
        }

        public async override Task<ResultModel<IEnumerable<PermissionModel>>> GetAll(string orderBy = null)
        {
            return await Task.Run(() => _logic.GetAll(orderBy));
        }

        public async override Task<ResultModel<PermissionModel>> GetByID(int id)
        {
            return await Task.Run(() => _logic.GetByID(id));
        }

        public async override Task<ResultModel<ValueViewModel>> GetCount([FromBody] PermissionFilterModel filterModel)
        {
            return await Task.Run(() => _logic.GetCount(filterModel));
        }

        public async override Task<ResultModel<PermissionModel>> GetModel([FromBody] PermissionFilterModel filterModel, string orderBy = null)
        {
            return await Task.Run(() => _logic.GetModel(filterModel, orderBy));
        }

        public async override Task<ResultModel<PageModel<PermissionModel>>> GetPage([FromBody] PermissionFilterModel filterModel, int pageIndex = 1, int pageSize = 20, string orderBy = null)
        {
            return await Task.Run(() => _logic.GetPage(filterModel, pageIndex, pageSize, orderBy));
        }

        public async override Task<ResultModel<IEnumerable<PermissionModel>>> GetSearch([FromBody] PermissionFilterModel filterModel, string orderBy = null, int? top = null)
        {
            return await Task.Run(() => _logic.GetSearch(filterModel, orderBy, top));
        }

        public async override Task<ResultModel<PermissionModel>> Insert([FromBody] PermissionModel model)
        {
            return await Task.Run(() => _logic.Insert(model));
        }

        public async override Task<ResultModel<PermissionModel>> Update([FromBody] PermissionModel model)
        {
            return await Task.Run(() => _logic.Update(model));
        }

        /// <summary>
        /// 按照树形结构获取全部的权限数据集合
        /// </summary>
        /// <returns></returns>
        [HttpGet("tree")]
        public async Task<ResultModel<IEnumerable<PermissionModel>>> GetTreeData()
        {
            return await Task.Run(() => _logic.GetPermissionTree());
        }
    }
}
