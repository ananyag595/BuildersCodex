using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    [Header("Look")]
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [Header("Interaction")]
    public float interactRange = 3f;
    public KeyCode interactKey = KeyCode.E;
    public LayerMask interactableLayer;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    [Header("Cursor")]
    public bool lockCursor = true;
    private bool cursorLocked = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        UpdateCursorState();

        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
            Debug.LogWarning("Camera not assigned - auto-assigned child camera");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleLookRotation();
        HandleInteraction();
        HandleCursorToggle();
    }

    void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleLookRotation()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            // Remove the layer mask check and use tags instead
            if (Physics.Raycast(ray, out hit, interactRange))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
                    if (interactable != null)
                    {
                        interactable.OnInteract();
                    }
                    else
                    {
                        Debug.LogWarning("Object is tagged 'Interactable' but missing InteractableObject script!");
                    }
                }
            }
        }
    }

    void HandleCursorToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLocked = !cursorLocked;
            UpdateCursorState();
        }

        if (cursorLocked && Input.GetMouseButtonDown(0))
        {
            UpdateCursorState();
        }
    }

    void UpdateCursorState()
    {
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !cursorLocked;
    }

    void OnDrawGizmosSelected()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.blue;
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            Gizmos.DrawRay(rayOrigin, playerCamera.transform.forward * interactRange);
        }
    }
}