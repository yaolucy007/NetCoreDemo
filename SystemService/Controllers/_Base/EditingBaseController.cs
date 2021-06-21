
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using ViewModels;

namespace SystemService.Controllers
{
    public abstract class EditingBaseController<TModel, TFilterModel> : AuthorizeBaseController
        where TModel : BaseViewModel
        where TFilterModel : BaseFilterModel
    {
        #region 添加  Insert  Post
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">数据对象</param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public abstract Task<ResultModel<TModel>> Insert([FromBody] TModel model);
        #endregion

        #region 批量添加  BatchInsert  Post
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="models">数据对象集合</param>
        /// <returns></returns>
        [HttpPost("BatchInsert")]
        public abstract Task<ResultModel<ValueViewModel>> BatchInsert([FromBody] IEnumerable<TModel> models);

        #endregion



        #region 修改  Update  Put
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">数据对象</param>
        /// <returns></returns>
        [HttpPut("Update")]
        public abstract Task<ResultModel<TModel>> Update([FromBody] TModel model);
        #endregion

        #region 批量修改  BatchUpdate  Put
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="models">数据对象集合</param>
        /// <returns></returns>
        [HttpPut("BatchUpdate")]
        public abstract Task<ResultModel<ValueViewModel>> BatchUpdate([FromBody] IEnumerable<TModel> models);
        #endregion



        #region 物理删除  Delete  Delete
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public abstract Task<ResultModel<ValueViewModel>> Delete(int id);
        #endregion

        #region 批量物理删除  BatchDelete  Delete
        /// <summary>
        /// 批量物理删除
        /// </summary>
        /// <param name="autoIDs">主键ID集合</param>
        /// <returns></returns>
        [HttpDelete("BatchDelete")]
        public abstract Task<ResultModel<ValueViewModel>> BatchDelete([FromBody] IEnumerable<int> autoIDs);
        #endregion



        #region 根据ID得到对象  GetByID  Get
        /// <summary>
        /// 根据ID得到对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        [HttpGet("GetByID/{id}")]
        public abstract Task<ResultModel<TModel>> GetByID(int id);
        #endregion

        #region 自定义查询对象  GetModel  Post
        /// <summary>
        /// 自定义查询对象
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [HttpPost("GetModel")]
        public abstract Task<ResultModel<TModel>> GetModel([FromBody] TFilterModel filterModel, string orderBy = null);
        #endregion

        #region 得到所有数据  GetAll  Get
        /// <summary>
        /// 得到所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public abstract Task<ResultModel<IEnumerable<TModel>>> GetAll(string orderBy = null);
        #endregion

        #region 自定义搜索  GetSearch  Post
        /// <summary>
        /// 自定义搜索
        /// </summary>
        /// <param name="filterModel">搜索条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="top">取前N条数据</param>
        /// <returns></returns>
        [HttpPost("GetSearch")]
        public abstract Task<ResultModel<IEnumerable<TModel>>> GetSearch([FromBody] TFilterModel filterModel, string orderBy = null, int? top = null);
        #endregion

        #region 分页查询  GetPage  Post
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterModel">搜索条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns></returns>
        [HttpPost("GetPage")]
        public abstract Task<ResultModel<PageModel<TModel>>> GetPage([FromBody] TFilterModel filterModel, int pageIndex = 1, int pageSize = Gx._PAGE_SIZE, string orderBy = null);
        #endregion

        #region 自定义条件返回数量  GetCount  Post
        /// <summary>
        /// 自定义条件返回数量
        /// </summary>
        /// <param name="filterModel">搜索条件</param>
        /// <returns></returns>
        [HttpPost("GetCount")]
        public abstract Task<ResultModel<ValueViewModel>> GetCount([FromBody] TFilterModel filterModel);
        #endregion
    }
}
