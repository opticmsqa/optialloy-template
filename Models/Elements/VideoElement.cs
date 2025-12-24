using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace OptiAlloy.Models.Elements;

[ContentType(
    GUID = "5D2831CB-A5CF-45E9-8FC3-2BE3D9A3B694",
    DisplayName = "Video",
    GroupName = "Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
public class VideoElement : BlockData
{
    /// <summary>
    /// Gets/sets the video reference.
    /// </summary>
    [UIHint(UIHint.Video)]
    public virtual ContentReference Video { get; set; }
}
