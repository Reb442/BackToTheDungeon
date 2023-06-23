using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
	[SerializeField] private Weapon wp;
	[SerializeField] private GameObject Grenade;
	[SerializeField] private float GrenadeSpeed;
	[SerializeField] private float ShakeStrength;
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

	void Shoot()
	{
        
        wp.PlaySound(wp.loadout[wp.currentWeaponIndex].shootSound);
        GameObject muzzle = GameObject.Find("Muzzle");
		GameObject grenade = Instantiate(Grenade,muzzle.transform);
		grenade.transform.parent = null;
		grenade.GetComponent<Rigidbody>().AddForce(muzzle.transform.forward * GrenadeSpeed, ForceMode.Impulse);
		wp.Recoil();
		wp.loadout[wp.currentWeaponIndex].curAmmo--;
		CameraShake.Instance.CamShake(0.4f, 0.3f); ;
		wp.AmmoSlider.value = wp.loadout[wp.currentWeaponIndex].curAmmo;
		wp.gunCurrentAmmoText.text = wp.loadout[wp.currentWeaponIndex].curAmmo.ToString();
	}
}
