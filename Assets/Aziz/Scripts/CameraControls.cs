using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraControls : MonoBehaviour
{
    private CinemachineCamera cinemachineCamera;
    private Transform playerBody;

    public float mouseSensitivity = 2f;
    public float maxPitch = 90f;
    public float minPitch = -90f;

    private float currentPitch = 0f;
    private float currentYaw = 0f;

    void Start()
    {
        // Find the Cinemachine camera
        cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();

        if (cinemachineCamera == null)
        {
            Debug.LogError("CinemachineCamera not found!");
            return;
        }

        // Get the player from the camera's Follow target
        if (cinemachineCamera.Follow != null)
        {
            playerBody = cinemachineCamera.Follow;
        }
        else
        {
            Debug.LogError("Camera has no Follow target!");
        }

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (cinemachineCamera == null || playerBody == null)
            return;

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Track yaw rotation
        currentYaw += mouseX;

        // Rotate character body ONLY for yaw (left/right)
        playerBody.rotation = Quaternion.Euler(0f, currentYaw, 0f);

        // Calculate pitch for camera (up/down)
        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        // Unlock cursor with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    void LateUpdate()
    {
        if (cinemachineCamera == null)
            return;

        // Apply pitch AND yaw to camera using WORLD rotation
        cinemachineCamera.transform.rotation = Quaternion.Euler(-currentPitch, currentYaw, 0f);
    }
}