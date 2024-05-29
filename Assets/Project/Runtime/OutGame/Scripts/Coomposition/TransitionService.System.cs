using System.Threading;
using Cysharp.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace Project.Runtime.OutGame.Composition
{
    public sealed partial class TransitionService
    {
        private static ModalContainer SystemModalContainer => ModalContainer.Find("SystemModalContainer");

        public UniTask PreloadSystemModals(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
    }
}