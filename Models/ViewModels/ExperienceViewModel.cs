using EPiServer.VisualBuilder.Compositions;

namespace OptiAlloy.Models.ViewModels;

public class ExperienceViewModel<T> : PageViewModel<T> where T : PageData
{
    public ExperienceViewModel(T currentPage, Composition composition) : base(currentPage)
    {
        Composition = composition;
    }

    public Composition Composition { get; }
}

public class ExperienceViewModel
{
    public static ExperienceViewModel<T> Create<T>(T currentPage, Composition composition)
        where T : PageData => new(currentPage, composition);
}
