using System.ComponentModel.DataAnnotations;
using EPiServer.Validation;

namespace OptiAlloy.Models.Elements;

[ContentType(
    GUID = "7DB4E8DE-2DC6-44E2-A715-056375C33101",
    DisplayName = "Custom Element 1",
    GroupName = "Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
public class CustomElement : BlockData
{
    /// <summary>
    /// Gets/sets the call to action text.
    /// </summary>
    public virtual string Text { get; set; }

    public virtual string Text2 { get; set; }
}

[ContentType(
    GUID = "683FC138-DD84-46E0-BE59-B773AD1BDCCE",
    DisplayName = "Custom Element 2",
    GroupName = "Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
public class CustomElement2 : BlockData
{
    [Required]
    public virtual string Text { get; set; }

    [MaxLength(5)]
    public virtual string Text2 { get; set; }
}

public class CustomElementValidation : IValidate<CustomElement>
{
    public IEnumerable<ValidationError> Validate(CustomElement instance)
    {
        if (instance != null)
        {
            if (instance.Text == "error")
            {
                yield return new ValidationError
                {
                    ErrorMessage = "Property cannot have this value",
                    Severity = ValidationErrorSeverity.Error,
                    Source = instance,
                    PropertyName = nameof(CustomElement.Text)
                };
            }

            if (instance.Text2 == "error")
            {
                yield return new ValidationError
                {
                    ErrorMessage = "Property TEXT 2 cannot have this value",
                    Severity = ValidationErrorSeverity.Error,
                    Source = instance,
                    PropertyName = nameof(CustomElement.Text2)
                };
            }
        }
    }
}
