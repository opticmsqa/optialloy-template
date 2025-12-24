using System.ComponentModel.DataAnnotations;
using EPiServer.VisualBuilder;

namespace OptiAlloy.Models.Sections;

[ContentType(GUID = "80F54E4A-E61B-496E-9DED-B962AF8E8D17", GroupName = "Default", DisplayName = "Alloy Section")]
public class AlloySection : SectionData
{
    [Display(GroupName = SystemTabNames.Content)]
    [CultureSpecific]
    public virtual string Title { get; set; }

    [Display(Name = "Short description", GroupName = SystemTabNames.Content)]
    public virtual string Description { get; set; }

    [Display(Name = "Main body", GroupName = SystemTabNames.Content)]
    public virtual XhtmlString MainBody { get; set; }

    [Display(Name = "Integer 1", GroupName = SystemTabNames.Content)]
    public virtual int Integer1 { get; set; }

    [Display(Name = "Integer List 1", GroupName = SystemTabNames.Content)]
    public virtual IList<int> IntegerList1 { get; set; }

    [Display(Name = "Custom setting 1", GroupName = SystemTabNames.Content)]
    public virtual string Setting1 { get; set; }

    [Display(Name = "Custom setting 2", GroupName = SystemTabNames.Content)]
    public virtual string Setting2 { get; set; }

    [Display(Name = "Custom setting 3", GroupName = SystemTabNames.Content)]
    public virtual string Setting3 { get; set; }

    [Display(Name = "Custom setting 4", GroupName = SystemTabNames.Content)]
    public virtual string Setting4 { get; set; }

    [Display(Name = "Custom setting 5", GroupName = SystemTabNames.Content)]
    public virtual string Setting5 { get; set; }
}
