using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AiAttack : MonoBehaviour
{
    public GameObject EnemyBulletPrefab;
    public List<Transform> nuzzlelist;
    [SerializeField] private float launchForce = 10;
    [SerializeField] private float rateOfFire = 2;
    public bool attackOn = false;
    public bool canShoot = true;
    private AiAgent agent;
    public Collider enemyCollider = null;
    private bool isPlayerInTrigger = false;
    [SerializeField] private Collider explosionCollider;
    public Collider LowR_weapon;

    public float combotCount = 0;
    void Start()
    {
        agent = GetComponent<AiAgent>();
        if (agent.headprefab.GetComponent<Collider>() != null)
        {
            enemyCollider = agent.headprefab.GetComponent<Collider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enemyType == EnemyType.longRange_Enemy ||agent.enemyType == EnemyType.longRange_FlyEnemy)
        {

            if (attackOn == true && canShoot == true)
            {
                
                foreach (Transform nuzzle in nuzzlelist)
                {
                    if (agent.enemyType == EnemyType.longRange_FlyEnemy)
                    {
                        nuzzleMove(nuzzle);
                    }
                    agent.audioSourceEXP.Play();
                    StartCoroutine(LongAttack(nuzzle));
                    canShoot = false;
                }
            }
        }
        else if(agent.enemyType == EnemyType.explosion_Enemy)
        {
            enemyCollider = explosionCollider;
            //Debug.Log("Explsion");
            Debug.Log(isPlayerInTrigger);
            if (isPlayerInTrigger)
            {
                //Debug.Log("ExplsionA");
                PlayerHealth.Instance.TakeDamage(50);
                agent.audioSourceEXP.Play();
                Destroy(agent.audioSourceEXP,0.1f);
                if (transform!= null)
                {
					agent.transform.DOLocalJump(transform.position, 1, 1, 2, snapping: false);
					agent.transform.DOShakeScale(1, 0.5f, 1, 90, false);
				}
                Destroy(gameObject, 1);
            }
        }
        else if(agent.enemyType == EnemyType.lowRange_Enemy)
        {
            if (attackOn == true && canShoot == true)
            {
                agent.audioSourceEXP.Play();
                StartCoroutine(lowAttack());
                canShoot = false;
            }
        }
    }

    IEnumerator LongAttack(Transform muzzle)
    {
        GameObject bullet = Instantiate(EnemyBulletPrefab, muzzle.position, muzzle.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * launchForce, ForceMode.Impulse);
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }
    public void nuzzleMove(Transform muzzle)
    {
        muzzle.transform.LookAt(agent.playerTransform);
    }
    IEnumerator lowAttack()
    {
        float dTime = 0f;
        if (combotCount >2)
        {
            if (isPlayerInTrigger)
            {
                PlayerHealth.Instance.TakeDamage(50);
            }
            agent.animator.SetTrigger("attackCom");
            combotCount = 0;
            dTime = 2f;
        }
        else
        {
            if (isPlayerInTrigger)
            {
                PlayerHealth.Instance.TakeDamage(50);
            }
            agent.animator.SetTrigger("attackDef");
            combotCount++;
            dTime = 1f;
        }
        yield return new WaitForSeconds(dTime);
        canShoot = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("enter");
            isPlayerInTrigger = true;
		}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("exit");
            isPlayerInTrigger = false;
        }
    }   
}
