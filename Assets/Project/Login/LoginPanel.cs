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
    public sealed class LoginPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField usernameField;
        [SerializeField] private TMP_InputField passwordField;
        [SerializeField] private Button loginButton;

        [SerializeField] private ErrorPanel error;

        [field: SerializeField] public UnityEvent<User> LoggedIn { get; [UsedImplicitly] private set; }

        private UserQueries queries;

        private void Reset()
        {
            loginButton = GetComponentInChildren<Button>();
            error = FindObjectOfType<ErrorPanel>(true);
        }

        [Inject]
        private void Construct(UserQueries queries) => this.queries = queries;

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