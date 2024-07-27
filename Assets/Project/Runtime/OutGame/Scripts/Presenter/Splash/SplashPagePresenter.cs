using Cysharp.Threading.Tasks;
using Project.Framework.Utils;
using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;
using UnityEngine;

namespace Project.Runtime.OutGame.Presentation
{
    /// <summary>
    ///     スプラッシュのPresenter
    /// </summary>
    public sealed class SplashPagePresenter : PagePresenterBase<SplashPage, SplashView, SplashViewState>
    {
        private bool _didPush;

        public SplashPagePresenter(SplashPage view, ITransitionService transitionService) : base(view,
            transitionService)
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
            await (TransitionService.RegisterAllSheets(CancellationTokenSource.Token),
                UniTask.WaitUntil(() => _didPush),
                UniTask.WaitForSeconds(1.5f));

            var loggedInInt = PlayerPrefs.GetInt(PlayerPrefsConst.Bool.IsLoggedIn, 0);
            var isLoggedIn = loggedInInt == 1;

            if (isLoggedIn == false)
            {
                TransitionService.PushIntroPage();
            }
            else
            {
                TransitionService.PushQuestionPage();
            }
        }
    }
}