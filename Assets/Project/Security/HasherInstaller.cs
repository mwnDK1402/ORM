using UnityEngine;
using Zenject;

namespace Project.Security
{
    [CreateAssetMenu(menuName = "Installers/Create " + nameof(HasherInstaller), fileName = nameof(HasherInstaller))]
    public sealed class HasherInstaller : ScriptableObjectInstaller<HasherInstaller>
    {
        [SerializeField] private int degreesOfParallelism = 1;
        [SerializeField] private int iterations = 1;
        [SerializeField] private int memorySize = 16;

        public override void InstallBindings() => Container.Bind<Hasher>().AsSingle()
            .WithArguments(degreesOfParallelism, iterations, memorySize);
    }
}