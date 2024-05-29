using System.Threading;
using Cysharp.Threading.Tasks;

namespace Project.Runtime.OutGame.Presentation
{
    public partial interface ITransitionService
    {
        UniTask PreloadSystemModals(CancellationToken ct);
    }
}