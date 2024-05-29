using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Page;

namespace Project.Framework.OutGame
{
    public abstract class PagePresenter<TPage, TRootView, TRootViewState> : PagePresenter<TPage>,
        IDisposableCollectionHolder
        where TPage : Page<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new()
    {
        private readonly List<IDisposable> _disposables = new();
        private TRootViewState _rootViewState;

        protected PagePresenter(TPage view) : base(view)
        {
        }

        protected virtual ViewInitializationTiming RootViewInitializationTiming =>
            ViewInitializationTiming.BeforeFirstEnter;

        ICollection<IDisposable> IDisposableCollectionHolder.GetDisposableCollection()
        {
            return _disposables;
        }

        protected sealed override void Initialize(TPage page)
        {
            base.Initialize(page);

            async Task InitializeRootViewAsync()
            {
                if (page.IsRootViewInitialized)
                {
                    return;
                }

                var state = await page.InitializeRootViewAsync();
                _rootViewState = state;
                await ViewDidSetup(state);
            }

            var lifecycleEvent = RootViewInitializationTiming switch
            {
                ViewInitializationTiming.Initialize =>
                    new AnonymousPageLifecycleEvent(InitializeRootViewAsync),
                ViewInitializationTiming.BeforeFirstEnter =>
                    new AnonymousPageLifecycleEvent(onWillPushEnter: InitializeRootViewAsync),
                _ => throw new ArgumentOutOfRangeException()
            };
            page.AddLifecycleEvent(lifecycleEvent, -1);
        }

        protected abstract UniTask ViewDidSetup(TRootViewState state);

        protected sealed override async Task ViewDidLoad(TPage page)
        {
            await base.ViewDidLoad(page);
            await PageDidLoad(page, _rootViewState);
        }

        protected sealed override async Task ViewWillPushEnter(TPage page)
        {
            await base.ViewWillPushEnter(page);
            await PageWillPushEnter(page, _rootViewState);
        }

        protected sealed override void ViewDidPushEnter(TPage page)
        {
            base.ViewDidPushEnter(page);
            PageDidPushEnter(page, _rootViewState);
        }

        protected sealed override async Task ViewWillPushExit(TPage page)
        {
            await base.ViewWillPushExit(page);
            await PageWillPushExit(page, _rootViewState);
        }

        protected sealed override void ViewDidPushExit(TPage page)
        {
            base.ViewDidPushExit(page);
            PageDidPushExit(page, _rootViewState);
        }

        protected sealed override async Task ViewWillPopEnter(TPage page)
        {
            await base.ViewWillPopEnter(page);
            await PageWillPopEnter(page, _rootViewState);
        }

        protected sealed override void ViewDidPopEnter(TPage page)
        {
            base.ViewDidPopEnter(page);
            PageDidPopEnter(page, _rootViewState);
        }

        protected sealed override async Task ViewWillPopExit(TPage page)
        {
            await base.ViewWillPopExit(page);
            await PageWillPopExit(page, _rootViewState);
        }

        protected sealed override void ViewDidPopExit(TPage view)
        {
            base.ViewDidPopExit(view);
            PageDidPopExit(view, _rootViewState);
        }

        protected sealed override async Task ViewWillDestroy(TPage page)
        {
            await base.ViewWillDestroy(page);
            await PageWillDestroy(page, _rootViewState);
        }

        protected virtual Task PageDidLoad(TPage page, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual Task PageWillPushEnter(TPage page, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual void PageDidPushEnter(TPage page, TRootViewState rootViewState)
        {
        }

        protected virtual Task PageWillPushExit(TPage page, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual void PageDidPushExit(TPage page, TRootViewState rootViewState)
        {
        }

        protected virtual Task PageWillPopEnter(TPage page, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual void PageDidPopEnter(TPage page, TRootViewState rootViewState)
        {
        }

        protected virtual Task PageWillPopExit(TPage page, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual void PageDidPopExit(TPage page, TRootViewState rootViewState)
        {
        }

        protected virtual Task PageWillDestroy(TPage page, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected sealed override void Dispose(TPage page)
        {
            base.Dispose(page);
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}