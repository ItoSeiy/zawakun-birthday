using Project.Runtime.OutGame.View;

namespace Project.Runtime.OutGame.Presentation
{
    public sealed class IntroPagePresenterFactory
    {
        public IntroPagePresenter Create(IntroPage view, ITransitionService transitionService)
        {
            return new IntroPagePresenter(view, transitionService);
        }
    }
}