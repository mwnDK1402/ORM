using JetBrains.Annotations;
using Project.Database;
using Project.GUI;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Project.Login
{
    public sealed class LoginPrompt : MonoBehaviour
    {
        [field: SerializeField] public UnityEvent<User> LoggedIn { get; [UsedImplicitly] private set; }

        private TMP_InputField usernameField;
        private TMP_InputField passwordField;
        private Button loginButton;
        private ErrorPanel error;
        private UserQueries queries;

        [Inject]
        private void Construct(
            [Inject(Id = typeof(Username))] TMP_InputField usernameField,
            [Inject(Id = typeof(Password))] TMP_InputField passwordField,
            Button loginButton,
            ErrorPanel error,
            UserQueries queries)
        {
            this.loginButton = loginButton;
            this.passwordField = passwordField;
            this.usernameField = usernameField;
            this.error = error;
            this.queries = queries;
        }

        private void Start() =>
            loginButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (!(ValidateUsername() && ValidatePassword())) return;

                    if (queries.TryLogin(usernameField.text, passwordField.text, out var user)) LoggedIn.Invoke(user);
                    else error.Show("Invalid username or password");
                })
                .AddTo(gameObject);

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