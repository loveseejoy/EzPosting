using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using EZPost.Authorization;
using EZPost.NotificationAppService;
using EZPost.NotificationAppService.Dto;

namespace EZPost.Web.Controllers
{
    [AbpMvcAuthorize]
    public class NotificationsController : EZPostControllerBase
    {
        private INotificationAppService _notificationAppService;

        public NotificationsController(INotificationAppService notificationAppService)
        {
            _notificationAppService = notificationAppService;
        }

        // GET: Notifications
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetNotifications(GetUserNotificationsInput input)
        {
            var data = _notificationAppService.GetUserNotifications(input);
            return DataJsonResult(data, input.Draw);
        }

        [AbpMvcAuthorize(PermissionNames.Pages_Notification_SendMessage)]
        public ActionResult SendMessage()
        {
            return View();
        }
    }
}