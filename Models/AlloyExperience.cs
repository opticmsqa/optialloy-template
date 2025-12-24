using EPiServer.SpecializedProperties;
using EPiServer.VisualBuilder;

namespace OptiAlloy.Models;

/// <summary>
/// Example of experience with static properties
/// </summary>
[ContentType(GUID = "CA48BF33-4C0D-44C8-B94D-CBC6B426A1A1")]
public class AlloyExperience : ExperienceData
{
    public virtual ContentArea ContentArea1 { get; set; }

    public virtual ContentReference ContentReference1 { get; set; }

    public virtual LinkItemCollection LinkItemCollection1 { get; set; }

    public virtual LinkItem LinkItem1 { get; set; }
}
