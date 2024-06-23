using System.IO;
using System.Net;
using Cysharp.Threading.Tasks;
using Project.Framework.Extensions;
using Project.Framework.OutGame;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.OutGame.View
{
    /// <summary>
    /// ゲーム進行のView
    /// </summary>
    public sealed class LoginView : AppView<LoginViewState>
    {
        [SerializeField]
        private SimpleButton[] _stageButtonArray;
        
        protected override UniTask<LoginViewState> Setup()
        {
            var state = new LoginViewState();

            for (var i = 0; i < _stageButtonArray.Length; i++)
            {
                var index = i;
                _stageButtonArray[i].SetOnClickDestination(() => state.InvokeStageButtonClicked(index));
            }

            return UniTask.FromResult(state);
        }
    }
    
    public sealed class LoginViewState : AppViewState, ILoginViewState
    {
        public void InvokeStageButtonClicked(int index)
        {
            _onStageButtonClicked.OnNext(index);
        }

        public Observable<int> OnStageButtonClicked => _onStageButtonClicked;

        private readonly Subject<int> _onStageButtonClicked = new();
    }

    public interface ILoginViewState
    {
        public void InvokeStageButtonClicked(int index);
    }
}