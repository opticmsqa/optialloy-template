using Microsoft.AspNetCore.Mvc.Razor;

namespace OptiAlloy.Business.Rendering;

public class ExperienceViewEngineLocationExpander : IViewLocationExpander
{
    // Path arguments:
    // 0 - expanderContext.ViewName,
    // 1 - expanderContext.ControllerName,
    // 2 - expanderContext.AreaName
    private const string ExperienceViewFormat = TemplateCoordinator.ExperienceFolder + "{0}.cshtml";

    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        var isExperienceController = string.Equals(context.ControllerName, "Experience", StringComparison.Ordinal);
        if (isExperienceController)
        {
            // prioritize views from the experiences folder when request come from the ExperienceController
            yield return ExperienceViewFormat;
        }

        foreach (var location in viewLocations)
        {
            yield return location;
        }

        if (!isExperienceController)
        {
            yield return ExperienceViewFormat;
        }
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
    }
}
