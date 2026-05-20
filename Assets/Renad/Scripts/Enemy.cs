using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public float walkRange = 20f;
    public float chaseRange = 10f;
    public float viewAngle = 90f;

    public AudioSource audioSource; 
    public AudioClip chaseSound;    

    Vector3 randomPoint;

    void Start()
    {
        NewPoint();
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
            if (audioSource.isPlaying) audioSource.Stop();

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                NewPoint();
            }
            agent.SetDestination(randomPoint);
        }
    }

    // هذه الدالة هي الحل: بمجرد ما يلمس جسم اللاعب، ينتقل للسين 2
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }

    void NewPoint()
    {
        Vector3 random = Random.insideUnitSphere * walkRange;
        random += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(random, out hit, walkRange, NavMesh.AllAreas);
        randomPoint = hit.position;
    }
}