using System;
using Abp.Dependency;

namespace EZPost
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string DownLoadFolder { get; set; }

        /// <summary>
        /// 保存EventC的文件夹
        /// </summary>
        public string EventCFileFolder { get; set; }

        
        /// <summary>
        /// 保存Hawb导入Excel的文件夹 
        /// </summary>
        public string UploadHawbExcelFolder { get; set; }


        public string DhlTrackingFoledr { get; set; }
    }
}