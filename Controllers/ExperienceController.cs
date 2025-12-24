using EPiServer.Framework.DataAnnotations;
using EPiServer.VisualBuilder;
using Microsoft.AspNetCore.Mvc;
using OptiAlloy.Models.ViewModels;

namespace OptiAlloy.Controllers;

[TemplateDescriptor(Inherited = true, ModelType = typeof(ExperienceData))]
public sealed class ExperienceController : PageControllerBase<ExperienceData>
{
    public IActionResult Index(ExperienceData currentPage)
    {
        var model = CreateModel(currentPage);
        return View(model);
    }

    /// <summary>
    /// Creates a PageViewModel where the type parameter is the type of the experience.
    /// </summary>
    /// <remarks>
    /// Used to create models of a specific type without the calling method having to know that type.
    /// </remarks>
    private static object CreateModel(ExperienceData currentPage)
    {
        var type = typeof(PageViewModel<>).MakeGenericType(currentPage.GetOriginalType());
        return Activator.CreateInstance(type, currentPage);
    }
}
