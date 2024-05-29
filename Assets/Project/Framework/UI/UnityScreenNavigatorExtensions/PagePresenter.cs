using System.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Page;

namespace Project.Framework.OutGame
{
    public abstract class PagePresenter<TPage> : ScreenPresenter<TPage>, IPagePresenter where TPage : Page
    {
        protected PagePresenter(TPage view) : base(view)
        {
            View = view;
        }

        private TPage View { get; }

        Task IPageLifecycleEvent.Initialize()
        {
            return ViewDidLoad(View);
        }

        Task IPageLifecycleEvent.WillPushEnter()
        {
            return ViewWillPushEnter(View);
        }

        void IPageLifecycleEvent.DidPushEnter()
        {
            ViewDidPushEnter(View);
        }

        Task IPageLifecycleEvent.WillPushExit()
        {
            return ViewWillPushExit(View);
        }

        void IPageLifecycleEvent.DidPushExit()
        {
            ViewDidPushExit(View);
        }

        Task IPageLifecycleEvent.WillPopEnter()
        {
            return ViewWillPopEnter(View);
        }

        void IPageLifecycleEvent.DidPopEnter()
        {
            ViewDidPopEnter(View);
        }

        Task IPageLifecycleEvent.WillPopExit()
        {
            return ViewWillPopExit(View);
        }

        void IPageLifecycleEvent.DidPopExit()
        {
            ViewDidPopExit(View);
        }

        Task IPageLifecycleEvent.Cleanup()
        {
            return ViewWillDestroy(View);
        }

        protected virtual Task ViewDidLoad(TPage view)
        {
            return Task.CompletedTask;
        }

        protected virtual Task ViewWillPushEnter(TPage view)
        {
            return Task.CompletedTask;
        }

        protected virtual void ViewDidPushEnter(TPage view)
        {
        }

        protected virtual Task ViewWillPushExit(TPage view)
        {
            return Task.CompletedTask;
        }

        protected virtual void ViewDidPushExit(TPage view)
        {
        }

        protected virtual Task ViewWillPopEnter(TPage view)
        {
            return Task.CompletedTask;
        }

        protected virtual void ViewDidPopEnter(TPage view)
        {
        }

        protected virtual Task ViewWillPopExit(TPage view)
        {
            return Task.CompletedTask;
        }

        protected virtual void ViewDidPopExit(TPage view)
        {
        }

        protected virtual Task ViewWillDestroy(TPage view)
        {
            return Task.CompletedTask;
        }

        protected override void Initialize(TPage view)
        {
            // The lifecycle event of the view will be added with priority 0.
            // Presenters should be processed after the view so set the priority to 1.
            view.AddLifecycleEvent(this, 1);
        }

        protected override void Dispose(TPage view)
        {
            view.RemoveLifecycleEvent(this);
        }
    }
}