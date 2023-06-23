using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualPistol : MonoBehaviour
{
	[SerializeField] private Weapon wp;
	[SerializeField] private GameObject pistol1, pistol2;
	bool righthoot = true;
	private void Awake()
	{
		wp = GameObject.Find("Player").GetComponent<Weapon>();
		wp.AmmoSlider.maxValue = wp.loadout[wp.currentWeaponIndex].curAmmoMax;

	}
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && wp.loadout[wp.currentWeaponIndex].curAmmo > 0 && !wp.loadout[wp.currentWeaponIndex].reloading && wp.loadout[wp.currentWeaponIndex].canShoot)
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		GameObject muzzle1 = GameObject.Find("Muzzle1");
		GameObject muzzle2 = GameObject.Find("Muzzle2");

		
		if (righthoot)
		{
			var bull = Instantiate(wp.loadout[wp.currentWeaponIndex].bullet, muzzle1.transform.position, muzzle1.transform.rotation);
			bull.transform.GetComponent<Rigidbody>().AddForce(bull.transform.forward * 100, ForceMode.Impulse);
			righthoot = false;
		}
		else
		{
			var bull = Instantiate(wp.loadout[wp.currentWeaponIndex].bullet, muzzle2.transform.position, muzzle2.transform.rotation);
			bull.transform.GetComponent<Rigidbody>().AddForce(bull.transform.forward * 100, ForceMode.Impulse);
			righthoot = true;
		}


		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50, wp.canBeShot))
		{

			if (hit.collider.gameObject.CompareTag("Enemy"))
			{
				hit.transform.gameObject.GetComponent<AiHealth>().TakeDamage(wp.loadout[wp.currentWeaponIndex].damage);
				wp.AudioSource.Play();
				wp.hitMark.SetActive(true);
			}
            if (hit.collider.gameObject.CompareTag("BossEye"))
            {
                hit.transform.gameObject.GetComponent<BossEyeHealth>().TakeDamage(wp.loadout[wp.currentWeaponIndex].damage);
                wp.hitMark.SetActive(true);
            }
            if (hit.collider.gameObject.CompareTag("BossHead"))
            {
                hit.transform.gameObject.GetComponent<BossHeadHealth>().TakeDamage(wp.loadout[wp.currentWeaponIndex].damage);
                wp.hitMark.SetActive(true);
            }

        }
		wp.loadout[wp.currentWeaponIndex].curAmmo--;
		StartCoroutine(Shooting());

		wp.AmmoSlider.value = wp.loadout[wp.currentWeaponIndex].curAmmo;

		wp.gunCurrentAmmoText.text = wp.loadout[wp.currentWeaponIndex].curAmmo.ToString();
		wp.loadout[wp.currentWeaponIndex].canShoot = false;
	}
	IEnumerator Shooting()
	{
		wp.Recoil();
		yield return new WaitForSeconds(60 / wp.loadout[wp.currentWeaponIndex].fireRate);
		wp.loadout[wp.currentWeaponIndex].canShoot = true;
	}
}
