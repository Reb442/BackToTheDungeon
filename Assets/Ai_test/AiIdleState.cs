using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState{    
    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.ResetPath();
        if (agent.animator != null)
        {
            agent.animator.SetBool("idle", true);
        }
    }
    public void Exit(AiAgent agent)
    {
        if (agent.animator != null)
        {
            agent.animator.SetBool("idle", false);
        }
    }
    public void Update(AiAgent agent)
    {
        Vector3 playerDiretion = agent.playerTransform.position - agent.transform.position;
        if (playerDiretion.magnitude < agent.config.chaseDistance)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }

    }
}
