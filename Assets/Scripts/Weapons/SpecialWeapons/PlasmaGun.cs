using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : MonoBehaviour
{
	[SerializeField] private Weapon wp;
	[SerializeField] private GameObject plasma;
	private GameObject plasmaObj;
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
	private	void Shoot()
	{
        wp.PlaySound(wp.loadout[wp.currentWeaponIndex].shootSound);
		GameObject muzzle = GameObject.Find("Muzzle");
		plasmaObj = Instantiate(plasma, muzzle.transform);
		plasmaObj.transform.parent = null;
		Destroy(plasmaObj, 0.5f);



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
		wp.Recoil();



		CameraShake.Instance.CamShake(0.4f, 0.3f);
		wp.AmmoSlider.value = wp.loadout[wp.currentWeaponIndex].curAmmo;



		wp.gunCurrentAmmoText.text = wp.loadout[wp.currentWeaponIndex].curAmmo.ToString();
		StartCoroutine(Shooting());
		wp.loadout[wp.currentWeaponIndex].canShoot = false;
	}
	IEnumerator Shooting()
	{
		yield return new WaitForSeconds(60 / wp.loadout[wp.currentWeaponIndex].fireRate);
		wp.loadout[wp.currentWeaponIndex].canShoot = true;
	}
}
