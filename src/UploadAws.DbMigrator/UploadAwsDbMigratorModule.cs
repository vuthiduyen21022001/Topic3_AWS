using UploadAws.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace UploadAws.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(UploadAwsEntityFrameworkCoreModule),
    typeof(UploadAwsApplicationContractsModule)
    )]
public class UploadAwsDbMigratorModule : AbpModule
{

}
