using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Transform objectToRotate;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private float rotationObjectx = 0f;
    [SerializeField] private float rotationObjecty = 0f;
    [SerializeField] private float rotationObjectz = 0f;
    [SerializeField] private ParticleSystem pickupEffect;
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private KeyCode pickupKey = KeyCode.E;

    private bool hasBeenPickedUp = false;
    private Transform player;
    private bool isPlayerInRange = false;

    private void Start()
    {
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

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isPlayerInRange = distanceToPlayer <= interactionRange;

        if (isPlayerInRange && Input.GetKeyDown(pickupKey))
        {
            PickupItem();
        }
    }

    private void PickupItem()
    {
        hasBeenPickedUp = true;

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }


        else
        {
            Debug.LogWarning("No object assigned to rotate in ItemPickup script!");
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        DrawWireSphere(transform.position, interactionRange, 20);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.8f);
        DrawWireSphere(transform.position, interactionRange, 20);

        if (Application.isPlaying && isPlayerInRange && player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }

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