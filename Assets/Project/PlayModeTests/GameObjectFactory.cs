using JetBrains.Annotations;
using NUnit.Framework;
using Zenject;

namespace Project.PlayModeTests
{
    public sealed class GameObjectFactory : ZenjectIntegrationTestFixture
    {
        [Test]
        public void ScriptWithDependency_Gets_Injected()
        {
            PreInstall();
            Container.BindFactory<object, ScriptWithDependency, Factory>().FromNewComponentOnNewGameObject();
            PostInstall();

            Container.Resolve<Factory>().Create(0);
        }
        
        [UsedImplicitly]
        private sealed class Factory : PlaceholderFactory<object, ScriptWithDependency>
        {
        }
    }
}
