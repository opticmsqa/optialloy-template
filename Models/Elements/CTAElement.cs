namespace OptiAlloy.Models.Elements;

[ContentType(
    GUID = "9BAD7B0E-C344-4BC7-90E7-D8E92E69EC9D",
    DisplayName = "Call To Action",
    GroupName = "Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
public class CTAElement : BlockData
{
    /// <summary>
    /// Gets/sets the call to action text.
    /// </summary>
    public virtual string Text { get; set; }

    /// <summary>
    /// Gets/sets the content reference.
    /// </summary>
    public virtual ContentReference Link { get; set; }
}
