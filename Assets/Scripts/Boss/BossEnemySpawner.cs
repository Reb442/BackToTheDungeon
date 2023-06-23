using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour
{
	public GameObject Enemy;
	public int a;
	bool canSpawn = true;

	private void Update()
	{
		for (int i = 0; i < a; i++)
		{
			if (Boss.Instance.enemySpawnAttack && canSpawn)
			{
				StartCoroutine(spawner());
				canSpawn = false;
			}
		}
		
	}

	IEnumerator spawner()
	{
		GameObject enemy = Instantiate(Enemy, transform.position, transform.rotation);
		enemy.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000, ForceMode.Force);
		yield return new WaitForSeconds(1);
		canSpawn = true;
	}
}
