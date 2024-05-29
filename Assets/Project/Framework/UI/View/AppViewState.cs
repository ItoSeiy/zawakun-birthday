using System;

namespace Project.Framework.OutGame
{
    public abstract class AppViewState
    {
        protected AppViewState()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }
    }
}