using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebugNewMesh : MonoBehaviour
{
    public bool velocity;
    public bool desiredVelocity;
    public bool path;
    public bool range;
    
    AiAgent agent;
    NavMeshAgent navagent;
    void Start()
    {
        agent = GetComponent<AiAgent>();
        navagent = GetComponent<NavMeshAgent>();
    }

    void OnDrawGizmos()
    {
        if (navagent == null) return;
        if (velocity)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + navagent.velocity);
        }
        if (desiredVelocity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + navagent.desiredVelocity);
        }
        if (path)
        {
            Gizmos.color = Color.blue;
            var agentPath = navagent.path;
            Vector3 prevCorner = transform.position;
            foreach (var corner in agentPath.corners)
            {
                Gizmos.DrawLine(prevCorner, corner);
                Gizmos.DrawSphere(corner, 0.1f);
                prevCorner = corner;
            }
        }
        if (range)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, agent.config.stopDistance);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, agent.config.chaseDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, agent.config.attackDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, agent.config.idleDistance);
        }
    }
}
