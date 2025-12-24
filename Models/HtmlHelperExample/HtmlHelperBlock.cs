namespace OptiAlloy.Models.HtmlHelperExample;

[ContentType(
    GUID = "148317FF-926D-434B-AC99-A20C81730E6E",
    DisplayName = "Example block for HTML Helpers",
    CompositionBehaviors = [CompositionBehavior.SectionEnabledKey, CompositionBehavior.ElementEnabledKey]
    )]
public class HtmlHelperBlock : BlockData
{
    public virtual XhtmlString Body { get; set; }
}
