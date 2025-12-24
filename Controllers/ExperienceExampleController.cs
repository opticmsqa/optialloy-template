using EPiServer.Framework.DataAnnotations;
using EPiServer.VisualBuilder;
using Microsoft.AspNetCore.Mvc;
using OptiAlloy.Models.HtmlHelperExample;
using OptiAlloy.Models.ViewModels;

namespace OptiAlloy.Controllers;

/// <summary>
/// Example to show how to use HTML helpers with the composition
/// </summary>
[TemplateDescriptor(ModelType = typeof(HtmlHelperExperience))]
public sealed class ExperienceExampleController(ICompositionMapper compositionMapper) : PageControllerBase<HtmlHelperExperience>
{
    public IActionResult Index(HtmlHelperExperience currentPage)
    {
        var composition = compositionMapper.ToComposition(currentPage);
        var model = new ExperienceViewModel<HtmlHelperExperience>(currentPage, composition);
        return View(model);
    }
}
