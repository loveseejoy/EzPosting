using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace EZPost.Web.Controllers
{
    [AbpMvcAuthorize]
    public class FileController : EZPostControllerBase
    {
        private readonly IAppFolders _appFolders;
        public FileController(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        // GET: File
        public ActionResult UpLoadImage(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return  Error("文件为空");
            }
            file.SaveAs(_appFolders.Images);
            var filepath = Path.Combine(_appFolders.Images, file.FileName);
            return Success("上传成功",filepath);
        }
    }
}