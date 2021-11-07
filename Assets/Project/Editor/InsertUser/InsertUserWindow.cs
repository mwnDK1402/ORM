using Project.Database;
using Project.Security;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Project.Editor.InsertUser
{
    public sealed class InsertUserWindow : ZenjectEditorWindow
    {
        [SerializeField] private VisualTreeAsset window;

        [SerializeField] private string username = string.Empty;
        [SerializeField] private string password = string.Empty;

        [Inject] private UserQueries queries;

        [MenuItem("Database/Insert User")]
        public static void Init()
        {
            var wnd = GetWindow<InsertUserWindow>();
            wnd.titleContent = new GUIContent("Insert User");
            var size = new Vector2(350, 60);
            wnd.minSize = size;
            wnd.maxSize = size;
        }

        public override void InstallBindings()
        {
            DatabaseInstaller.InstallFromResource(Container);
            HasherInstaller.InstallFromResource(Container);
        }

        public override void OnEnable()
        {
            base.OnEnable();

            var root = rootVisualElement;
            var so = new SerializedObject(this);
            root.Bind(so);
            root.Add(window.Instantiate());
            root.Q<Button>().clicked += () => queries.Insert(username, password);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            var root = rootVisualElement;
            root.Unbind();
            root.Q<TextField>(nameof(username)).value = "";
            root.Q<TextField>(nameof(password)).value = "";
        }
    }
}