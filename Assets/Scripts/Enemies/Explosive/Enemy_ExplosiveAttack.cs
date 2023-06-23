using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy_ExplosiveAttack : MonoBehaviour
{
	public GameObject enemy;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("Explode");
			PlayerHealth.Instance.TakeDamage(50);
			enemy.transform.DOLocalJump(transform.position,1,1,2,snapping:false);
			enemy.transform.DOShakeScale(1,0.5f,1,90,false);
			Destroy(enemy,1);
		}
	}
}
