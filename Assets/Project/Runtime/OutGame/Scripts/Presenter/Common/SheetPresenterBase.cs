using Project.Framework.OutGame;

namespace Project.Runtime.OutGame.Presentation
{
    public abstract class SheetPresenterBase<TSheet, TRootView, TRootViewState>
        : SheetPresenter<TSheet, TRootView, TRootViewState>
        where TSheet : Sheet<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState
    {
        protected SheetPresenterBase(TSheet view, ITransitionService transitionService) : base(view)
        {
            TransitionService = transitionService;
        }

        protected ITransitionService TransitionService { get; }
    }
}