using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Transform objectToRotate;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private ParticleSystem pickupEffect;
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private KeyCode pickupKey = KeyCode.E;

    private bool hasBeenPickedUp = false;
    private Transform player;
    private bool isPlayerInRange = false;

    private void Start()
    {
        // Find the player in the scene
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogWarning("Player not found! Make sure to tag your player as 'Player'");
        }
    }

    private void Update()
    {
        if (hasBeenPickedUp || player == null)
            return;

        // Check if player is in range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isPlayerInRange = distanceToPlayer <= interactionRange;

        // Check if player pressed the pickup key
        if (isPlayerInRange && Input.GetKeyDown(pickupKey))
        {
            PickupItem();
        }
    }

    private void PickupItem()
    {
        hasBeenPickedUp = true;

        // Play pickup sound if assigned
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        // Play particle effect if assigned
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }

        // Rotate the object 90 degrees instantly
        if (objectToRotate != null)
        {
            objectToRotate.Rotate(0f, 90f, 0f, Space.Self);
        }
        else
        {
            Debug.LogWarning("No object assigned to rotate in ItemPickup script!");
        }

        // Destroy the item from the scene
        Destroy(gameObject);
    }

    // Visualize the interaction range in the Scene view
    private void OnDrawGizmos()
    {
        // Draw semi-transparent sphere when not selected
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        DrawWireSphere(transform.position, interactionRange, 20);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw brighter sphere when selected
        Gizmos.color = new Color(1f, 1f, 0f, 0.8f);
        DrawWireSphere(transform.position, interactionRange, 20);

        // Draw a line to the player if in range
        if (Application.isPlaying && isPlayerInRange && player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }

    // Helper method to draw a wire sphere
    private void DrawWireSphere(Vector3 position, float radius, int segments)
    {
        float angle = 0f;
        float angleStep = 360f / segments;

        Vector3 lastPoint = position + new Vector3(radius, 0, 0);

        for (int i = 0; i <= segments; i++)
        {
            float rad = angle * Mathf.Deg2Rad;
            Vector3 newPoint = position + new Vector3(Mathf.Cos(rad) * radius, 0, Mathf.Sin(rad) * radius);
            Gizmos.DrawLine(lastPoint, newPoint);
            lastPoint = newPoint;
            angle += angleStep;
        }

        // Draw vertical circles
        for (int i = 0; i < 3; i++)
        {
            float yOffset = (i - 1) * radius * 0.5f;
            angle = 0f;
            lastPoint = position + new Vector3(radius, yOffset, 0);

            for (int j = 0; j <= segments; j++)
            {
                float rad = angle * Mathf.Deg2Rad;
                Vector3 newPoint = position + new Vector3(Mathf.Cos(rad) * radius, yOffset, Mathf.Sin(rad) * radius);
                Gizmos.DrawLine(lastPoint, newPoint);
                lastPoint = newPoint;
                angle += angleStep;
            }
        }
    }
}