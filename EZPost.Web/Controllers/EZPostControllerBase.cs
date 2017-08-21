using System;
using System.Web.Mvc;
using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using EZPost.Common;
using Microsoft.AspNet.Identity;
using EZPost.Common.WebControl;
using System.Collections.Generic;
using System.Linq;
using EZPost.Dto;
using Abp.Extensions;
using Abp.Timing;

namespace EZPost.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class EZPostControllerBase : AbpController
    {


        protected EZPostControllerBase()
        {
            LocalizationSourceName = EZPostConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual JsonResult Success(string message)
        {
            return new JsonResult
            {
                Data = new AjaxResult { type = ResultType.success, message = message }
            };
            //return Content(new AjaxResult { type = ResultType.success, message = message }.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { type = ResultType.success, message = message, resultdata = data }.ToJson());
        }
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual JsonResult Error(string message)
        {
            return new JsonResult
            {
                Data = new AjaxResult { type = ResultType.error, message = message }
            };
        }


        /// <summary>
        ///   返回datatTable数据
        /// </summary>
        /// <param name="draw">请求次数计数器</param>
        /// <param name="recordsTotal">总共记录数</param>
        /// <param name="recordsFiltered">过滤后的记录数</param>
        /// <param name="data">数据</param>
        /// <param name="error">服务器错误信息</param>
        public JsonResult DataTablesJson<TEntity>(int draw, int recordsTotal, int recordsFiltered,
            IReadOnlyList<TEntity> data, string error = null)
        {
            var result = new DataTableModel<TEntity>(draw, recordsTotal, recordsFiltered, data);
            var jsonResult = new JsonResult
            {
                Data = result
            };
            return jsonResult;
        }


        /// <summary>
        /// 返回datatTable数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pagelList"></param>
        /// <param name="draw">请求次数计数器</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public JsonResult DataJsonResult<TEntity>(IPagedList<TEntity> pagelList, int draw, string error = null)
        {
            var result = new DataTableModel<TEntity>(draw, pagelList.BeforeFilterTotalCount, pagelList.TotalCount, pagelList.ToList());
            return new JsonResult()
            {
                Data = result
            };
        }

        /// <summary>
        /// 当前登录者的代理和客户id
        /// 如果id都为空，则说明是管理者
        ///  customerid不为空，则说明是客户， agentId不为空则说明是代理
        /// </summary>
        /// <returns></returns>
        public int GetCurrentAgentId()
        {
            return AbpSession.GetAgentId() == null ? 0 : Convert.ToInt32(AbpSession.GetAgentId());
        }

        /// <summary>
        /// 当前登录者的代理和客户id
        /// 如果id都为空，则说明是管理者
        ///  customerid不为空，则说明是客户， agentId不为空则说明是代理
        /// </summary>
        /// <returns></returns>
        public int GetCurrentCustomerId()
        {
            return AbpSession.GetCustomerId() == null ? 0 : Convert.ToInt32(AbpSession.GetCustomerId());
        }
    }
}