using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.Web.Mvc.Authorization;
using EZPost.AuditingLog;
using EZPost.AuditingLog.Dto;
using EZPost.Authorization;

namespace EZPost.Web.Controllers
{
    [DisableAuditing]
    [AbpMvcAuthorize(PermissionNames.Pages_Setting_AuditLogs)]
    public class AudtingLogController : EZPostControllerBase
    {

        private readonly IAuditLogAppService _auditLogAppService;
        public AudtingLogController(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }

        // GET: AudtingLog
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Gets(GetAuditLogsInput input)
        {
            var data = _auditLogAppService.Gets(input);
            return DataJsonResult(data, input.Draw);
        }
    }
}