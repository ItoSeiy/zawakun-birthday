using Cysharp.Threading.Tasks;
using Project.Runtime.OutGame.View;

namespace Project.Runtime.OutGame.Presentation
{
    /// <summary>
    /// ゲーム進行のPresenter
    /// </summary>
    public sealed class MainPagePresenter : PagePresenterBase<MainPage, MainView, MainViewState>
    {
        public MainPagePresenter(MainPage view, ITransitionService transitionService) : base(view, transitionService)
        {
        }

        protected override UniTask ViewDidSetup(MainViewState state)
        {
            return UniTask.FromResult(state);
        }
    }
}
