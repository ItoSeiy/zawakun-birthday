using Cysharp.Threading.Tasks;
using Project.Runtime.OutGame.Presentation;
using Project.Runtime.OutGame.View;

namespace Project.Runtime.OutGame.Presentation
{
    /// <summary>
    ///     スプラッシュのPresenter
    /// </summary>
    public sealed class SplashPagePresenter : PagePresenterBase<SplashPage, SplashView, SplashViewState>
    {

        private bool _didPush;

        public SplashPagePresenter(SplashPage view, ITransitionService transitionService) : base(view, transitionService)
        {
        }

        protected override UniTask ViewDidSetup(SplashViewState state)
        {
            Load().Forget();
            return UniTask.CompletedTask;
        }

        protected override void PageDidPushEnter(SplashPage page, SplashViewState rootViewState)
        {
            base.PageDidPushEnter(page, rootViewState);
            _didPush = true;
        }

        private async UniTaskVoid Load()
        {
            await (TransitionService.RegisterAllSheets(CancellationTokenSource.Token), UniTask.WaitUntil(() => _didPush));
            
            // ここで次の画面に遷移する
        }
    }
}