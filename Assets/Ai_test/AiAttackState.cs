using UnityEngine;


public class AiAttackState : AiState
{

    public AiStateId GetId()
    {
        return AiStateId.Attack;
    }
    public void Enter(AiAgent agent)
    {
        if (agent.animator != null)
        {
            agent.animator.SetBool("attack", true);
        }
    }

    public void Exit(AiAgent agent)
    {
        if (agent.animator != null)
        {
            agent.animator.SetBool("attack", false);
        }
        agent.attack.combotCount = 0;
    }

    public void Update(AiAgent agent)
    {
        Vector3 direction = (agent.playerTransform.position - agent.transform.position);

        float targetAngleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotationY = Quaternion.Euler(0f, targetAngleY, 0f);

        if(agent.config.enemyType == EnemyType.lowRange_FlyEnemy || agent.config.enemyType == EnemyType.longRange_FlyEnemy)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.playerTransform.position - agent.headprefab.transform.position);
            float xRotation = targetRotation.eulerAngles.x;
            Quaternion headRotation = Quaternion.Lerp(agent.headprefab.transform.rotation, 
                                        Quaternion.Euler(xRotation*-1, targetAngleY + 180, 0f), agent.config.angularSpeed / 3600);
            agent.headprefab.transform.rotation = headRotation;
        }
        else if(agent.config.enemyType == EnemyType.longRange_Enemy)
        {
            Quaternion fixRot = Quaternion.Lerp(agent.headprefab.transform.rotation,
                                                Quaternion.Euler(0f, targetAngleY + agent.attackdeff, 0f),agent.config.angularSpeed / 3600);
            agent.headprefab.transform.rotation = fixRot;
            agent.attack.nuzzleMove(agent.attack.nuzzlelist[0]);
        }
        agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, targetRotationY, agent.config.angularSpeed / 3600);

        agent.attack.attackOn = true;

        if (direction.magnitude > agent.config.chaseDistance)
        {
            agent.attack.attackOn = false;
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }
}