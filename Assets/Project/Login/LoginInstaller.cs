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
            // This installs one SignalBus in the SceneContext.
            // If the Signal spans multiple scenes, it should be put in a parent container like ProjectContext.
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<LoginSignal>();

            Container.BindInstance(loginButton);
            Container.BindInstance(errorPanel);

            Container.BindInterfacesTo<LoginPrompt>()
                .AsSingle()
                .WithArguments(usernameField, passwordField);

            Container.BindSignal<LoginSignal>()
                .ToMethod(signal => Debug.Log($"Logging in {signal.User.Username}...", this));
        }
    }
}