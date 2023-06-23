using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    public GameObject deathMenu;
    public PlayerMove playerMove;
    public Weapon weapon;
    public Gun pistol;

    public int HealthMax;
    public int ArmorMax;

    float ArmorRegenRate = 5f;
    float ArmorRegenCountdown = 5f;
    float ArmorRegenCountdownTEMP;
    bool canRegenArmor = false;

    float CurHealth;
    float curArmor;

    public Slider HealthSlider;
    public Slider ArmorSlider;

	[SerializeField] private TextMeshProUGUI healthText;
	[SerializeField] private TextMeshProUGUI armorText;

	void Awake() { 
    Instance = this;

        SetHealth(HealthMax);
        SetArmor(ArmorMax);
        HealthSlider.maxValue = HealthMax;
        ArmorSlider.maxValue = ArmorMax;
        ArmorRegenCountdownTEMP = ArmorRegenCountdown;
        healthText.text = CurHealth.ToString();
        armorText.text = curArmor.ToString();

    }
    private void LateUpdate()
    {
        RegenArmor();

        HealthSlider.value = GetHealth();
        ArmorSlider.value = GetArmor();
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(30);
        }
    }

    void RegenArmor()
    {
        if (canRegenArmor)
        {
            ArmorRegenCountdownTEMP -= Time.deltaTime;
            
            if (ArmorRegenCountdownTEMP <= 0)
            {
                if(curArmor < ArmorMax)
                {
                    AddArmor(ArmorRegenRate * Time.deltaTime);
                }
                else
                {
                    ArmorRegenCountdownTEMP = ArmorRegenCountdown;
                    canRegenArmor = false;
                }
                
            }
        }
		armorText.text = ((int)curArmor).ToString();
	}
    public void TakeDamage(int Amount)
    {
        if (curArmor > 0)
        {
            if (curArmor < Amount)
            {
                curArmor -= curArmor;
            }
            else
            {
                curArmor -= Amount;
            }
        }
        else
        {
            
            CurHealth -= Amount;
            if (CurHealth <= 0)
            {
                Die();
            }

        }
        ArmorRegenCountdownTEMP = ArmorRegenCountdown;
        canRegenArmor = true;
		healthText.text = ((int)CurHealth).ToString();
		armorText.text = ((int)curArmor).ToString();
	}

    void Die()
    {

		Invoke("DeathButton", 2f);
		deathMenu.SetActive(true);
		//Time.timeScale = 0;
        playerMove.enabled = false;
        weapon.enabled = false;
        
	}
    void SetHealth(int value)
    {
        CurHealth = value;
    }
    public void AddHealth(int value)
    {
        if(CurHealth + value > HealthMax)
        {
            CurHealth = HealthMax;
        }
        else
        CurHealth += value;
		healthText.text = ((int)CurHealth).ToString();
	}
    public void AddArmor(float value)
    {
        curArmor += value;
		armorText.text = ((int)curArmor).ToString();
	}
    void SetArmor(int value)
    {
        curArmor = value;   
    }
    float GetArmor()
    {
        return curArmor;
    }
    public float GetHealth()
    {
        return CurHealth;
    }
    public void DeathButton()
    {
        StartCoroutine(SceneChange(1));
        CurHealth = HealthMax;
        curArmor = ArmorMax;
		healthText.text = CurHealth.ToString();
		armorText.text = curArmor.ToString();
		deathMenu.SetActive(false);
		playerMove.enabled = true;
		weapon.enabled = true;
        weapon.loadout[0] = pistol;
        weapon.loadout[0].curAmmo = weapon.loadout[0].curAmmoMax;
        weapon.loadout[0].ReserveAmmo = weapon.loadout[0].ReserveAmmoMax;

		if (weapon.loadout[1] != null)
        {
            weapon.loadout[1] = null;

		}
	}
	IEnumerator SceneChange(int index)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
		while (!asyncLoad.isDone)
		{

			yield return null;

		}

	}

}
