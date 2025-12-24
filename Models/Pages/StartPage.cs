using System.ComponentModel.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace OptiAlloy.Models.Pages;

/// <summary>
/// Used for the site's start page and also acts as a container for site settings
/// </summary>
[ContentType(
    GUID = "19671657-B684-4D95-A61F-8DD4FE60D559",
    GroupName = Globals.GroupNames.Specialized)]
[SiteImageUrl]
[AvailableContentTypes(
    Availability.Specific,
    Include =
    [
        typeof(ContainerPage),
        typeof(ProductPage),
        typeof(StandardPage),
        typeof(ISearchPage),
        typeof(LandingPage),
        typeof(BlankExperience),
    ], // Pages we can create under the start page...
    ExcludeOn =
    [
        typeof(ContainerPage),
        typeof(ProductPage),
        typeof(StandardPage),
        typeof(ISearchPage),
        typeof(LandingPage)
    ])] // ...and underneath those we can't create additional start pages
public class StartPage : SitePageData, IContentPage, IHeroPage
{
    [Display(
        GroupName = Globals.GroupNames.Hero)]
    public virtual JumbotronBlock Hero { get; set; }

    [Display(
        GroupName = SystemTabNames.Content,
        Order = 320)]
    [CultureSpecific]
    public virtual ContentArea MainContentArea { get; set; }

    [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 300)]
    public virtual LinkItemCollection ProductPageLinks { get; set; }

    [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 350)]
    public virtual LinkItemCollection CompanyInformationPageLinks { get; set; }

    [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 400)]
    public virtual LinkItemCollection NewsPageLinks { get; set; }

    [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 450)]
    public virtual LinkItemCollection CustomerZonePageLinks { get; set; }

    [Display(GroupName = Globals.GroupNames.SiteSettings)]
    [AllowedTypes(AllowedTypes = [typeof(PageData)])]
    public virtual ContentReference GlobalNewsPageLink { get; set; }

    [Display(GroupName = Globals.GroupNames.SiteSettings)]
    [AllowedTypes(AllowedTypes = [typeof(PageData)])]
    public virtual ContentReference ContactsPageLink { get; set; }

    [Display(GroupName = Globals.GroupNames.SiteSettings)]
    [AllowedTypes(AllowedTypes = [typeof(PageData)])]
    public virtual ContentReference SearchPageLink { get; set; }

    [Display(GroupName = Globals.GroupNames.SiteSettings)]
    public virtual SiteLogotypeBlock SiteLogotype { get; set; }

    /// <summary>
    /// Allow to test scenario when Content Manager is turned on/off at runtime
    /// </summary>
    [Display(GroupName = Globals.GroupNames.FeatureFlags, Name = "Is Content ManagerEnabled Enabled")]
    public virtual bool? IsContentManagerEnabled { get; set; }

    [Display(GroupName = Globals.GroupNames.FeatureFlags, Name = "Is Content ManagerLiteEnabled Enabled")]
    public virtual bool? IsContentManagerLiteEnabled { get; set; }

    /// <summary>
    /// Allow to test scenario when Content Bindings is turned on/off at runtime
    /// </summary>
    [Display(GroupName = Globals.GroupNames.FeatureFlags, Name = "Is Content Bindings Enabled")]
    public virtual bool? IsContentBindingsEnabled { get; set; }
}
