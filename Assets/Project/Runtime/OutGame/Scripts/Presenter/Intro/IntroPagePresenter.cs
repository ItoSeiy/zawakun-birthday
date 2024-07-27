using Cysharp.Threading.Tasks;
using Project.Framework.OutGame;
using Project.Runtime.OutGame.View;
using R3;

namespace Project.Runtime.OutGame.Presentation
{
    /// <summary>
    /// 導入のPresenter
    /// </summary>
    public sealed class IntroPagePresenter : PagePresenterBase<IntroPage, IntroView, IntroViewState>
    {
        public IntroPagePresenter(IntroPage view, ITransitionService transitionService) : base(view, transitionService)
        {
        }

        protected override UniTask ViewDidSetup(IntroViewState state)
        {
            state.OnTextFinished.Subscribe(_ => TransitionService.PushLoginPage()).AddTo(this);
            return UniTask.FromResult(state);
        }
    }
}