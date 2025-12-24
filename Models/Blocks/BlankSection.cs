using EPiServer.VisualBuilder;

namespace OptiAlloy.Models.Blocks;

/// <summary>
/// Class representing a section without a layout
/// </summary>
[ContentType(GUID = "951a2959-cbad-4c9a-86cd-f17d3537b3d1",
    GroupName = "Default",
    DisplayName = "Blank Section",
    Description = "A section without a predefined layout.")]
public class BlankSection : SectionData;
