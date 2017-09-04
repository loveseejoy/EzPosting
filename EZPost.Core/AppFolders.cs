using System;
using Abp.Dependency;

namespace EZPost
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string Images { set;get; }
        public string ReturnImages { get; set; }
    }
}