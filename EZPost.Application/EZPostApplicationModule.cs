using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using AutoMapper;
using EZPost.Users;
using EZPost.Users.Dto;

namespace EZPost
{
    [DependsOn(typeof(EZPostCoreModule), typeof(AbpAutoMapperModule))]
    public class EZPostApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
                mapper.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());

                mapper.CreateMap<User, UserListDto>();

            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
