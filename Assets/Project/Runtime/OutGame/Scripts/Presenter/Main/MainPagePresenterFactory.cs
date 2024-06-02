using Project.Runtime.OutGame.View;

namespace Project.Runtime.OutGame.Presentation
{
    public sealed class MainPagePresenterFactory
    {
        public MainPagePresenter Create(MainPage view, ITransitionService transitionService)
        {
            return new MainPagePresenter(view, transitionService);
        }
    }
}