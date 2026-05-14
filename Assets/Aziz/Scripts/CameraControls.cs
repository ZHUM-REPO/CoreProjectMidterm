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

        // Rotate player body horizontally (yaw/left-right)
        playerBody.Rotate(Vector3.up * mouseX, Space.World);

        // Rotate camera pitch (up/down) - apply to camera's local rotation
        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        // Apply ONLY pitch to camera's local X rotation
        cinemachineCamera.transform.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);

        // Unlock cursor with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}