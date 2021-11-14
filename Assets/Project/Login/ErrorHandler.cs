using System;
using JetBrains.Annotations;
using Project.GUI;
using UniRx;
using Zenject;

namespace Project.Login
{
    [UsedImplicitly]
    public sealed class ErrorHandler : IInitializable, IDisposable
    {
        private readonly LoginObservable loginObservable;
        private readonly ErrorPanel errorPanel;

        private IDisposable loginObserver;

        public ErrorHandler(LoginObservable loginObservable, ErrorPanel errorPanel)
        {
            this.loginObservable = loginObservable;
            this.errorPanel = errorPanel;
        }

        void IInitializable.Initialize() =>
            loginObserver = loginObservable
                .Subscribe(user =>
                {
                    if (user == default) errorPanel.Show("Invalid username or password");
                });

        void IDisposable.Dispose() => loginObserver.Dispose();
    }
}