using System;
using JetBrains.Annotations;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace Project.Login
{
    [UsedImplicitly]
    public sealed class RememberMe : IInitializable, IDisposable
    {
        private readonly TMP_InputField usernameField;
        private readonly TMP_InputField passwordField;
        private readonly Toggle rememberMeToggle;
        private readonly LoginObservable loginObservable;

        private readonly CompositeDisposable observer = new();

        public RememberMe(
            TMP_InputField usernameField,
            TMP_InputField passwordField,
            Toggle rememberMeToggle,
            LoginObservable loginObservable)
        {
            this.usernameField = usernameField;
            this.passwordField = passwordField;
            this.rememberMeToggle = rememberMeToggle;
            this.loginObservable = loginObservable;
        }

        void IInitializable.Initialize()
        {
            rememberMeToggle.OnValueChangedAsObservable()
                .Subscribe(isOn =>
                {
                    if (!isOn) SaveLoginService.Clear();
                })
                .AddTo(observer);

            loginObservable
                .Where(user => user != default)
                .WithLatestFrom(rememberMeToggle.OnValueChangedAsObservable(), (_, isOn) => isOn)
                .Subscribe(isOn =>
                {
                    if (isOn) SaveLoginService.Save(usernameField.text, passwordField.text);
                })
                .AddTo(observer);

            (usernameField.text, passwordField.text) = SaveLoginService.Load();
        }

        void IDisposable.Dispose() => observer.Dispose();
    }
}