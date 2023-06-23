using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
{
    float timer = 0.0f;
    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = agent.config.stopDistance;
        if (agent.animator != null)
        {
            agent.animator.SetBool("walk", true);
        }
    }
    public void Exit(AiAgent agent)
    {
        if (agent.animator != null)
        {
            agent.animator.SetBool("walk", false);
        }
    }
    public void Update(AiAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position;

        }

        if (timer < 0.0f)
        {
            Vector3 direction = (agent.playerTransform.position - agent.navMeshAgent.destination);
            Vector3 e_direction = (agent.playerTransform.position - agent.transform.position);
            if (e_direction.magnitude < agent.config.attackDistance)
            {
                agent.stateMachine.ChangeState(AiStateId.Attack);
            }
            else if (e_direction.magnitude > agent.config.idleDistance)
            {
                agent.stateMachine.ChangeState(AiStateId.Idle);
            }
            else if (direction.magnitude > agent.config.stopDistance)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.playerTransform.position;

                    if(!agent.audioSource.isPlaying)
                    {
                        agent.audioSource.Play();
                    }
                }
            }
            else
            {
                return;
            }
            timer = agent.config.maxTime;
        }
    }
}
