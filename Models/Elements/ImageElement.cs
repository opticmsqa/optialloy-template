using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace OptiAlloy.Models.Elements;

[ContentType(
    GUID = "A2659914-5F1A-4C63-B48D-48C6B55CDBCC",
    DisplayName = "Image",
    GroupName = "Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
public class ImageElement : BlockData
{
    /// <summary>
    /// Gets/sets the alternative text for the image.
    /// </summary>
    [ScaffoldColumn(false)]
    public virtual string AltText { get; set; }

    /// <summary>
    /// Gets/sets the image reference.
    /// </summary>
    [UIHint(UIHint.Image)]
    public virtual ContentReference Image { get; set; }
}
