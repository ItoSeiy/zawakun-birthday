using Cysharp.Threading.Tasks;
using Project.Framework.Extensions;
using Project.Framework.OutGame;
using R3;
using UnityEngine;

namespace Project.Runtime.OutGame.View
{
    public sealed class PostLetterView : AppView<PostLetterViewState>
    {
        [SerializeField]
        private SimpleButton _post;

        [SerializeField]
        private SimpleButton _letter;

        [SerializeField]
        private AudioSource _seAudioSource;

        [SerializeField]
        private AudioClip _postAudioClip;

        [SerializeField]
        private AudioClip _letterAudioClip;


        protected override UniTask<PostLetterViewState> Setup()
        {
            var state = new PostLetterViewState();
            var iViewState = (ILoginViewState)state;

            _post.SetOnClickDestination(() => iViewState.InvokePostClicked());
            _letter.SetOnClickDestination(() =>
            {
                _seAudioSource.PlayOneShot(_letterAudioClip);
                iViewState.InvokeLetterClicked();
            });

            state.IsPostActive.Subscribe(SetPostActive).AddTo(this);
            state.IsLetterActive.Subscribe(SetLetterActive).AddTo(this);

            return UniTask.FromResult(state);
        }

        private void SetPostActive(bool active)
        {
            _post.gameObject.SetActive(active);
        }

        private void SetLetterActive(bool active)
        {
            if (active)
            {
                _seAudioSource.PlayOneShot(_postAudioClip);
            }

            _letter.gameObject.SetActive(active);
        }
    }

    public sealed class PostLetterViewState : AppViewState, ILoginViewState
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