using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")] 
public class Gun : ScriptableObject
{
    public void OnEnable()
    {
        ReserveAmmo = ReserveAmmoMax;
        curAmmo = curAmmoMax;
        reloading = false;
        canShoot = true;
    }
    public enum FireMode { singleFire, automaticFire}
    [Header("Info")]
    public new string name;
    public bool specialGun;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;
	public float fireRate;
    public float recoilPower;

    [Header("Reloading")]
    
    public FireMode fireMode;
    
    public float aimSpeed;
    public float reloadTime;

    public int ReserveAmmo;
    public int ReserveAmmoMax;
    public int curAmmo;
    public int curAmmoMax;


    
    public bool reloading = false;
    public bool canShoot = true;


    public GameObject prefab;
    public GameObject WeaponPickupPrefab;
    public GameObject bullet;
    public AudioClip shootSound;
	public AudioClip reloadSound;
}
