using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    longRange_Enemy,
    lowRange_Enemy,
    longRange_FlyEnemy,
    lowRange_FlyEnemy,
    explosion_Enemy
}
public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public EnemyType enemyType;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Transform playerTransform;
    public AiAttack attack;
    public AiHealth health;
    public Animator animator;
    public float attackdeff = 219.4f;

    public AudioSource audioSource;
    public AudioSource audioSourceEXP;

    public  GameObject headprefab;
    public GameObject ammop;
    public GameObject healthp;

    // Start is called before the first frame update
    void Start()
    {
        enemyType = config.enemyType;
        attack = GetComponent<AiAttack>();
        health = GetComponent<AiHealth>(); 
        if(GetComponentInChildren<Animator>() != null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiAttackState());
        initialState = AiStateId.Idle;
        stateMachine.ChangeState(initialState);
        navMeshAgent.angularSpeed = config.angularSpeed;
        navMeshAgent.speed = config.enemyspeed;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
    public void RotReset()
    {
        if(stateMachine.currentState != AiStateId.Attack) { 
            if (config.enemyType == EnemyType.lowRange_FlyEnemy || config.enemyType == EnemyType.longRange_FlyEnemy)
            {
                do
                {
                    Quaternion headRotation = Quaternion.Lerp(headprefab.transform.rotation,
                                                Quaternion.Euler(0f, transform.rotation.y, 0f), config.angularSpeed / 3600);
                    headprefab.transform.rotation = headRotation;
                } while (headprefab.transform.rotation.y == 180f);
            }
            if (config.enemyType == EnemyType.longRange_Enemy)
            {
                do
                {
                    Quaternion headRotation = Quaternion.Slerp(headprefab.transform.localRotation,
                                            Quaternion.Euler(0f, 180f, 0f), config.angularSpeed / 3600);
                    headprefab.transform.localRotation = headRotation;
                } while (headprefab.transform.localRotation.y == 180f);
            }
        }
    }
}
