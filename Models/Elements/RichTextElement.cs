using EPiServer.Validation;

namespace OptiAlloy.Models.Elements;

[ContentType(
    GUID = "70FF2666-B976-4069-80EC-C764E590077E",
    DisplayName = "RichText",
    GroupName = "Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
public class RichTextElement : BlockData
{
    /// <summary>
    /// Gets/sets the main body of the element.
    /// </summary>
    public virtual XhtmlString Body { get; set; }
}

public class RichTextElementValidation : IValidate<RichTextElement>
{
    public IEnumerable<ValidationError> Validate(RichTextElement instance)
    {
        if (instance != null && instance.Body != null)
        {
            if (instance.Body.ToHtmlString()?.Contains("error") == true)
            {
                yield return new ValidationError
                {
                    ErrorMessage = "Property HTML cannot have this value",
                    Severity = ValidationErrorSeverity.Error,
                    Source = instance,
                    PropertyName = nameof(RichTextElement.Body)
                };
            }
        }
    }
}
