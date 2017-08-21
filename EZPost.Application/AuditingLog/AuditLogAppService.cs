using System.Collections.Generic;
using System.Linq;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using EZPost.AuditingLog.Dto;
using EZPost.Common.WebControl;
using EZPost.Users;
using Abp.Extensions;
using EZPost.Authorization;
using EZPost.Common;

namespace EZPost.AuditingLog
{
    [DisableAuditing]
    [AbpAuthorize(PermissionNames.Pages_Setting_AuditLogs)]
    public class AuditLogAppService:EZPostAppServiceBase,IAuditLogAppService
    {
        private readonly IRepository<AuditLog, long> _auditLogRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly INamespaceStripper _namespaceStripper;

        public AuditLogAppService(
          IRepository<AuditLog, long> auditLogRepository,
          IRepository<User, long> userRepository,
          INamespaceStripper namespaceStripper
        )
        {
            _auditLogRepository = auditLogRepository;
            _userRepository = userRepository;
            _namespaceStripper = namespaceStripper;
        }

        public IPagedList<AuditLogListDto> Gets(GetAuditLogsInput input)
        {
            var query = CreateAuditLogAndUsersQuery(input);
            var count =  query.Count();
            var list = query.OrderBys(input).PageBy(input).ToList();
            var listDto = ConvertToAuditLogListDtos(list);
            return  new PagedList<AuditLogListDto>(count,listDto);
        }

        private List<AuditLogListDto> ConvertToAuditLogListDtos(List<AuditLogAndUser> results)
        {
            return results.Select(
                result =>
                {
                    var auditLogListDto = result.AuditLog.MapTo<AuditLogListDto>();
                    auditLogListDto.UserName = result.User == null ? null : result.User.UserName;
                    auditLogListDto.ServiceName = _namespaceStripper.StripNameSpace(auditLogListDto.ServiceName);
                    return auditLogListDto;
                }).ToList();
        }

        private IQueryable<AuditLogAndUser> CreateAuditLogAndUsersQuery(GetAuditLogsInput input)
        {
            input.EndDate = input.EndDate.AddDays(1);

            var query = from auditLog in _auditLogRepository.GetAll()
                        join user in _userRepository.GetAll() on auditLog.UserId equals user.Id into userJoin
                        from joinedUser in userJoin.DefaultIfEmpty()
                        where auditLog.ExecutionTime >= input.StartDate && auditLog.ExecutionTime <input.EndDate
                        select new AuditLogAndUser { AuditLog = auditLog, User = joinedUser };

            query = query
                .WhereIf(!input.UserName.IsNullOrWhiteSpace(), item => item.User.UserName.Contains(input.UserName))
                .WhereIf(!input.ServiceName.IsNullOrWhiteSpace(), item => item.AuditLog.ServiceName.Contains(input.ServiceName))
                .WhereIf(!input.MethodName.IsNullOrWhiteSpace(), item => item.AuditLog.MethodName.Contains(input.MethodName))
                .WhereIf(!input.BrowserInfo.IsNullOrWhiteSpace(), item => item.AuditLog.BrowserInfo.Contains(input.BrowserInfo))
                .WhereIf(input.MinExecutionDuration.HasValue && input.MinExecutionDuration > 0, item => item.AuditLog.ExecutionDuration >= input.MinExecutionDuration.Value)
                .WhereIf(input.MaxExecutionDuration.HasValue && input.MaxExecutionDuration < int.MaxValue, item => item.AuditLog.ExecutionDuration <= input.MaxExecutionDuration.Value)
                .WhereIf(input.HasException == true, item => item.AuditLog.Exception != null && item.AuditLog.Exception != "")
                .WhereIf(input.HasException == false, item => item.AuditLog.Exception == null || item.AuditLog.Exception == "");
            return query;
        }
    }
}