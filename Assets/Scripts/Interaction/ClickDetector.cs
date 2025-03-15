using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ClickDetector : MonoBehaviour
{
    public UnityAction<GameObject> OnItemPickUp;
    public UnityAction<GameObject> OnMatchItem;
    public UnityAction<GameObject> OnHover;

    public bool hovers;

    [SerializeField] private PlayerInputReader playerInput;
    private Ray ray;
    private RaycastHit hit;

    private void OnLeftClick()
    {
        CheckForInteractable(OnItemPickUp);
    }

    private void OnRightClick()
    {
        CheckForInteractable(OnMatchItem);
    }

    private void CheckForInteractable(UnityAction<GameObject> interactAction)
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                //Debug.Log("Hit interactable");
                interactAction?.Invoke(hit.collider.gameObject);
                hovers = true;
            }
            else
            {
                hovers = false;
            }
        }
    }

    private void OnEnable()
    {
        playerInput.OnLeftClickEvent += OnLeftClick;
        playerInput.OnRightClickEvent += OnRightClick;
    }

    private void OnDisable()
    {
        playerInput.OnLeftClickEvent -= OnLeftClick;
        playerInput.OnRightClickEvent -= OnRightClick;
    }
}
