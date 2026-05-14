using UnityEngine;
using UnityEngine.AI;
public class eenemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform[] waypoints;
    public float waitTime = 2f;
    public float patrolSpeed = 3.5f;

    [Header("Vision Settings")]
    public float viewDistance = 10f;
    public float viewAngle = 60f;
    public Light visionLight; // Drag your Spotlight here


    private NavMeshAgent agent;
    private Transform player;
    private int currentWaypointIndex;
    private bool isWaiting;
    private float waitTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = patrolSpeed;
 
    }

    void Update()
    {
    
    }

   

    

    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        if (angle < viewAngle / 2f && dirToPlayer.magnitude <= viewDistance)
        {
            // Raycast check to ensure no walls are in the way
            if (Physics.Raycast(transform.position, dirToPlayer.normalized, out RaycastHit hit, viewDistance))
            {
                if (hit.collider.CompareTag("Player")) return true;
            }
        }
        return false;
    }
}
