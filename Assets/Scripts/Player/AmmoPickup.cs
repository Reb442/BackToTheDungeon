using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;

    bool pickedUp;
    GameObject player;
    float multi = 0;

    private void LateUpdate()
    {
        if (pickedUp)
        {
            multi += Time.deltaTime * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, curve.Evaluate(multi));
            if (Vector3.Distance(player.transform.position, this.transform.position) < 0.55f)
            {
                var playerobj = player.GetComponent<Weapon>().loadout[player.GetComponent<Weapon>().currentWeaponIndex];

                playerobj.ReserveAmmo += playerobj.ReserveAmmoMax - playerobj.ReserveAmmo;
                player.GetComponent<Weapon>().ReserveAmmoText.text = playerobj.ReserveAmmo.ToString();
                Destroy(this.gameObject);
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        var Weapon = other.GetComponent<Weapon>();

        if (other.gameObject.CompareTag("Player") && Weapon.loadout[Weapon.currentWeaponIndex].ReserveAmmo < Weapon.loadout[Weapon.currentWeaponIndex].ReserveAmmoMax)
        {
            pickedUp = true;
            player = other.gameObject;
        }

    }
}
