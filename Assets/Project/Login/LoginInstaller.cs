using System.Linq;
using Project.GUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Login
{
    public sealed class LoginInstaller : MonoInstaller<LoginInstaller>
    {
        [SerializeField] private TMP_InputField usernameField;
        [SerializeField] private TMP_InputField passwordField;
        [SerializeField] private Button loginButton;
        [SerializeField] private ErrorPanel errorPanel;

        private void Reset()
        {
            var fields = GetComponentsInChildren<TMP_InputField>(true);
            usernameField = fields.FirstOrDefault(f => f.transform.parent.name == "Username");
            passwordField = fields.FirstOrDefault(f => f.transform.parent.name == "Password");
            loginButton = GetComponentInChildren<Button>(true);
            errorPanel = GetComponentInChildren<ErrorPanel>(true);
        }

        public override void InstallBindings()
        {
            Container.BindInstance(usernameField).WithId(typeof(Username));
            Container.BindInstance(passwordField).WithId(typeof(Password));
            Container.BindInstance(loginButton);
            Container.BindInstance(errorPanel);
        }
    }
}