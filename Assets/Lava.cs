using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			PlayerHealth.Instance.TakeDamage(99999);
            PlayerHealth.Instance.TakeDamage(99999);
        }
	}
}
