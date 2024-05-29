using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Framework.OutGame
{
    public abstract class AppView<TState> : MonoBehaviour, IAppView<TState>
        where TState : AppViewState
    {
        public bool IsSetup { get; private set; }

        public async UniTask<TState> SetupAsync()
        {
            if (IsSetup)
            {
                throw new InvalidOperationException($"{GetType().Name} is already initialized.");
            }

            IsSetup = true;
            return await Setup();
        }

        protected abstract UniTask<TState> Setup();
    }
}