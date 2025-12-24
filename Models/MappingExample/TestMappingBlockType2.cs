using System.ComponentModel.DataAnnotations;

namespace OptiAlloy.Models.MappingExample;

[SiteContentType(GUID = "CF1EAC70-F5DE-41DB-8274-B7066F2C0A46", GroupName = SystemTabNames.Settings)]
[SiteImageUrl]
public class TestMappingBlockType2 : BlockData
{
    [Display(GroupName = SystemTabNames.Content)]
    public virtual string ArticleTitle { get; set; }

    [Display(GroupName = SystemTabNames.Content)]
    [CultureSpecific]
    public virtual XhtmlString MainBody { get; set; }

    [Display(GroupName = SystemTabNames.Content)]
    public virtual string Text1 { get; set; }

    [Display(Name = "Boolean property 1", GroupName = SystemTabNames.Content)]
    public virtual bool Boolean1 { get; set; }

    [Display(GroupName = SystemTabNames.Content)]
    public virtual ContentReference Link1 { get; set; }

    [Display(GroupName = SystemTabNames.Content)]
    public virtual SiteLogotypeBlock ContentLogo { get; set; }

    [Display(GroupName = SystemTabNames.Content)]
    public virtual EditorialBlock NestedHtmlProperty { get; set; }
}
