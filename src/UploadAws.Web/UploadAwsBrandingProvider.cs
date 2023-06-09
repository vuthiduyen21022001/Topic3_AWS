using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace UploadAws.Web;

[Dependency(ReplaceServices = true)]
public class UploadAwsBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "UploadAws";
}
