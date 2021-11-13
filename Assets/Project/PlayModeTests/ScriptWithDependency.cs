using UnityEngine;
using Zenject;

namespace Project.PlayModeTests
{
    public sealed class ScriptWithDependency : MonoBehaviour
    {
        [Inject]
        private void Construct(object dependency)
        {
        }
    }
}