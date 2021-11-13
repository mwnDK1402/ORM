using System;
using JetBrains.Annotations;
using Project.Database;
using Project.GUI;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace Project.Login
{
    [UsedImplicitly]
    public sealed class LoginPrompt : IInitializable, IDisposable
    {
        private readonly SignalBus signalBus;
        private readonly UserQueries queries;
        private readonly TMP_InputField usernameField;
        private readonly TMP_InputField passwordField;
        private readonly Button loginButton;
        private readonly ErrorPanel error;
        
        private IDisposable loginButtonObserver;

        public LoginPrompt(
            SignalBus signalBus,
            UserQueries queries,
            TMP_InputField usernameField,
            TMP_InputField passwordField,
            Button loginButton,
            ErrorPanel error)
        {
            this.signalBus = signalBus;
            this.queries = queries;
            this.usernameField = usernameField;
            this.passwordField = passwordField;
            this.loginButton = loginButton;
            this.error = error;
        }

        void IInitializable.Initialize() =>
            loginButtonObserver = loginButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (!(ValidateUsername() && ValidatePassword())) return;

                    if (queries.TryLogin(usernameField.text, passwordField.text, out var user))
                        signalBus.TryFire(new LoginSignal(user));
                    else error.Show("Invalid username or password");
                });

        void IDisposable.Dispose() => loginButtonObserver.Dispose();

        private bool ValidateUsername()
        {
            var username = usernameField.text;
            var usernameResult = Username.Validate(username);
            if (usernameResult == Username.ValidationResult.Valid)
                return true;

            error.Show(Username.GetError(usernameResult));
            return false;
        }

        private bool ValidatePassword()
        {
            var password = passwordField.text;
            var passwordResult = Password.Validate(password);
            if (passwordResult == Password.ValidationResult.Valid)
                return true;

            error.Show(Password.GetError(passwordResult));
            return false;
        }
    }
}