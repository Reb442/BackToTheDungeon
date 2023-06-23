using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
	[SerializeField]private float maxHealth;
	public float currentHealth;
	public BossEyeHealth[] bossEye;
	public BossHeadHealth[] bossHead;
	public GameObject[] phase1objs;
	public GameObject[] phase2objs;
	public GameObject[] phase2laserObjs;
	public GameObject[] enemies;
	public Transform[] enemySpawnPoints;

	public Material redMat;
	Animator anim;
	public bool phase2 = false,deathPhase = false;

	private void Start()
	{
		anim = GetComponent<Animator>();
		currentHealth = maxHealth;
	}

	private void Update()
	{
		if (bossEye[0].dead && bossEye[1].dead && bossEye[2].dead && !phase2)
		{
			Phase2();
		}
		if (bossHead[0].dead && bossHead[1].dead && !deathPhase)
		{
			for (int i = 0; i < phase2laserObjs.Length; i++)
			{
				phase2laserObjs[i].SetActive(false);
			}
			DeathPhase();
		}
		if (bossEye[0].dead)
		{
			phase1objs[0].SetActive(false);
		}

	}

	void Phase1()
	{
		StartCoroutine(Phase1Coroutine());
	}
	void EnemySpawn()
	{
		Instantiate(enemies[Random.Range(0, enemies.Length)], enemySpawnPoints[Random.Range(0, enemies.Length)].position, Quaternion.identity);
	}

	void Phase2()
	{
		phase2 = true;
		StopAllCoroutines();
		StartCoroutine(Phase2Coroutine());
		anim.SetTrigger("Phase2");
		foreach (var obj in phase2objs)
		{
			obj.GetComponent<Renderer>().material = redMat;
		}
		
	}
	void DeathPhase()
	{
		StopAllCoroutines();
		anim.SetTrigger("Death");
		deathPhase = true;
		
	}

	IEnumerator Phase1Coroutine()
	{
		if (!bossEye[0].dead)
		{
			phase1objs[0].SetActive(true);
		}
		yield return new WaitForSeconds(10f);
		phase1objs[0].SetActive(false);
		for (int i = 0; i <= 5; i++)
		{
			EnemySpawn();
			yield return new WaitForSeconds(1f);
		}

		yield return new WaitForSeconds(20f);
		if (!phase2 || !deathPhase)
		{
			StartCoroutine(Phase1Coroutine());
		}
	}

	IEnumerator Phase2Coroutine()
	{
		yield return new WaitForSeconds(2f);
		for (int i = 0; i < phase2laserObjs.Length; i++)
		{
			phase2laserObjs[i].SetActive(true);
		}
		yield return new WaitForSeconds(10f);
		for (int i = 0; i < phase2laserObjs.Length; i++)
		{
			phase2laserObjs[i].SetActive(false);
		}
		yield return new WaitForSeconds(1f);
		for (int i = 0; i <= 5; i++)
		{
			EnemySpawn();
			yield return new WaitForSeconds(1f);
		}
		if (phase2)
		{
			StartCoroutine(Phase2Coroutine());
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Phase1();
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}
}
