using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Death;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.enabled = false; //bu kod legal deðil
        agent.attack.attackOn = false;
        if (agent.animator != null)
        {
            agent.animator.SetTrigger("death");
        }
    }
    public void Exit(AiAgent agent)
    {

    }
    public void Update(AiAgent agent)
    {
        float deathY = 0;
        if (agent.enemyType == EnemyType.lowRange_Enemy || agent.enemyType == EnemyType.longRange_Enemy || agent.enemyType == EnemyType.longRange_FlyEnemy){
            if (agent.enemyType == EnemyType.lowRange_Enemy || agent.enemyType == EnemyType.longRange_Enemy)
            {
                deathY = -0.145f;
            }
            else if (agent.enemyType == EnemyType.longRange_FlyEnemy)
            {
                deathY = 1.68f;
            }
            Vector3 deathPos = new Vector3(agent.transform.position.x, deathY, agent.transform.position.z);
            agent.transform.position = Vector3.Lerp(agent.transform.position, deathPos, 8 * Time.deltaTime);
        }
    }
}
