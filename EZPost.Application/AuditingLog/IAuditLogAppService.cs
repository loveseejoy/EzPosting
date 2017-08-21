using Abp.Application.Services;
using EZPost.AuditingLog.Dto;
using EZPost.Common.WebControl;

namespace EZPost.AuditingLog
{
    public interface IAuditLogAppService: IApplicationService
    {
        IPagedList<AuditLogListDto> Gets(GetAuditLogsInput input);
    }
}