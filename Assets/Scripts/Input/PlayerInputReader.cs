using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static InputActions;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "Scriptable Objects/PlayerInputReader")]
public class PlayerInputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction OnLeftClickEvent = delegate { };
    public event UnityAction OnRightClickEvent = delegate { };

    public event UnityAction OnLeaveEvent = delegate { };
    public event UnityAction<Vector2> OnRotateEvent = delegate { };

    public bool IsHoldingMouseButtonDown { get; private set; }

    private InputActions inputActions;

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new InputActions();
            inputActions.Player.SetCallbacks(this);
        }
        inputActions.Player.Enable();

    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void EnablePlayerActions() => inputActions.Player.Enable();
    public void DisablePlayerActions() => inputActions.Player.Disable();

    public void OnLeave(InputAction.CallbackContext context)
    {
        OnLeaveEvent?.Invoke();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        OnRotateEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            IsHoldingMouseButtonDown = true;
        }
        if (context.phase == InputActionPhase.Performed)
        {
            OnLeftClickEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            IsHoldingMouseButtonDown = false;
        }
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnRightClickEvent?.Invoke();
        }
    }

}
