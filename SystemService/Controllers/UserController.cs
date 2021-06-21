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
    public class UserController : EditingBaseController<UserModel, UserFilterModel>
    {
        private readonly IUserLogic _logic;

        public UserController(IUserLogic __IUserLogic)
        {
            this._logic = __IUserLogic;
        }

        public override async Task<ResultModel<ValueViewModel>> BatchDelete([FromBody] IEnumerable<int> autoIDs)
        {
            return await Task.Run(() => _logic.Delete(autoIDs.ToList()));
        }

        public override async Task<ResultModel<ValueViewModel>> BatchInsert([FromBody] IEnumerable<UserModel> models)
        {
            //return await Task.Run(() => _logic.Insert(models.ToList()));
            throw new NotImplementedException();
        }

        public override async Task<ResultModel<ValueViewModel>> BatchUpdate([FromBody] IEnumerable<UserModel> models)
        {
            throw new NotImplementedException();
        }

        public override async Task<ResultModel<ValueViewModel>> Delete(int id)
        {
            return await Task.Run(() => _logic.Delete(id));
        }

        public override async Task<ResultModel<IEnumerable<UserModel>>> GetAll(string orderBy = null)
        {
            return await Task.Run(() => _logic.GetAll(orderBy));
        }

        public override async Task<ResultModel<UserModel>> GetByID(int id)
        {
            return await Task.Run(() => _logic.GetByID(id));
        }

        public override async Task<ResultModel<ValueViewModel>> GetCount([FromBody] UserFilterModel filterModel)
        {
            return await Task.Run(() => _logic.GetCount(filterModel));
        }

        public override async Task<ResultModel<UserModel>> GetModel([FromBody] UserFilterModel filterModel, string orderBy = null)
        {
            return await Task.Run(() => _logic.GetModel(filterModel, orderBy));
        }

        public override async Task<ResultModel<PageModel<UserModel>>> GetPage([FromBody] UserFilterModel filterModel, int pageIndex = 1, int pageSize = 20, string orderBy = null)
        {
            return await Task.Run(() => _logic.GetPage(filterModel, pageIndex, pageSize, orderBy));
        }

        public override async Task<ResultModel<IEnumerable<UserModel>>> GetSearch([FromBody] UserFilterModel filterModel, string orderBy = null, int? top = null)
        {
            return await Task.Run(() => _logic.GetSearch(filterModel, orderBy, top));
        }

        public override async Task<ResultModel<UserModel>> Insert([FromBody] UserModel model)
        {
            model.CreateDate = model.UpdateDate = DateTime.Now;

            return await Task.Run(() => _logic.Insert(model));
        }

        public override async Task<ResultModel<UserModel>> Update([FromBody] UserModel model)
        {
            model.UpdateDate = DateTime.Now;
            return await Task.Run(() => _logic.Update(model));
        }

        /// <summary>
        /// 获取当前登录的个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("me")]
        public async Task<ResultModel<UserModel>> GetMe()
        {
            return null;
        }

        public async Task<ResultModel<IEnumerable<int>>> GetMePermissionIDs()
        {
            return null;
        }
    }
}
