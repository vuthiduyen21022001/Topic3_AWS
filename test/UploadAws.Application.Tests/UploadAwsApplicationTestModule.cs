using Volo.Abp.Modularity;

namespace UploadAws;

[DependsOn(
    typeof(UploadAwsApplicationModule),
    typeof(UploadAwsDomainTestModule)
    )]
public class UploadAwsApplicationTestModule : AbpModule
{

}
