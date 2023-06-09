using System.Threading.Tasks;

namespace UploadAws.Data;

public interface IUploadAwsDbSchemaMigrator
{
    Task MigrateAsync();
}
