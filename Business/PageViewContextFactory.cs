using EPiServer.Applications;
using EPiServer.Data;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OptiAlloy.Models.ViewModels;

namespace OptiAlloy.Business;

[ServiceConfiguration]
public class PageViewContextFactory
{
    private readonly IContentLoader _contentLoader;
    private readonly UrlResolver _urlResolver;
    private readonly IDatabaseMode _databaseMode;
    private readonly IApplicationResolver _applicationResolver;
    private readonly ServiceAccessor<SystemDefinition> _systemDefinition;
    private readonly CookieAuthenticationOptions _cookieAuthenticationOptions;

    public PageViewContextFactory(
        IContentLoader contentLoader,
        UrlResolver urlResolver,
        IDatabaseMode databaseMode,
        IApplicationResolver applicationResolver,
        ServiceAccessor<SystemDefinition> systemDefinition,
        IOptionsMonitor<CookieAuthenticationOptions> optionMonitor)
    {
        _contentLoader = contentLoader;
        _urlResolver = urlResolver;
        _databaseMode = databaseMode;
        _applicationResolver = applicationResolver;
        _systemDefinition = systemDefinition;
        _cookieAuthenticationOptions = optionMonitor.Get(IdentityConstants.ApplicationScheme);
    }

    public virtual LayoutModel CreateLayoutModel(ContentReference currentContentLink, HttpContext httpContext)
    {
        var website = _applicationResolver.GetByContext() as Website;
        var startPageContentLink = website?.RoutingEntryPoint;

        // Use the content link with version information when editing the startpage,
        // otherwise the published version will be used when rendering the props below.
        if (currentContentLink.CompareToIgnoreWorkID(startPageContentLink))
        {
            startPageContentLink = currentContentLink;
        }

        var layoutModel = new LayoutModel
        {
            LoggedIn = httpContext.User.Identity.IsAuthenticated,
            LoginUrl = new HtmlString(GetLoginUrl(currentContentLink)),
            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
        };

        if (!ContentReference.IsNullOrEmpty(startPageContentLink) && _contentLoader.TryGet<StartPage>(startPageContentLink, out var startPage))
        {
            layoutModel.Logotype = startPage.SiteLogotype;
            layoutModel.LogotypeLinkUrl = new HtmlString(_urlResolver.GetUrl(startPageContentLink));
            layoutModel.ProductPages = startPage.ProductPageLinks;
            layoutModel.CompanyInformationPages = startPage.CompanyInformationPageLinks;
            layoutModel.NewsPages = startPage.NewsPageLinks;
            layoutModel.CustomerZonePages = startPage.CustomerZonePageLinks;
            layoutModel.SearchActionUrl = new HtmlString(UrlResolver.Current.GetUrl(startPage.SearchPageLink));
        }

        return layoutModel;
    }

    private string GetLoginUrl(ContentReference returnToContentLink)
    {
        return $"{_cookieAuthenticationOptions?.LoginPath.Value ?? Globals.LoginPath}?ReturnUrl={_urlResolver.GetUrl(returnToContentLink)}";
    }

    public virtual IContent GetSection(ContentReference contentLink)
    {
        var currentContent = _contentLoader.Get<IContent>(contentLink);
        var systemDefinition = _systemDefinition();
        var website = _applicationResolver.GetByContext() as Website;
        var currentStartPage = website?.RoutingEntryPoint;

        bool isSectionRoot(ContentReference contentReference) =>
            ContentReference.IsNullOrEmpty(contentReference) ||
            contentReference.Equals(currentStartPage) ||
            contentReference.Equals(systemDefinition.RootPage);

        if (isSectionRoot(currentContent.ParentLink))
        {
            return currentContent;
        }

        return _contentLoader.GetAncestors(contentLink)
            .OfType<PageData>()
            .SkipWhile(x => !isSectionRoot(x.ParentLink))
            .FirstOrDefault();
    }
}
