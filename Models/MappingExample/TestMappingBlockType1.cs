using System.ComponentModel.DataAnnotations;
using OptiAlloy.Models.Elements;

namespace OptiAlloy.Models.MappingExample;

[SiteContentType(GUID = "9E672C6F-28F5-4358-B987-4858D20ADF98", GroupName = SystemTabNames.Settings)]
[SiteImageUrl]
public class TestMappingBlockType1 : BlockData
{
    [Display(GroupName = SystemTabNames.Content)]
    public virtual string Title { get; set; }

    [Display(Name = "Main body", GroupName = SystemTabNames.Content)]
    [CultureSpecific]
    public virtual XhtmlString MainBody { get; set; }

    [Display(Name = "Related content", GroupName = SystemTabNames.Content)]
    public virtual CTAElement RelatedContent { get; set; }

    [Display(GroupName = SystemTabNames.Content)]
    public virtual SiteLogotypeBlock SiteLogotype { get; set; }

    [Display(GroupName = SystemTabNames.Content)]
    public virtual EditorialBlock Editorial1 { get; set; }
}
