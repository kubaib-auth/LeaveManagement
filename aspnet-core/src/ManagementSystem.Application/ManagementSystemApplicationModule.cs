using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ManagementSystem.Authorization;

namespace ManagementSystem
{
    [DependsOn(
        typeof(ManagementSystemCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ManagementSystemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ManagementSystemAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ManagementSystemApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
