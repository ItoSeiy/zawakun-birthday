using Project.Runtime.OutGame.Presentation;
using Project.Runtime.OutGame.View;

namespace Project.Runtime.OutGame.Presentation
{
    public sealed class SplashPagePresenterFactory
    {

        public SplashPagePresenter Create(SplashPage view, ITransitionService transitionService)
        {
            return new SplashPagePresenter(view, transitionService);
        }
    }
}