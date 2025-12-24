using EPiServer.Cms.FeatureManagement;
using EPiServer.Cms.Shell.UI;
using EPiServer.Cms.Shell.UI.Configurations;
using EPiServer.Cms.Shell.UI.VisualBuilder.Services;
using EPiServer.Cms.TinyMce.Core;
using EPiServer.Cms.TinyMce.PropertySettings.Internal;
using EPiServer.Cms.UI.Admin;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Cms.UI.VisitorGroups;
using EPiServer.Cms.UI.VisualBuilder.Services.ThumbnailResolving;
using EPiServer.DependencyInjection;
using EPiServer.Web;
using EPiServer.Web.Routing;
using OptiAlloy.Extensions;
using OptiAlloy.Models.Elements;

namespace OptiAlloy;

public class Startup
{
    private readonly IWebHostEnvironment _webHostingEnvironment;
    private readonly IConfiguration _configuration;
    private readonly string corsPolicyName = "myCorsPolicy";

    public Startup(IWebHostEnvironment webHostingEnvironment, IConfiguration configuration)
    {
        _webHostingEnvironment = webHostingEnvironment;
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddPolicy(corsPolicyName, builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

        services
            // .AddSegmentTelemetry()
            .Configure<AdminOptions>(o => o.SectionsVisibility = new()
            {
                License = true
            })
            .AddCmsHost()
            .AddCmsCore()
            .AddCmsAspNetIdentity<ApplicationUser>()
            .AddCmsHtmlHelpers()
            .AddCmsTagHelpers()
            .AddCmsUI()
            .AddAdmin()
            .AddTinyMce()
            .AddCmsImageSharpImageLibrary()
            .AddVisitorGroupsMvc()
            .AddVisitorGroupsUI()
            .AddAlloy()
            .AddAdminUserRegistration(options =>
                options.Behavior = RegisterAdminUserBehaviors.Enabled)
            .AddEmbeddedLocalization<Globals>();

        services.TryIntercept<IFeatureEvaluator>(
            (provider, evaluator) => new OptiAlloyFeatureEvaluator(evaluator, provider.GetService<IContentLoader>()));

        services.AddTransient<IBlueprintThumbnailGenerator, WebPagePreviewTemplateThumbnailGenerator>();

        services.AddCmsValidator<CustomElementValidation>()
            .AddCmsValidator<RichTextElementValidation>();

        // Required by Wangkanai.Detection
        services.AddDetection();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.Configure<UIOptions>(options =>
        {
            options.InlineBlocksInContentAreaEnabled = true;
        });

        services.Configure<CmsFeatureOptions>(o =>
        {
            o.SectionsVisibility = new() { Archiving = false, VisitorGroups = true };
        });

        services.Configure<TinyMcePropertySettingsOptions>(o =>
        {
            o.Enabled = true;
        });

        services.Configure<TinyMceConfiguration>(config =>
        {
            var defaultSettings = config.Default();
            defaultSettings.AddPlugin("code").AppendToolbar("code");
        });

        //Using DXP
        //services.AddCmsCloudPlatformSupport(_configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // Required by Wangkanai.Detection
        app.UseDetection();
        app.UseSession();
        app.UseCors(corsPolicyName);
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapContent();
            endpoints.MapDefaultControllerRoute();
        });
    }
}
