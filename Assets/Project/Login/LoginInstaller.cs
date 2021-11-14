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
        [SerializeField] private Toggle rememberMeToggle;
        [SerializeField] private Button loginButton;
        [SerializeField] private ErrorPanel errorPanel;

        private void Reset()
        {
            var fields = GetComponentsInChildren<TMP_InputField>(true);
            usernameField = fields.FirstOrDefault(f => f.transform.parent.name == "Username");
            passwordField = fields.FirstOrDefault(f => f.transform.parent.name == "Password");
            rememberMeToggle = GetComponentInChildren<Toggle>(true);
            loginButton = GetComponentInChildren<Button>(true);
            errorPanel = GetComponentInChildren<ErrorPanel>(true);
        }

        public override void InstallBindings()
        {
            Container.BindInstance(rememberMeToggle);
            Container.BindInstance(errorPanel);
            
            Container.Bind<LoginObservable>()
                .AsSingle()
                .WithArguments(usernameField, passwordField, loginButton);

            Container.BindInterfacesTo<ErrorHandler>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<RememberMe>()
                .AsSingle()
                .WithArguments(usernameField, passwordField);
        }
    }
}