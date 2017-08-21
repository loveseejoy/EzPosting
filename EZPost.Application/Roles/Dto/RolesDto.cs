using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using EZPost.Authorization.Roles;

namespace EZPost.Roles.Dto
{
    [AutoMapFrom(typeof(Role))]
    public class RolesDto:EntityDto, IHasCreationTime
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsStatic { set; get; }
        public DateTime CreationTime { get; set; }
    }
}