// using UnityEngine;
// using UnityEngine.AI;

// public class Enemy : MonoBehaviour
// {
//     public NavMeshAgent agent;
//     public Transform player;

//     public float walkRange = 20f;
//     public float chaseRange = 10f;
//     public float viewAngle = 90f;

//     Vector3 randomPoint;

//     void Start()
//     {
//         NewPoint();
//     }

//     void Update()
//     {
//         Vector3 dir =
//             (player.position - transform.position).normalized;

//         float distance =
//             Vector3.Distance(transform.position, player.position);

//         float angle =
//             Vector3.Angle(transform.forward, dir);

//         bool canSeePlayer = false;

//         if (distance < chaseRange && angle < viewAngle / 2)
//         {
//             RaycastHit hit;

//             if (Physics.Raycast(transform.position, dir,
//                 out hit, chaseRange))
//             {
//                 if (hit.transform == player)
//                 {
//                     canSeePlayer = true;
//                 }
//             }
//         }

//         if (canSeePlayer)
//         {
//             agent.SetDestination(player.position);
//         }
//         else
//         {
//             if (!agent.pathPending &&
//                 agent.remainingDistance <= agent.stoppingDistance)
//             {
//                 NewPoint();
//             }

//             agent.SetDestination(randomPoint);
//         }
//     }

//     void NewPoint()
//     {
//         Vector3 random =
//             Random.insideUnitSphere * walkRange;

//         random += transform.position;

//         NavMeshHit hit;

//         NavMesh.SamplePosition(
//             random,
//             out hit,
//             walkRange,
//             NavMesh.AllAreas);

//         randomPoint = hit.position;
//     }
// }