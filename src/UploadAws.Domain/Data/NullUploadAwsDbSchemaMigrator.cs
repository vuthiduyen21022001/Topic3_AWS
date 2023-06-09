using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace UploadAws.Data;

/* This is used if database provider does't define
 * IUploadAwsDbSchemaMigrator implementation.
 */
public class NullUploadAwsDbSchemaMigrator : IUploadAwsDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
