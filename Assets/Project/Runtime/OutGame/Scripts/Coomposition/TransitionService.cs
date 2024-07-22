using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Framework.OutGame;
using Project.Runtime.Const;
using Project.Runtime.OutGame.Model;
using Project.Runtime.OutGame.Presentation;
using Project.Runtime.OutGame.View;
using Project.Runtime.OutGame.Presentation;
using R3;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Sheet;

namespace Project.Runtime.OutGame.Composition
{
    public sealed partial class TransitionService : ITransitionService
    {
        private readonly SplashPagePresenterFactory _splashPagePresenterFactory;
        private readonly LoginPagePresenterFactory _loginPagePresenterFactory;

        public TransitionService(SplashPagePresenterFactory splashPagePresenterFactory,
            LoginPagePresenterFactory loginPagePresenterFactory)
        {
            _splashPagePresenterFactory = splashPagePresenterFactory;
            _loginPagePresenterFactory = loginPagePresenterFactory;
        }

        private static SheetContainer RootSheetContainer => SheetContainer.Find("RootSheetContainer");
        private static PageContainer RootPageContainer => PageContainer.Find("RootPageContainer");
        private static ModalContainer MainModalContainer => ModalContainer.Find("MainModalContainer");

        /// <summary>
        ///     アプリ開始時
        /// </summary>
        public void PushSplashPage()
        {
            RootPageContainer.Push<SplashPage>(ResourceKeys.Prefabs.UI.GetPageKey<SplashPage>(), true,
                onLoad:
                x =>
                {
                    var page = x.page;
                    OnPagePresenterCreated(_splashPagePresenterFactory.Create(page, this), page);
                });
        }

        public void PushLoginPage()
        {
            RootPageContainer.Push<PostLetterPage>(
                $"{ResourceKeys.Prefabs.UI.UIPrefix}login{ResourceKeys.Prefabs.UI.PageSuffix}", true,
                onLoad: x =>
                {
                    var page = x.page;
                    OnPagePresenterCreated(_loginPagePresenterFactory.Create(page, this), page);
                });
        }


        /// <summary>
        ///     モーダル、ページのPop時 (モーダルを閉じる)
        /// </summary>
        public void PopCommandExecuted()
        {
            if (MainModalContainer.IsInTransition || RootPageContainer.IsInTransition)
            {
                throw new InvalidOperationException(
                    $"モーダルまたはページが遷移中だったため、閉じることができませんでした。{Environment.NewLine}" +
                    $"遷移中のコンテナ: Modal: {MainModalContainer.IsInTransition}, Page: {RootPageContainer.IsInTransition}");
            }

            if (MainModalContainer.Modals.Count >= 1)
            {
                MainModalContainer.Pop(true);
            }
            else if (RootPageContainer.Pages.Count >= 1)
            {
                RootPageContainer.Pop(true);
            }
            else
            {
                throw new InvalidOperationException("モーダルまたはページが存在しなかったため、閉じることができませんでした。");
            }
        }

        /// <summary>
        ///     全てのシートをロードする
        /// </summary>
        public async UniTask RegisterAllSheets(CancellationToken ct)
        {
            await UniTask.CompletedTask;
        }

        private ISheetPresenter OnSheetPresenterCreated(ISheetPresenter presenter, Sheet sheet,
            bool shouldInitialize = true)
        {
            if (shouldInitialize)
            {
                ((IPresenter)presenter).Initialize();
                presenter.AddTo(sheet);
            }

            return presenter;
        }

        /// <summary>
        ///     ページのプレゼンターの初期化
        /// </summary>
        private static IPagePresenter OnPagePresenterCreated(IPagePresenter presenter, Page page,
            bool shouldInitialize = true)
        {
            if (shouldInitialize)
            {
                ((IPresenter)presenter).Initialize();
                presenter.AddTo(page);
            }

            return presenter;
        }

        /// <summary>
        ///     モーダルのプレゼンターの初期化
        /// </summary>
        private static IModalPresenter OnModalPresenterCreated(IModalPresenter presenter, Modal modal,
            bool shouldInitialize = true)
        {
            if (shouldInitialize)
            {
                ((IPresenter)presenter).Initialize();
                presenter.AddTo(modal);
            }

            return presenter;
        }
    }
}