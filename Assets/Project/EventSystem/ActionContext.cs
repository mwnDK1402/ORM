using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.EventSystem
{
    /// <summary>
    /// Enables and disables actions along with this component.
    /// </summary>
    public sealed class ActionContext : MonoBehaviour
    {
        [SerializeField] private InputActionReference[] refs;

        private void OnEnable()
        {
            foreach (var @ref in refs)
                @ref.action.Enable();
        }

        private void OnDisable()
        {
            foreach (var @ref in refs)
                @ref.action.Disable();
        }
    }
}