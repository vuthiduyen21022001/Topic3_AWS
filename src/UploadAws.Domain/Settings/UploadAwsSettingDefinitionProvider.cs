using Volo.Abp.Settings;

namespace UploadAws.Settings;

public class UploadAwsSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(UploadAwsSettings.MySetting1));
    }
}
