using System.ComponentModel.DataAnnotations;

namespace OptiAlloy.Models.Blocks;

[SiteContentType(
    GUID = "8753E580-10CF-45FC-9653-99C946113D88",
    GroupName = SystemTabNames.Content)]
[SiteImageUrl]
public class HeroBlock : SiteBlockData
{
    [Display(GroupName = Globals.GroupNames.Content)]
    public virtual SiteLogotypeBlock SiteLogotype { get; set; }

    [Display(GroupName = SystemTabNames.Content)]
    [CultureSpecific]
    public virtual string Description { get; set; }
}
