using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    [Header("Patrol Points (4 Points)")]
    public Transform[] points; // حط 4 نقاط هنا في Inspector

    public float chaseRange = 10f;
    public float viewAngle = 90f;

    public AudioSource audioSource;
    public AudioClip chaseSound;

    int currentIndex = 0;

    void Start()
    {
        agent.autoBraking = false;
        agent.stoppingDistance = 0.3f;

        GoToNextPoint();
    }

    void Update()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);
        float angle = Vector3.Angle(transform.forward, dir);

        bool canSeePlayer = false;

        if (distance < chaseRange && angle < viewAngle / 2)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + Vector3.up, dir, out hit, chaseRange))
            {
                if (hit.transform == player)
                {
                    canSeePlayer = true;
                }
            }
        }

        if (canSeePlayer)
        {
            agent.SetDestination(player.position);

            if (!audioSource.isPlaying)
            {
                audioSource.clip = chaseSound;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            if (!agent.pathPending && agent.remainingDistance <= 0.5f)
            {
                GoToNextPoint();
            }

            if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.1f)
            {
                GoToNextPoint();
            }
        }
    }

    void GoToNextPoint()
    {
        if (points.Length == 0) return;

        agent.SetDestination(points[currentIndex].position);

        currentIndex = (currentIndex + 1) % points.Length;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
}