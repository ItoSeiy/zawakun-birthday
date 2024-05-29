using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Sheet;

namespace Project.Framework.OutGame
{
    public abstract class Sheet<TRootView, TViewState> : Sheet
        where TRootView : AppView<TViewState>
        where TViewState : AppViewState
    {
        [SerializeField]
        private TRootView _rootView;

        [SerializeField]
        private bool _usePrefabNameAsIdentifier;

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

        public override Task Initialize()
        {
            Identifier = _usePrefabNameAsIdentifier ? gameObject.name.Replace("(Clone)", string.Empty) : Identifier;
            return base.Initialize();
        }
    }
}