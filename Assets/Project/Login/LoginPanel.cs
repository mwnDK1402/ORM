using Project.GUI;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Login
{
    public sealed class LoginPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField usernameField;
        [SerializeField] private TMP_InputField passwordField;
        [SerializeField] private Button loginButton;

        [SerializeField] private ErrorPanel error;

        private void Reset()
        {
            loginButton = GetComponentInChildren<Button>();
            error = FindObjectOfType<ErrorPanel>(true);
        }

        private void Start() =>
            loginButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (!(ValidateUsername() && ValidatePassword())) return;

                    // Retrieve user
                    // Verify password
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