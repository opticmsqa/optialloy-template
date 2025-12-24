namespace OptiAlloy.Business.Rendering;

public static class ContentAreaItemExtensions
{
    public static string GetBlockId(this ContentAreaItem contentAreaItem)
    {
        if (contentAreaItem.RenderSettings != null && contentAreaItem.RenderSettings.TryGetValue("data-epi-block-id", out var id))
        {
            return id.ToString();
        }

        return null;
    }
}
