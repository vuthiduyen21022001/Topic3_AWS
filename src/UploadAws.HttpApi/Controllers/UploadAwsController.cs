using UploadAws.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace UploadAws.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class UploadAwsController : AbpControllerBase
{
    protected UploadAwsController()
    {
        LocalizationResource = typeof(UploadAwsResource);
    }
}
