using UnityEngine;
using UnityEngine.InputSystem;

public class HoverDetector : MonoBehaviour
{
    [SerializeField] private Inspector inspector;
    private Ray ray;
    private RaycastHit hit;
    private Shard shard;
    private Shard lastShard;

    public void Start()
    {
        InvokeRepeating(nameof(CursorHoversInteractable), 0f, 1/30f);
    }

    private void CursorHoversInteractable()
    {
        if (inspector.isExamining)
        {
            lastShard?.RemoveHoverOutline();
            return;
        }

        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                shard = hit.collider.GetComponent<Shard>();
                shard.AddHoverOutline();
                if (lastShard != null && lastShard != shard)
                {
                    lastShard.RemoveHoverOutline();
                }
                lastShard = shard;
            }
            else
            {
                shard?.RemoveHoverOutline();
            }
        }
    }
}
