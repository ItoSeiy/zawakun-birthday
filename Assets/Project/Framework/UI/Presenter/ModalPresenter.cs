using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace Project.Framework.OutGame
{
    public abstract class ModalPresenter<TModal, TRootView, TRootViewState> : ModalPresenter<TModal>,
        IDisposableCollectionHolder
        where TModal : Modal<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState
    {
        private readonly List<IDisposable> _disposables = new();

        private TRootViewState _rootViewState;

        protected ModalPresenter(TModal modal) : base(modal)
        {
        }

        protected virtual ViewInitializationTiming RootViewInitializationTiming =>
            ViewInitializationTiming.BeforeFirstEnter;

        ICollection<IDisposable> IDisposableCollectionHolder.GetDisposableCollection()
        {
            return _disposables;
        }

        protected sealed override void Initialize(TModal modal)
        {
            base.Initialize(modal);

            async Task InitializeRootViewAsync()
            {
                if (modal.IsRootViewInitialized)
                {
                    return;
                }

                var state = await modal.InitializeRootViewAsync();
                _rootViewState = state;
                await ViewDidSetup(state);
            }

            var lifecycleEvent = RootViewInitializationTiming switch
            {
                ViewInitializationTiming.Initialize =>
                    new AnonymousModalLifecycleEvent(InitializeRootViewAsync),
                ViewInitializationTiming.BeforeFirstEnter =>
                    new AnonymousModalLifecycleEvent(onWillPushEnter: InitializeRootViewAsync),
                _ => throw new ArgumentOutOfRangeException()
            };
            modal.AddLifecycleEvent(lifecycleEvent, -1);
        }

        protected abstract UniTask ViewDidSetup(TRootViewState state);

        protected sealed override async Task ViewDidLoad(TModal modal)
        {
            await base.ViewDidLoad(modal);
            await ModalDidLoad(modal, _rootViewState);
        }

        protected sealed override async Task ViewWillPushEnter(TModal modal)
        {
            await base.ViewWillPushEnter(modal);
            await ModalWillPushEnter(modal, _rootViewState);
        }

        protected sealed override void ViewDidPushEnter(TModal modal)
        {
            base.ViewDidPushEnter(modal);
            ModalDidPushEnter(modal, _rootViewState);
        }

        protected sealed override async Task ViewWillPushExit(TModal modal)
        {
            await base.ViewWillPushExit(modal);
            await ModalWillPushExit(modal, _rootViewState);
        }

        protected sealed override void ViewDidPushExit(TModal modal)
        {
            base.ViewDidPushExit(modal);
            ModalDidPushExit(modal, _rootViewState);
        }

        protected sealed override async Task ViewWillPopEnter(TModal modal)
        {
            await base.ViewWillPopEnter(modal);
            await ModalWillPopEnter(modal, _rootViewState);
        }

        protected sealed override void ViewDidPopEnter(TModal modal)
        {
            base.ViewDidPopEnter(modal);
            ModalDidPopEnter(modal, _rootViewState);
        }

        protected sealed override async Task ViewWillPopExit(TModal modal)
        {
            await base.ViewWillPopExit(modal);
            await ModalWillPopExit(modal, _rootViewState);
        }

        protected sealed override void ViewDidPopExit(TModal modal)
        {
            base.ViewDidPopExit(modal);
            ModalDidPopExit(modal, _rootViewState);
        }

        protected sealed override async Task ViewWillDestroy(TModal modal)
        {
            await base.ViewWillDestroy(modal);
            await ModalWillDestroy(modal, _rootViewState);
        }

        protected virtual Task ModalDidLoad(TModal view, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual Task ModalWillPushEnter(TModal modal, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual void ModalDidPushEnter(TModal modal, TRootViewState rootViewState)
        {
        }

        protected virtual Task ModalWillPushExit(TModal modal, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual void ModalDidPushExit(TModal modal, TRootViewState rootViewState)
        {
        }

        protected virtual Task ModalWillPopEnter(TModal modal, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual void ModalDidPopEnter(TModal modal, TRootViewState rootViewState)
        {
        }

        protected virtual Task ModalWillPopExit(TModal modal, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected virtual void ModalDidPopExit(TModal modal, TRootViewState rootViewState)
        {
        }

        protected virtual Task ModalWillDestroy(TModal modal, TRootViewState rootViewState)
        {
            return Task.CompletedTask;
        }

        protected sealed override void Dispose(TModal modal)
        {
            base.Dispose(modal);
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}