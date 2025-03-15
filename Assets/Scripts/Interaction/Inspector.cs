using UnityEngine;
using UnityEngine.InputSystem;

public class Inspector : MonoBehaviour
{
    public bool isExamining { get; private set; }

    [SerializeField] private PlayerInputReader playerInput;
    [SerializeField] private ClickDetector clickDetector;
    [SerializeField] private Transform examinePosition;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 25f;

    private MoveUtils moveUtils;
    private GameObject currentGameObject;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake()
    {
        moveUtils = new MoveUtils();
    }

    private void OnEnable()
    {
        clickDetector.OnItemPickUp += EnterExamineMode;
        playerInput.OnRightClickEvent += ExitExamineMode;
    }

    private void OnDisable()
    {
        clickDetector.OnItemPickUp -= EnterExamineMode;
        playerInput.OnRightClickEvent -= ExitExamineMode;
    }

    private void EnterExamineMode(GameObject clickedObject)
    {
        if (isExamining) return;
        isExamining = true;

        currentGameObject = clickedObject;
        originalPosition = clickedObject.transform.position;
        originalRotation = clickedObject.transform.rotation;

        // Move object to examine position
        StartCoroutine(moveUtils.MoveToPosition(clickedObject.transform, examinePosition.position, examinePosition.rotation, moveSpeed));
    }

    private void ExitExamineMode()
    {
        if (!isExamining) return;

        StartCoroutine(moveUtils.MoveToPosition(currentGameObject.transform, originalPosition, originalRotation, moveSpeed, () => isExamining = false, rotate: true));
    }

    private void Update()
    {
        if (isExamining && playerInput.IsHoldingMouseButtonDown)
        {
            RotateObject();
        }
    }
    private void RotateObject()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float rotX = mouseDelta.x * rotateSpeed * Time.deltaTime;
        float rotY = mouseDelta.y * rotateSpeed * Time.deltaTime;

        currentGameObject.transform.Rotate(Vector3.up, -rotX, Space.World);
        currentGameObject.transform.Rotate(Vector3.right, rotY, Space.World);
    }
}
