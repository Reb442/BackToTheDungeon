using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeHealth : MonoBehaviour
{
	public BossHealth boss;
	public bool dead;
    public float maxHealth;
    public float currentHealth;
	[SerializeField] private Material blackMat;

	private void Start()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			dead = true;
			boss.currentHealth -= maxHealth;
			gameObject.GetComponent<SphereCollider>().enabled = false;
			gameObject.GetComponent<Renderer>().material = blackMat;
		}

	}
}
