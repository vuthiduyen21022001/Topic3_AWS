using UploadAws.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace UploadAws.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class UploadAwsPageModel : AbpPageModel
{
    protected UploadAwsPageModel()
    {
        LocalizationResourceType = typeof(UploadAwsResource);
    }
}
