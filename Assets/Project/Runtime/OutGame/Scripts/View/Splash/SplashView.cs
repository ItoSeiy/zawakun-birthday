using Cysharp.Threading.Tasks;
using Project.Framework.OutGame;

namespace Project.Runtime.OutGame.View
{
    /// <summary>
    ///     スプラッシュのView
    /// </summary>
    public sealed class SplashView : AppView<SplashViewState>
    {
        protected override UniTask<SplashViewState> Setup()
        {
            var state = new SplashViewState();
            return UniTask.FromResult(state);
        }
    }

    public sealed class SplashViewState : AppViewState
    {
    }
}