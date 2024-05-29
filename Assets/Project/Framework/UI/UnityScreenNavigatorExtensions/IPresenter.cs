using System;

namespace Project.Framework.OutGame
{
    public interface IPresenter : IDisposable
    {
        bool IsDisposed { get; }
        bool IsInitialized { get; }
        void Initialize();
    }
}