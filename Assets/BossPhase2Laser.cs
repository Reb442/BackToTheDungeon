using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2Laser : MonoBehaviour
{
	GameObject target;
	public GameObject Laser;
	public float turnSpeed;
	public AudioSource audioSource;
	private void Awake()
	{
		target = GameObject.Find("Player");
		if (gameObject.activeSelf)
		{
			InvokeRepeating("Shoot", 0f, 1f);
		}
	}

	void Update()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((target.transform.position - transform.position).normalized), turnSpeed);
	}
	void Shoot()
	{
		if (gameObject.activeSelf)
		{
			StartCoroutine(ShootLaser());
		}
	}

	IEnumerator ShootLaser()
	{
		int i = 0;
		while (i < 3)
		{
			audioSource.Play();
            var laser = Instantiate(Laser, transform.position, transform.rotation);
			laser.GetComponent<Rigidbody>().AddForce(transform.forward * 500,ForceMode.Impulse);
			Destroy(laser, 10f);
			yield return new WaitForSeconds(.2f);
			i++;
		}
	}
}
