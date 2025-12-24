
using EPiServer.VisualBuilder;

namespace OptiAlloy.Models.Pages;

/// <summary>
/// Class representing an experience without a layout
/// </summary>
[ContentType(GUID = "81f0ceb3-d44d-43b3-9421-59be651a9138",
    DisplayName = "Blank Experience",
    Description = "An experience without a predefined layout.")]
public class BlankExperience : ExperienceData { }
