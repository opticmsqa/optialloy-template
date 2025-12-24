using EPiServer.Cms.Shell.UI.VisualBuilder.Services;
using EPiServer.ServiceLocation;
using PuppeteerSharp;

namespace EPiServer.Cms.UI.VisualBuilder.Services.ThumbnailResolving;

[ServiceConfiguration(typeof(IBlueprintThumbnailGenerator))]
internal sealed class WebPagePreviewTemplateThumbnailGenerator(ILogger<WebPagePreviewTemplateThumbnailGenerator> log) : IBlueprintThumbnailGenerator
{
    /*private readonly ProtectedModuleOptions _protectedModuleOptions;

    public WebPagePreviewTemplateThumbnailGenerator(ProtectedModuleOptions protectedModuleOptions)
    {
        _protectedModuleOptions = protectedModuleOptions;
    }*/

    public async Task<Stream> Generate(string url, string sectionId)
    {
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        var launchOptions = new LaunchOptions
        {
            Headless = true
        };

        await using (var browser = await Puppeteer.LaunchAsync(launchOptions))
        await using (var page = await browser.NewPageAsync())
        {
            try
            {
                /*var host = request.Scheme + "://" + request.Host;

                var protectedModuleUrl = _protectedModuleOptions.RootPath.TrimStart('~');
                protectedModuleUrl = protectedModuleUrl.TrimStart('/');
                protectedModuleUrl = protectedModuleUrl.TrimEnd('/');

                await page.GoToAsync(host + "/" + protectedModuleUrl + "/CMS/");
                foreach (var cookie in request.Cookies)
                {
                    await page.SetCookieAsync(new CookieParam
                    {
                        Name = cookie.Key,
                        Value = cookie.Value
                    });
                }*/

                await page.GoToAsync(url);
                if (string.IsNullOrWhiteSpace(sectionId))
                {
                    var result = await page.ScreenshotStreamAsync();
                    return result;
                }
                else
                {
                    var sectionEl = await page.QuerySelectorAsync("[data-epi-inline-block-id=" + sectionId + "]");
                    var result = await sectionEl.ScreenshotStreamAsync();
                    return result;
                }
            }
            catch (Exception e)
            {
                log.CannotGenerateScreenshot(e);
                return null;
            }
        }
    }
}

internal static partial class WebPagePreviewTemplateThumbnailGeneratorExtensions
{
    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Cannot generate screenshot")]
    public static partial void CannotGenerateScreenshot(this ILogger<WebPagePreviewTemplateThumbnailGenerator> logger, Exception exception);
}
