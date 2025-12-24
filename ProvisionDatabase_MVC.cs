using EPiServer.Authorization;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Security;

namespace OptiAlloy;

/// <summary>
/// Provision the database for easier development by:
///  * Enabling project mode
///  * Adding some default users
///
/// This file is preferably deployed in the App_Code folder, where it will be picked up and executed automatically.
/// </summary>
[InitializableModule]
[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
public class ProvisionDatabase : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        var log = context.Services.GetInstance<ILogger<ProvisionDatabase>>();
        log.StartingToProvisionUsersAndGroups();

        AddUsersAndRoles(context.Services.GetInstance<IContentSecurityRepository>(), log);
    }

    public void Uninitialize(InitializationEngine context) { }

    private async void AddUsersAndRoles(IContentSecurityRepository securityRepository, ILogger<ProvisionDatabase> log)
    {
        const string password = "sparr0wHawk!";

        await AddRole(Roles.WebAdmins, AccessLevel.FullAccess, securityRepository, log);
        await AddRole(Roles.WebEditors, AccessLevel.FullAccess ^ AccessLevel.Administer, securityRepository, log);

        await AddUser("cmsadmin", password, new[] { Roles.WebEditors, Roles.WebAdmins }, log);
        await AddUser("abbie", password, new[] { Roles.WebEditors, Roles.WebAdmins }, log);
        await AddUser("eddie", password, new[] { Roles.WebEditors }, log);
        await AddUser("erin", password, new[] { Roles.WebEditors }, log);
        await AddUser("reid", password, new[] { Roles.WebEditors }, log);
    }

    private async Task AddUser(string userName, string passWord, string[] roleNames, ILogger<ProvisionDatabase> log)
    {
        log.AddingUser(userName);

        if (await UIUserProvider.GetUserAsync(userName) != null)
        {
            log.UserAlreadyExists(userName);
            return;
        }

        var email = $"epic-{userName}@mailinator.com";
        await UIUserProvider.CreateUserAsync(userName, passWord, email, null, null, true);
        await UIRoleProvider.AddUserToRolesAsync(userName, roleNames);
    }

    private async Task AddRole(string roleName, AccessLevel accessLevel, IContentSecurityRepository securityRepository,
        ILogger<ProvisionDatabase> log)
    {
        log.AddingRole(roleName);

        if (await UIRoleProvider.RoleExistsAsync(roleName))
        {
            log.RoleAlreadyExists(roleName);
            return;
        }

        await UIRoleProvider.CreateRoleAsync(roleName);

        var permissions =
            (IContentSecurityDescriptor)securityRepository.Get(ContentReference.RootPage).CreateWritableClone();
        permissions.AddEntry(new AccessControlEntry(roleName, accessLevel));

        securityRepository.Save(ContentReference.RootPage, permissions, SecuritySaveType.Replace);
        securityRepository.Save(ContentReference.WasteBasket, permissions, SecuritySaveType.Replace);
    }

    private UIUserProvider UIUserProvider => ServiceLocator.Current.GetInstance<UIUserProvider>();

    private UIRoleProvider UIRoleProvider => ServiceLocator.Current.GetInstance<UIRoleProvider>();
}

internal static partial class ProvisionDatabaseExtensions
{
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Starting to provision users and groups")]
    public static partial void StartingToProvisionUsersAndGroups(this ILogger<ProvisionDatabase> logger);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Adding user {userName}")]
    public static partial void AddingUser(this ILogger<ProvisionDatabase> logger, string userName);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "User {userName} already exists")]
    public static partial void UserAlreadyExists(this ILogger<ProvisionDatabase> logger, string userName);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Adding role {roleName}")]
    public static partial void AddingRole(this ILogger<ProvisionDatabase> logger, string roleName);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Role {roleName} already exists")]
    public static partial void RoleAlreadyExists(this ILogger<ProvisionDatabase> logger, string roleName);
}
