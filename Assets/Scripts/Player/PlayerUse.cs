using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUse : MonoBehaviour
{
    public static PlayerUse Instance;
    public PlayerMove player;
    public Weapon weapon;

    public RaycastHit hit;

    public int money;
    public TextMeshProUGUI MoneyText;

    public GameObject selection = null;
    
    public void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (Physics.Raycast(player.PlayerCamera.transform.position, player.PlayerCamera.transform.forward, out hit, 3f))
        {

            if (hit.transform.CompareTag("PickupWeapon"))
            {
                selection = hit.transform.gameObject;
                selection.transform.GetChild(0).gameObject.SetActive(true);

            }
            else
            {
                //selection.transform.GetChild(0).gameObject.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (hit.collider.gameObject.tag)
                {
                    case "PickupWeapon":
                        hit.collider.GetComponent<IWeaponPickup>().Use(weapon);
                        break;
                    case "Buyable":
                        
                        hit.collider.GetComponent<BuyableObject>().Use(weapon);
                        break;
                }

            }
        }
        else if (selection == null)
        {

        }
        else
        {
            selection.transform.GetChild(0).gameObject.SetActive(false);
        }
        //MoneyText.text = money.ToString();
    }
    
}
