using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;

namespace Project.Framework.OutGame
{
    public abstract class Page<TRootView, TViewState> : Page
        where TRootView : AppView<TViewState>
        where TViewState : AppViewState
    {
        [SerializeField]
        private TRootView _rootView;

        public bool IsRootViewInitialized { get; private set; }

        public async UniTask<TViewState> InitializeRootViewAsync()
        {
            if (IsRootViewInitialized)
            {
                throw new InvalidOperationException("RootView is already initialized.");
            }

            IsRootViewInitialized = true;
            return await _rootView.SetupAsync();
        }
    }
}