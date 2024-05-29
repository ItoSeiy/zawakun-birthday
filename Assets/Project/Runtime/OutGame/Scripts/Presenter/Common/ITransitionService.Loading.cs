using System.Threading;
using Cysharp.Threading.Tasks;

namespace Project.Runtime.OutGame.Presentation
{
    public partial interface ITransitionService
    {
        UniTask RegisterLoadLoadingUIs(CancellationToken ct);

        UniTask ShowLoadingUI(CancellationToken ct);

        UniTask ShowLoadingIconUI(CancellationToken ct);

        UniTask HideLoadingUI(CancellationToken ct);
    }
}