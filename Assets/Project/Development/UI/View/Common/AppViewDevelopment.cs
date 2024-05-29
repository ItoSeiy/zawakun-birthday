using Project.Framework.OutGame;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Project.Development.OutGame
{
    public abstract class AppViewDevelopment<TView, TState> : MonoBehaviour
        where TView : IAppView<TState>
        where TState : AppViewState
    {
        [SerializeField]
        private TView _view;

        [SerializeField]
        private GameObject _loadingCover;

        protected abstract bool UseLocalization { get; }

        public TView View
        {
            get => _view;
            set => _view = value;
        }

        public GameObject LoadingCover
        {
            get => _loadingCover;
            set => _loadingCover = value;
        }

        private async void Start()
        {
            if (_loadingCover != null)
            {
                _loadingCover.SetActive(true);
            }

            if (UseLocalization)
            {
                await LocalizationSettings.InitializationOperation.Task;
                StringTableDevelopment.CacheTable();
            }

            InitializeView(_view);
            var state = await _view.SetupAsync();
            ViewDidSetup(state);

            if (_loadingCover != null)
            {
                _loadingCover.SetActive(false);
            }
        }

        protected abstract void InitializeView(TView view);

        protected abstract void ViewDidSetup(TState state);
    }
}