using System;
using System.Collections.Generic;
using System.Text;
using UploadAws.Localization;
using Volo.Abp.Application.Services;

namespace UploadAws;

/* Inherit your application services from this class.
 */
public abstract class UploadAwsAppService : ApplicationService
{
    protected UploadAwsAppService()
    {
        LocalizationResource = typeof(UploadAwsResource);
    }
}
