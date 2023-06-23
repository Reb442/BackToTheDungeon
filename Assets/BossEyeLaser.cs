using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeLaser : MonoBehaviour
{
    GameObject target;
    public GameObject LaserEffect;
    public float turnSpeed;
    public AudioSource audioSource;
	RaycastHit hit;
	private void Awake()
	{
		target = GameObject.Find("Player");
        if (gameObject.activeSelf)
        {
			InvokeRepeating("Shoot", 0f, .2f);
		}
	}

	void Update()
    {
        if(!audioSource.isPlaying) { 
        audioSource.Play();
        }
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((target.transform.position - transform.position).normalized), turnSpeed);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 200))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                PlayerHealth.Instance.TakeDamage(1);
            }
        }
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
			var effect = Instantiate(LaserEffect, transform.position, transform.rotation);
			Destroy(effect, 2f);
            yield return new WaitForSeconds(.5f);
            i++;
		}
    }
}
