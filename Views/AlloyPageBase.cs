using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OptiAlloy.Business.Rendering;

namespace OptiAlloy.Views;

public abstract class AlloyPageBase<TModel> : RazorPage<TModel> where TModel : class
{
    private readonly AlloyContentAreaItemRenderer _alloyContentAreaItemRenderer;

    public abstract override Task ExecuteAsync();

    public AlloyPageBase() : this(ServiceLocator.Current.GetInstance<AlloyContentAreaItemRenderer>())
    {
    }

    public AlloyPageBase(AlloyContentAreaItemRenderer alloyContentAreaItemRenderer)
    {
        _alloyContentAreaItemRenderer = alloyContentAreaItemRenderer;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Convention API")]
    protected void OnItemRendered(ContentAreaItem contentAreaItem, TagHelperContext context, TagHelperOutput output)
    {
        _alloyContentAreaItemRenderer.RenderContentAreaItemCss(contentAreaItem, output);
    }
}
