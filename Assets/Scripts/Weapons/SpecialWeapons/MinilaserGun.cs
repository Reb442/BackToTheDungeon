using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MinilaserGun : MonoBehaviour
{
	[SerializeField]private Weapon wp;
	[SerializeField]private GameObject laser;
	[SerializeField] private float strength;
	private GameObject laserObj;
	private void Awake()
	{
		wp = GameObject.Find("Player").GetComponent<Weapon>();
		wp.AmmoSlider.maxValue = wp.loadout[wp.currentWeaponIndex].curAmmoMax;
	}
	void Update()
	{
		GameObject muzzle = GameObject.Find("Muzzle");
		if (Input.GetMouseButton(0) && wp.loadout[wp.currentWeaponIndex].curAmmo > 0 && !wp.loadout[wp.currentWeaponIndex].reloading && wp.loadout[wp.currentWeaponIndex].canShoot)
		{
			Shoot();
		}
		if (Input.GetMouseButtonDown(0) && wp.loadout[wp.currentWeaponIndex].curAmmo > 0)
		{
			laserObj = Instantiate(laser, muzzle.transform);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Destroy(laserObj,0.5f);
		}
		else if (wp.loadout[wp.currentWeaponIndex].curAmmo <= 0)
		{
			Destroy(laserObj, 0.5f);
		}
	}

	void Shoot()
	{
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, wp.loadout[wp.currentWeaponIndex].maxDistance, wp.canBeShot))
	    {
			if(hit.transform.CompareTag("Enemy"))
			{
                hit.transform.gameObject.GetComponent<AiHealth>().TakeDamage(wp.loadout[wp.currentWeaponIndex].damage);
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

		if (!wp.AudioSource.isPlaying)
		{
            wp.PlaySound(wp.loadout[wp.currentWeaponIndex].shootSound);
        }

        wp.loadout[wp.currentWeaponIndex].curAmmo--;
		DOTween.Rewind(transform);
		DOTween.Rewind(Camera.main);
		transform.DOShakePosition(0.1f, strength / 100, 10, 45, false, true);
		Camera.main.DOShakePosition(0.1f, strength / 10, 10, 45, false);
		wp.AmmoSlider.value = wp.loadout[wp.currentWeaponIndex].curAmmo;
		wp.gunCurrentAmmoText.text = wp.loadout[wp.currentWeaponIndex].curAmmo.ToString();
	}
}
