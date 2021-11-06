using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Project.EventSystem
{
    public sealed class UpDownNavigationWithAction : MonoBehaviour
    {
        [SerializeField] private InputActionReference down;

        private UnityEngine.EventSystems.EventSystem system;

        private void OnValidate() => enabled = down;

        private void Start() => system = UnityEngine.EventSystems.EventSystem.current;

        private void OnEnable() => down.action.performed += OnAction;

        private void OnAction(InputAction.CallbackContext obj)
        {
            var selectable = GetNewSelectable();
            if (selectable) SetSelected(selectable);
        }

        private void SetSelected(Selectable next)
        {
            if (next.TryGetComponent<InputField>(out var field))
                field.OnPointerClick(
                    new PointerEventData(system)); // If it's an input field, also set the text caret

            system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
        }

        private Selectable GetNewSelectable()
        {
            var selected = system.currentSelectedGameObject.GetComponent<Selectable>();
            return Keyboard.current.shiftKey.isPressed
                ? selected.FindSelectableOnUp()
                : selected.FindSelectableOnDown();
        }

        private void OnDisable() => down.action.performed -= OnAction;
    }
}