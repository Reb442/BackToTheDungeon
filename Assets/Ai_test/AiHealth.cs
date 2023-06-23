using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHealth : MonoBehaviour
{
    private float currentHealth;
    private AiAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AiAgent>();
        currentHealth = agent.config.enemyHealt;
    }
    // Update is called once per frame
    public void TakeDamage(float amount)
    {
        agent.animator.SetTrigger("takehit");
        currentHealth -= amount;
        Debug.Log(currentHealth);
        if(currentHealth <= 0 )
        {
            Die();
        }
        if (agent.stateMachine.currentState == AiStateId.Idle)
        {
            agent.config.idleDistance = 99999;
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }
    private void Die()
    {
        AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
        StartCoroutine(itemDrop());
        Destroy(this.gameObject,6f);
        agent.stateMachine.ChangeState(AiStateId.Death);
    }
    IEnumerator itemDrop()
    {
        yield return new WaitForSeconds(4f);
        float randomValue = Random.value;
        float pRandomValue = Random.value;
        if (randomValue <= 0.4f)
        {
            if (pRandomValue <= 0.5f)
            {
                GameObject ammopup = Instantiate(agent.ammop, agent.transform.position + Vector3.up, agent.transform.rotation);
                ammopup.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
            else
            {
                GameObject healtpup = Instantiate(agent.healthp, agent.transform.position + Vector3.up, agent.transform.rotation);
                healtpup.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
        }
        else
        {

        }
    }
}
