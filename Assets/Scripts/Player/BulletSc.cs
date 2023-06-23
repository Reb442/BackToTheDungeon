using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSc : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			PlayerHealth.Instance.TakeDamage(20);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
