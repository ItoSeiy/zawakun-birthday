using System.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace Project.Framework.OutGame
{
    public abstract class ModalPresenter<TModal> : ScreenPresenter<TModal>, IModalPresenter where TModal : Modal
    {
        protected ModalPresenter(TModal modal) : base(modal)
        {
            Modal = modal;
        }

        private TModal Modal { get; }

        Task IModalLifecycleEvent.Initialize()
        {
            return ViewDidLoad(Modal);
        }

        Task IModalLifecycleEvent.WillPushEnter()
        {
            return ViewWillPushEnter(Modal);
        }

        void IModalLifecycleEvent.DidPushEnter()
        {
            ViewDidPushEnter(Modal);
        }

        Task IModalLifecycleEvent.WillPushExit()
        {
            return ViewWillPushExit(Modal);
        }

        void IModalLifecycleEvent.DidPushExit()
        {
            ViewDidPushExit(Modal);
        }

        Task IModalLifecycleEvent.WillPopEnter()
        {
            return ViewWillPopEnter(Modal);
        }

        void IModalLifecycleEvent.DidPopEnter()
        {
            ViewDidPopEnter(Modal);
        }

        Task IModalLifecycleEvent.WillPopExit()
        {
            return ViewWillPopExit(Modal);
        }

        void IModalLifecycleEvent.DidPopExit()
        {
            ViewDidPopExit(Modal);
        }

        Task IModalLifecycleEvent.Cleanup()
        {
            return ViewWillDestroy(Modal);
        }

        protected virtual Task ViewDidLoad(TModal modal)
        {
            return Task.CompletedTask;
        }

        protected virtual Task ViewWillPushEnter(TModal modal)
        {
            return Task.CompletedTask;
        }

        protected virtual void ViewDidPushEnter(TModal modal)
        {
        }

        protected virtual Task ViewWillPushExit(TModal modal)
        {
            return Task.CompletedTask;
        }

        protected virtual void ViewDidPushExit(TModal modal)
        {
        }

        protected virtual Task ViewWillPopEnter(TModal modal)
        {
            return Task.CompletedTask;
        }

        protected virtual void ViewDidPopEnter(TModal modal)
        {
        }

        protected virtual Task ViewWillPopExit(TModal modal)
        {
            return Task.CompletedTask;
        }

        protected virtual void ViewDidPopExit(TModal modal)
        {
        }

        protected virtual Task ViewWillDestroy(TModal modal)
        {
            return Task.CompletedTask;
        }

        protected override void Initialize(TModal modal)
        {
            // The lifecycle event of the view will be added with priority 0.
            // Presenters should be processed after the view so set the priority to 1.
            modal.AddLifecycleEvent(this, 1);
        }

        protected override void Dispose(TModal modal)
        {
            modal.RemoveLifecycleEvent(this);
        }
    }
}