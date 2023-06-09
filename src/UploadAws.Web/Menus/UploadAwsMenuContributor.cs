using System.Threading.Tasks;
using UploadAws.Localization;
using UploadAws.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace UploadAws.Web.Menus;

public class UploadAwsMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<UploadAwsResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                UploadAwsMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );
       
     context.Menu.AddItem(
    new ApplicationMenuItem(
        "Saless",
        l["Menu:Sales"],
        icon: "fa fa-book"
    ).AddItem(
        new ApplicationMenuItem(
            "Saless.Sales",
            l["Menu:Sales"],
            url: "/Sales"
        )
    )
);


        //context.Menu.Items.Insert(
        //    1,
        //    new ApplicationMenuItem(
        //        UploadAwsMenus.Home,
        //        l["AWS"],
        //        "/awsconfig",
        //        icon: "fas fa-cog",
        //        order: 1
        //    )
        //);

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
