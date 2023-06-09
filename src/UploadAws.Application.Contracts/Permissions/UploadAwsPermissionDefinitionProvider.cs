using UploadAws.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace UploadAws.Permissions;

public class UploadAwsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(UploadAwsPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(UploadAwsPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<UploadAwsResource>(name);
    }
}
