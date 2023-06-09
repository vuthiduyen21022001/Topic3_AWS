using UploadAws.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace UploadAws;

[DependsOn(
    typeof(UploadAwsEntityFrameworkCoreTestModule)
    )]
public class UploadAwsDomainTestModule : AbpModule
{

}
