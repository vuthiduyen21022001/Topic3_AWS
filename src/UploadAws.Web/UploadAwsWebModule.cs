using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UploadAws.EntityFrameworkCore;
using UploadAws.Localization;
using UploadAws.MultiTenancy;
using UploadAws.Web.Menus;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.BlobStoring.Aws;
using Volo.Abp.BlobStoring;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;   
namespace UploadAws.Web;

[DependsOn(
    typeof(UploadAwsHttpApiModule),
    typeof(UploadAwsApplicationModule),
    typeof(UploadAwsEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpSettingManagementWebModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpBlobStoringAwsModule)

    )]
public class UploadAwsWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(UploadAwsResource),
                typeof(UploadAwsDomainModule).Assembly,
                typeof(UploadAwsDomainSharedModule).Assembly,
                typeof(UploadAwsApplicationModule).Assembly,
                typeof(UploadAwsApplicationContractsModule).Assembly,
                typeof(UploadAwsWebModule).Assembly
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("UploadAws");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

       
        ConfigureAuthentication(context);
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureSwaggerServices(context.Services);

        var configurationBuilder = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json");

        IConfiguration configurationa = configurationBuilder.Build();



        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseAws(Aws =>
                {
                    Aws.AccessKeyId = "AKIATNBK4DPL7SHEJN55";
                    Aws.SecretAccessKey = "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";
                    Aws.UseCredentials = false;
                    Aws.UseTemporaryCredentials = false;
                    Aws.UseTemporaryFederatedCredentials = false;
                    Aws.ProfileName = "the name of the profile to get credentials from";
                    Aws.ProfilesLocation = "https://hqsoftawsadmin.signin.aws.amazon.com/console";
                    Aws.Region = "ap-southeast-1";
                    Aws.Name = "demo01";
                    Aws.Policy = "{\r\n    " +
                    "\"Version\": \"2012-10-17\",\r\n    " +
                    "\"Statement\": [\r\n        {\r\n            " +
                    "\"Effect\": \"Allow\",\r\n           " +
                    " \"Action\": [\r\n                " +
                    "\"s3:*\",\r\n                " +
                    "\"s3-object-lambda:*\"\r\n            ],\r\n            " +
                    "\"Resource\": \"*\"\r\n        }\r\n    ]\r\n}"; ;
                    Aws.DurationSeconds = 1000;
                    Aws.ContainerName = "awsdemo02";
                    Aws.CreateContainerIfNotExists = true;
                });
            });
        });


        //Configure<AbpBlobStoringOptions>(options =>
        //{
        //    options.Containers.ConfigureDefault(container =>
        //    {
        //        container.UseAws(Aws =>
        //        {
        //            Aws.AccessKeyId = "AKIATNBK4DPL7SHEJN55";
        //            Aws.SecretAccessKey = "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";
        //            Aws.UseCredentials = false;
        //            Aws.UseTemporaryCredentials = false;
        //            Aws.UseTemporaryFederatedCredentials = false;
        //            Aws.ProfileName = "the name of the profile to get credentials from";
        //            Aws.ProfilesLocation = "https://hqsoftawsadmin.signin.aws.amazon.com/console";
        //            Aws.Region = "ap-southeast-1";
        //            Aws.Name = "demo01";
        //            Aws.Policy = "{\r\n    " +
        //            "\"Version\": \"2012-10-17\",\r\n    " +
        //            "\"Statement\": [\r\n        {\r\n            " +
        //            "\"Effect\": \"Allow\",\r\n           " +
        //            " \"Action\": [\r\n                " +
        //            "\"s3:*\",\r\n                " +
        //            "\"s3-object-lambda:*\"\r\n            ],\r\n            " +
        //            "\"Resource\": \"*\"\r\n        }\r\n    ]\r\n}"; ;
        //            Aws.DurationSeconds = 1000;
        //            Aws.ContainerName = "awsdemo02";
        //            Aws.CreateContainerIfNotExists = true;
        //        });
        //    });
        //});

    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<UploadAwsWebModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<UploadAwsDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}UploadAws.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<UploadAwsDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}UploadAws.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<UploadAwsApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}UploadAws.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<UploadAwsApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}UploadAws.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<UploadAwsWebModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new UploadAwsMenuContributor());
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(UploadAwsApplicationModule).Assembly);
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "UploadAws API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "UploadAws API");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
