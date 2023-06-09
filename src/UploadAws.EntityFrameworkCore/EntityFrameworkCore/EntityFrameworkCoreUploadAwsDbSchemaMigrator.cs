using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UploadAws.Data;
using Volo.Abp.DependencyInjection;

namespace UploadAws.EntityFrameworkCore;

public class EntityFrameworkCoreUploadAwsDbSchemaMigrator
    : IUploadAwsDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreUploadAwsDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the UploadAwsDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<UploadAwsDbContext>()
            .Database
            .MigrateAsync();
    }
}
