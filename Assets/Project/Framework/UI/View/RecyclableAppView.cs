using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Framework.OutGame
{
    public abstract class RecyclableAppView<TState> : MonoBehaviour, IAppView<TState>
        where TState : AppViewState
    {
        protected virtual void OnDestroy()
        {
            Cleanup();
        }

        public bool IsSetup { get; private set; }

        public async UniTask<TState> SetupAsync()
        {
            if (IsSetup)
            {
                Cleanup();
            }

            IsSetup = true;
            return await Setup();
        }

        protected abstract UniTask<TState> Setup();

        protected abstract void Cleanup();
    }
}