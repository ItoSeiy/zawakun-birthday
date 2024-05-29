using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Sheet;

namespace Project.Framework.OutGame
{
    public abstract class SheetPresenter<TSheet, TRootView, TRootViewState> : SheetPresenter<TSheet>,
        IDisposableCollectionHolder
        where TSheet : Sheet<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState
    {
        private readonly List<IDisposable> _disposables = new();

        private TRootViewState _rootViewState;

        protected SheetPresenter(TSheet sheet) : base(sheet)
        {
        }

        protected virtual ViewInitializationTiming RootViewInitializationTiming =>
            ViewInitializationTiming.BeforeFirstEnter;

        ICollection<IDisposable> IDisposableCollectionHolder.GetDisposableCollection()
        {
            return _disposables;
        }

        protected sealed override void Initialize(TSheet sheet)
        {
            base.Initialize(sheet);

            async Task InitializeRootViewAsync()
            {
                if (sheet.IsRootViewInitialized)
                {
                    return;
                }

                var state = await sheet.InitializeRootViewAsync();
                _rootViewState = state;
                await ViewDidSetup(state);
            }

            var lifecycleEvent = RootViewInitializationTiming switch
            {
                ViewInitializationTiming.Initialize =>
                    new AnonymousSheetLifecycleEvent(InitializeRootViewAsync),
                ViewInitializationTiming.BeforeFirstEnter =>
                    new AnonymousSheetLifecycleEvent(onWillEnter: InitializeRootViewAsync),
                _ => throw new ArgumentOutOfRangeException()
            };
            sheet.AddLifecycleEvent(lifecycleEvent, -1);
        }

        protected abstract UniTask ViewDidSetup(TRootViewState state);

        protected sealed override async Task ViewDidLoad(TSheet sheet)
        {
            await base.ViewDidLoad(sheet);
            await SheetDidLoad(sheet, _rootViewState);
        }

        protected sealed override async Task ViewWillEnter(TSheet sheet)
        {
            await base.ViewWillEnter(sheet);
            await SheetWillEnter(sheet, _rootViewState);
        }

        protected sealed override void ViewDidEnter(TSheet sheet)
        {
            base.ViewDidEnter(sheet);
            SheetDidEnter(sheet, _rootViewState);
        }

        protected sealed override async Task ViewWillExit(TSheet sheet)
        {
            await base.ViewWillExit(sheet);
            await SheetWillExit(sheet, _rootViewState);
        }

        protected sealed override void ViewDidExit(TSheet sheet)
        {
            base.ViewDidExit(sheet);
            SheetDidExit(sheet, _rootViewState);
        }

        protected sealed override async Task ViewWillDestroy(TSheet sheet)
        {
            await base.ViewWillDestroy(sheet);
            await SheetWillDestroy(sheet, _rootViewState);
        }

        protected virtual Task SheetDidLoad(TSheet sheet, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual Task SheetWillEnter(TSheet sheet, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual void SheetDidEnter(TSheet sheet, TRootViewState rootViewState)
        {
        }

        protected virtual Task SheetWillExit(TSheet sheet, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual void SheetDidExit(TSheet sheet, TRootViewState rootViewState)
        {
        }

        protected virtual Task SheetWillDestroy(TSheet sheet, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected sealed override void Dispose(TSheet sheet)
        {
            base.Dispose(sheet);
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}