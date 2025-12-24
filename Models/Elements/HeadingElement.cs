namespace OptiAlloy.Models.Elements;

[ContentType(
    GUID = "545BC516-50E8-4A70-AB0E-30171B26B22B",
    DisplayName = "Heading",
    GroupName = "Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
public class HeadingElement : BlockData
{
    /// <summary>
    /// Gets/sets the main body of the element.
    /// </summary>
    public virtual string Body { get; set; }
}
