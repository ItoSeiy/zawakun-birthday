using Cysharp.Threading.Tasks;

namespace Project.Framework.OutGame
{
    public interface IAppView<TState> where TState : AppViewState
    {
        bool IsSetup { get; }

        UniTask<TState> SetupAsync();
    }
}