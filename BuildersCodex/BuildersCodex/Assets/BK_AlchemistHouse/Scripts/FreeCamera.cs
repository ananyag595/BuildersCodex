using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float lookSpeed = 2.0f; // Adjust mouse sensitivity
    public float zoomSpeed = 5.0f;
    public float minFOV = 20f;
    public float maxFOV = 60f;

    private Camera cam;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        // Movement using WASD or arrow keys
        var horizontalAxis = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * movementSpeed;
        var verticalAxis = Input.GetAxis("Vertical") * Time.fixedDeltaTime * movementSpeed;
        transform.Translate(horizontalAxis, 0, verticalAxis);

        // Rotate only when right-clicking
        if (Input.GetMouseButton(1))
        {
            var lookX = Input.GetAxis("Mouse X") * lookSpeed;
            var lookY = Input.GetAxis("Mouse Y") * lookSpeed;
            transform.eulerAngles += new Vector3(-lookY, lookX, 0);
        }

        // Zoom with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (cam != null)
        {
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - scroll, minFOV, maxFOV);
        }
    }
}
