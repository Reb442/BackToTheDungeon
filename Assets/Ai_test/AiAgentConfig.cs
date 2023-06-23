using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    //enemy config
    public EnemyType enemyType = EnemyType.longRange_Enemy;
    public float enemyHealt = 100.0f;
    public float angularSpeed = 200.0f;
    public float enemyspeed = 10.0f;

    //state config
    public float stopDistance = 10.0f; //Durma Uzaklýðý
    public float maxTime = 1.0f; //navmesh yenileme süresi
    public float chaseDistance = 20.0f; //idledan chase gecme uzaklýðý
    public float attackDistance = 15.0f; //chaseten attack gecme uzaklýðý
    public float idleDistance = 30.0f; //ChaseStateten Idle gecme uzaklýðý
}
