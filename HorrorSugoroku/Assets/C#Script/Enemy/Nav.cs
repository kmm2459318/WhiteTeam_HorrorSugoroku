using UnityEngine;
using UnityEngine.AI;

public class Nav : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform target;

    private void Update()
    {
        agent.SetDestination(target.position);
    }
}
