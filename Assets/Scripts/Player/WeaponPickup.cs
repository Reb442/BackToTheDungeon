using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponPickup : MonoBehaviour, IWeaponPickup
{

    public Gun WeaponSO;
    [SerializeField] private TextMeshPro weaponName,weaponDamage,WeaponFireRate,WeaponAmmo;

    public void Start()
    {
        weaponName.text = WeaponSO.name;
        weaponDamage.text = "Damage " + WeaponSO.damage.ToString();
        WeaponFireRate.text = "FireRate " +WeaponSO.fireRate.ToString();
        WeaponAmmo.text = "Ammo " +WeaponSO.curAmmoMax.ToString() + " / " + WeaponSO.ReserveAmmoMax.ToString();

    }
    public void Use(Weapon wp)
    {
        if(wp.loadout[1] == null)
        {   //empty offhand
            wp.loadout[1] = WeaponSO;
        }
        else
        {  
            var go = Instantiate(wp.loadout[wp.currentWeaponIndex].WeaponPickupPrefab, wp.player.PlayerCamera.transform.position, Quaternion.identity,transform.parent = null);
            go.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5, ForceMode.Impulse);
            wp.loadout[wp.currentWeaponIndex] = WeaponSO;
            wp.Equip(wp.currentWeaponIndex);
        }
       
        Destroy(this.gameObject);
    }
    public void Use()
    {

    }
}
