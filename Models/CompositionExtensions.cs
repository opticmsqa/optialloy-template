using EPiServer.VisualBuilder;
using EPiServer.VisualBuilder.Compositions;
using EPiServer.Web;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Mvc.TagHelpers.Experience.Internal;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EPiServer.Cms.VisualBuilder.Rendering;

#nullable enable

public static class CompositionExtensions
{
    private static CompositionNode? GetCurrentRenderingNode(this ViewDataDictionary viewData) =>
        viewData.TryGetCompositionRenderingNode(out var compositionRenderingNode) ? compositionRenderingNode : null;

    private static IDictionary<string, string>? GetDisplaySettings(this ViewDataDictionary viewData) =>
        viewData.GetCurrentRenderingNode()?.DisplaySettings;

    public static string GetComponentCss<TModel>(this ViewDataDictionary<TModel> viewData, string key, Func<string, string> mapFunction, string defaultCss = "")
    {
        var displaySettings = viewData.GetDisplaySettings();

        if (displaySettings is null || !displaySettings.TryGetValue(key, out var setting))
        {
            return defaultCss;
        }

        return mapFunction(setting);

    }

    public static string GetComponentCss<TModel>(this ViewDataDictionary<TModel> viewData, string key, IReadOnlyDictionary<string, string> map, string defaultCss = "")
    {
        var displaySettings = viewData.GetDisplaySettings();

        if (displaySettings is not null && displaySettings.TryGetValue(key, out var setting))
        {
            if (map.TryGetValue(setting, out var css))
            {
                return css;
            }
        }

        return defaultCss;
    }

    public static string GetTheme(this ExperienceData model)
    {
        if (model.Layout.DisplayTemplateKey == "unicornExperience")
        {
            return "unicorn";
        }

        return "";
    }

    public static string GetTheme(this SectionData model)
    {
        if (model.Layout.DisplayTemplateKey == "unicornExperience")
        {
            return "unicorn";
        }

        return "";
    }

    public static string GetTheme(this CompositionNode model)
    {
        if (model.DisplayTemplateKey == "unicornExperience")
        {
            return "unicorn";
        }

        return "";
    }

    public static string? GetCss(this CompositionNode node, string key, Dictionary<string, string> map, string? defaultCss = null)
    {
        if (node.DisplaySettings.TryGetValue(key, out var setting))
        {
            if (map.TryGetValue(setting, out var css))
            {
                return css;
            }
        }
        return defaultCss;
    }

    public static IHtmlContent EditAttributes(this IHtmlHelper html, CompositionNode? node)
    {
        if (!string.IsNullOrWhiteSpace(node?.Key)
            && html.ViewContext.HttpContext.RequestServices.GetRequiredService<IContextModeResolver>().CurrentMode == ContextMode.Edit)
        {
            return new HtmlString($"data-epi-block-id=\"{node.Key}\"");
        }

        return HtmlString.Empty;
    }

    public static Task RenderCompositionSectionOrBlock(this IHtmlHelper html, CompositionNode node)
    {
        return node switch
        {
            SectionNode sectionNode => html.RenderContentDataAsync(sectionNode.SectionData, false, [sectionNode.DisplayTemplateKey], new { Node = sectionNode }),
            ComponentNode componentNode => html.RenderContentDataAsync(componentNode.Component, false, [componentNode.DisplayTemplateKey], new { Node = componentNode }),
            _ => Task.CompletedTask
        };
    }
}
