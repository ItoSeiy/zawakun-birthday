using System;
using System.Threading;
using Addler.Runtime.Core.LifetimeBinding;
using Cysharp.Threading.Tasks;

namespace Project.Framework.Utils
{
    public sealed class CancellationTokenSourceReleaseEvent : IReleaseEvent
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        public CancellationTokenSourceReleaseEvent(CancellationTokenSource cts)
        {
            _cancellationTokenSource = cts;
            WaitUntilCanceled().Forget();
        }

        event Action IReleaseEvent.Dispatched
        {
            add => ReleasedInternal += value;
            remove => ReleasedInternal -= value;
        }

        private async UniTaskVoid WaitUntilCanceled()
        {
            await _cancellationTokenSource.Token.WaitUntilCanceled();
            ReleasedInternal?.Invoke();
        }

        private event Action ReleasedInternal;
    }
}