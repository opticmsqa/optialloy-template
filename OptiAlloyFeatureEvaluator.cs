using EPiServer.Cms.FeatureManagement;

namespace OptiAlloy;

public class OptiAlloyFeatureEvaluator(IFeatureEvaluator inner, IContentLoader contentLoader) : IFeatureEvaluator
{
    public ValueTask<bool> IsEnabledAsync(string feature, CancellationToken cancellationToken) =>
        ValueTask.FromResult(IsEnabled(feature));

    public bool IsEnabled(string feature) =>
        feature switch
        {
            Features.GlobalFallbackLanguagesEnabled => true,
            Features.ContentManagerLiteEnabled => GetFeatureFromStartPageOrDefault(Features.ContentManagerLiteEnabled, false),
            Features.ContentVariationsEnabled => true,
            Features.ContentTypeContractsEnabled => true,
            Features.ContentSourceEnabled => true,
            Features.ContentSourceUIEnabled => true,
            Features.ContentBindingsEnabled => GetFeatureFromStartPageOrDefault(Features.ContentBindingsEnabled, false),
            _ => inner.IsEnabled(feature)
        };

    private bool GetFeatureFromStartPageOrDefault(string feature, bool defaultValue)
    {
        if (contentLoader.TryGet<StartPage>(ContentReference.StartPage, out var startPage))
        {
            return feature switch
            {
                Features.ContentBindingsEnabled => startPage.IsContentBindingsEnabled.GetValueOrDefault(defaultValue),
                Features.ContentManagerLiteEnabled => startPage.IsContentManagerLiteEnabled.GetValueOrDefault(defaultValue),
                _ => defaultValue
            };
        }
        return defaultValue;
    }
}
