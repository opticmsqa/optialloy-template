#pragma warning disable IDE0005 // Using directive is unnecessary - included in multiple projects with different global usings.
using EPiServer.Authorization;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web;

namespace OptiAlloy.Business.Initialization;

#pragma warning disable IDE0060 // Remove unused parameter - disable due to CI issues

[ModuleDependency(typeof(InitializationModule))]
//TODO: just a temporary workaround to allow WebAdmins & WebEditors to save templates in OptiAlloy and in e2e test site
public class BlueprintsAccessRightsInitializationModule : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        var contentRootService = context.Services.GetInstance<ContentRootService>();
        var contentSecurityRepository = context.Services.GetInstance<IContentSecurityRepository>();
        var container = contentRootService.Get(SystemContentRootNames.Blueprints);

        AddRolePermissions(contentSecurityRepository, container, Roles.WebAdmins, AccessLevel.FullAccess);
        AddRolePermissions(contentSecurityRepository, container, Roles.WebEditors, AccessLevel.FullAccess ^ AccessLevel.Administer);
    }

    private void AddRolePermissions(IContentSecurityRepository contentSecurityRepository, ContentReference contentLink,
        string roleName, AccessLevel accessLevel)
    {
        var permissions = contentSecurityRepository.Get(contentLink);
        if (permissions is null || permissions.Entries.Any(x => x.Name == roleName))
        {
            return;
        }

        permissions = (IContentSecurityDescriptor)permissions.CreateWritableClone();

        permissions.AddEntry(new AccessControlEntry(roleName, accessLevel));

        contentSecurityRepository.Save(contentLink, permissions, SecuritySaveType.Replace);
    }

    public void Uninitialize(InitializationEngine context)
    {

    }
}
