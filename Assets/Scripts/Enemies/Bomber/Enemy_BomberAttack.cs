using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BomberAttack : MonoBehaviour
{
	private Transform player;
	[SerializeField] private GameObject EnemyBulletPrefab;
	[SerializeField] private Transform muzzle;
	[SerializeField] private float launchFore;
	[SerializeField] private float launchForeY;
	[SerializeField] private float rateOfFire;
	[SerializeField] private float sightRange;
	[SerializeField] LayerMask whatIsPlayer;
	public bool playerInRange;
	bool canShoot = true;
	void Start()
	{
		player = GameObject.FindAnyObjectByType<PlayerMove>().transform;
	}
	void Update()
    {

		playerInRange = Physics.CheckSphere(transform.position, sightRange,whatIsPlayer);

		if (playerInRange)
		{
			launchFore = (Vector3.Distance(transform.position, player.transform.position)) *2;
			if (canShoot)
			{
				StartCoroutine(Shooting());
				canShoot = false;
			}
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.gameObject.name);
		if (other.gameObject.CompareTag("Player"))
		{
			playerInRange = true;
		}
	}
	IEnumerator Shooting()
	{
		GameObject bullet = Instantiate(EnemyBulletPrefab, muzzle.position, muzzle.rotation);
		bullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * launchFore, ForceMode.Impulse);
		bullet.GetComponent<Rigidbody>().AddForce(muzzle.up * launchForeY, ForceMode.Impulse);
		Destroy(bullet, 5);
		yield return new WaitForSeconds(rateOfFire);
		canShoot = true;

	}

	
}
