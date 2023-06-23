using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableObject : MonoBehaviour,IBuy
{
    public int Cost;
    public Gun WeaponSO;
    [SerializeField] private BuyableType Type;
    public enum BuyableType
    {
        Ammo,
        HealthUpgrade,
        ArmorUpgrade,
        Weapon
    }
    public void Use(Weapon wp)
    {

        switch (Type)
        {
            case BuyableType.Ammo:
                var playerobj = wp.loadout[wp.currentWeaponIndex];

                playerobj.ReserveAmmo += playerobj.ReserveAmmoMax - playerobj.ReserveAmmo;
                wp.ReserveAmmoText.text = playerobj.ReserveAmmo.ToString();
                break;

            case BuyableType.HealthUpgrade:

                break;
            case BuyableType.ArmorUpgrade:

                break;
            case BuyableType.Weapon:

                if (wp.loadout[1] == null)
                {   //empty offhand
                    wp.loadout[1] = WeaponSO;
                }
                else
                {
                    var go = Instantiate(wp.loadout[wp.currentWeaponIndex].WeaponPickupPrefab, wp.player.PlayerCamera.transform.position, Quaternion.identity, transform.parent = null);
                    go.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5, ForceMode.Impulse);
                    wp.loadout[wp.currentWeaponIndex] = WeaponSO;
                    wp.Equip(wp.currentWeaponIndex);
                }
                break;

            default:
                Debug.LogWarning("ERROR");
                break;

        }
        wp.GetComponent<PlayerUse>().money -= Cost;
        Destroy(this.gameObject);
    }

}

