using Cysharp.Threading.Tasks;
using Project.Framework.Extensions;
using Project.Framework.OutGame;
using R3;
using UnityEngine;

namespace Project.Runtime.OutGame.View
{
    public sealed class LoginView : AppView<LoginViewState>
    {
        [SerializeField]
        private SimpleButton _letter;

        [SerializeField]
        private SimpleButton _post;

        protected override UniTask<LoginViewState> Setup()
        {
            var state = new LoginViewState();
            var iViewState = (ILoginViewState)state;

            _letter.SetOnClickDestination(() => iViewState.InvokeLetterClicked());
            _post.SetOnClickDestination(() => iViewState.InvokePostClicked());

            state.IsLetterActive.Subscribe(SetLetterActive).AddTo(this);
            state.IsPostActive.Subscribe(SetPostActive).AddTo(this);

            return UniTask.FromResult(state);
        }

        private void SetLetterActive(bool active)
        {
            _letter.gameObject.SetActive(active);
        }

        private void SetPostActive(bool active)
        {
            _post.gameObject.SetActive(active);
        }
    }

    public sealed class LoginViewState : AppViewState, ILoginViewState
    {
        void ILoginViewState.InvokeLetterClicked()
        {
            _onLetterClicked.OnNext(Unit.Default);
        }

        void ILoginViewState.InvokePostClicked()
        {
            _onPostClicked.OnNext(Unit.Default);
        }

        public ReactiveProperty<bool> IsLetterActive { get; } = new();
        public ReactiveProperty<bool> IsPostActive { get; } = new();

        public Observable<Unit> OnLetterClicked => _onLetterClicked;
        private readonly Subject<Unit> _onLetterClicked = new();

        public Observable<Unit> OnPostClicked => _onPostClicked;
        private readonly Subject<Unit> _onPostClicked = new();
    }

    public interface ILoginViewState
    {
        void InvokeLetterClicked();
        void InvokePostClicked();
    }
}