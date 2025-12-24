using System.Runtime.CompilerServices;
using EPiServer.Shell.Navigation;

namespace OptiAlloy;

[MenuProvider]
public class GoogleMenuProvider : IMenuProvider
{
    private const string FakeMenuPath = MenuPaths.Global + "/cms/googlecom";

    public async IAsyncEnumerable<MenuItem> GetMenuItemsAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        yield return await Task.FromResult(new UrlMenuItem("google.com", FakeMenuPath, "https://google.com")
        {
            SortIndex = 100,
            IsAvailable = _ => true
        });
    }
}
