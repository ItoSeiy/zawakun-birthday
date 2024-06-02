using System.IO;
using System.Net;
using Cysharp.Threading.Tasks;
using Project.Framework.OutGame;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.OutGame.View
{
    /// <summary>
    /// ゲーム進行のView
    /// </summary>
    public sealed class MainView : AppView<MainViewState>
    {
        protected override UniTask<MainViewState> Setup()
        {

            var state = new MainViewState();
            return UniTask.FromResult(state);
        }
    }

    public sealed class MainViewState : AppViewState
    {
    }
}