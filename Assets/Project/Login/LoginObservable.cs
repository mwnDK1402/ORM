using System;
using JetBrains.Annotations;
using Project.Database;
using TMPro;
using UniRx;
using UnityEngine.UI;

namespace Project.Login
{
    [UsedImplicitly]
    public sealed class LoginObservable : IObservable<User>
    {
        private readonly IObservable<User> inner;

        public LoginObservable(
            UserQueries userQueries,
            TMP_InputField usernameField,
            TMP_InputField passwordField,
            Button loginButton) =>
            inner = loginButton.OnClickAsObservable()
                .Select(_ => userQueries.FindUser(usernameField.text, passwordField.text));

        public IDisposable Subscribe(IObserver<User> observer) => inner.Subscribe(observer);
    }
}