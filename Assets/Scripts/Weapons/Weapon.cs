using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Gun[] loadout;
    public Transform hand;
	private Animator anim;

	[Header("Scripts")]
	public PlayerMove player;
	public Recoil recoil;

	private Vector3 target;
	public LayerMask canBeShot;

	private Transform anchor;
	public bool aiming;

	public GameObject hitMark;
	public AudioClip HitmarkerSound;
	public AudioSource AudioSource;
	
	public Slider AmmoSlider;

	[Header("Texts")]
	public TextMeshProUGUI gunNameText;
	public TextMeshProUGUI gunCurrentAmmoText;
	public TextMeshProUGUI ReserveAmmoText;

	public GameObject currentWeapon;
	public int currentWeaponIndex;

	public GameObject Bullet;

	private void Start()
	{
        Equip(0);
        

		
	}
	private void Update()
	{
		if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.mouseScrollDelta.y > 0) && !loadout[currentWeaponIndex].reloading)
        {
            Equip(0);
        }
		if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.mouseScrollDelta.y < 0) && loadout[1] != null && !loadout[currentWeaponIndex].reloading)
		{
			Equip(1);
		}

		if (currentWeapon != null)
		{
			Aim(Input.GetMouseButton(1));
			if (!loadout[currentWeaponIndex].specialGun)
			{
				if (loadout[currentWeaponIndex].fireMode.ToString() == "singleFire")
				{
					if (Input.GetMouseButtonDown(0) && !loadout[currentWeaponIndex].reloading && loadout[currentWeaponIndex].canShoot && loadout[currentWeaponIndex].curAmmo > 0)
					{
						recoil.RecoilFire();
						Shoot();

					}

                }
				else
				{
					if (Input.GetMouseButton(0) && !loadout[currentWeaponIndex].reloading && loadout[currentWeaponIndex].canShoot && loadout[currentWeaponIndex].curAmmo > 0)
					{
						recoil.RecoilFire();
						Shoot();

					}
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.R) && loadout[currentWeaponIndex].canShoot && !loadout[currentWeaponIndex].reloading && loadout[currentWeaponIndex].curAmmo < loadout[currentWeaponIndex].curAmmoMax && loadout[currentWeaponIndex].ReserveAmmo > 0)
		{
			StartCoroutine(Reloading());
		}

		if (loadout[currentWeaponIndex].curAmmo <= 0 && !loadout[currentWeaponIndex].reloading && loadout[currentWeaponIndex].curAmmo < loadout[currentWeaponIndex].curAmmoMax && loadout[currentWeaponIndex].ReserveAmmo > 0)
		{
			if (Input.GetMouseButton(0))
			{
				StartCoroutine(Reloading());
			}
		}
        

    }
	
	public void Shoot()
	{
		GameObject muzzle = GameObject.Find("Muzzle");
		RaycastHit hit = new RaycastHit();
		var bull = Instantiate(Bullet, muzzle.transform.position, muzzle.transform.rotation, transform.parent = null);
		bull.transform.GetComponent<Rigidbody>().AddForce(bull.transform.forward * 100, ForceMode.Impulse);
		if (Physics.Raycast(player.PlayerCamera.transform.position, player.PlayerCamera.transform.forward,out hit, loadout[currentWeaponIndex].maxDistance,canBeShot))
		{
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
				hit.transform.gameObject.GetComponent<AiHealth>().TakeDamage(loadout[currentWeaponIndex].damage);
				hitMark.SetActive(true);
			}
            if (hit.collider.gameObject.CompareTag("BossEye"))
            {
                hit.transform.gameObject.GetComponent<BossEyeHealth>().TakeDamage(loadout[currentWeaponIndex].damage);
                hitMark.SetActive(true);
            }
            if (hit.collider.gameObject.CompareTag("BossHead"))
            {
                hit.transform.gameObject.GetComponent<BossHeadHealth>().TakeDamage(loadout[currentWeaponIndex].damage);
                hitMark.SetActive(true);
            }
        }
		else
		{
			Destroy(bull, 1);

        }

		loadout[currentWeaponIndex].curAmmo--;
		AmmoSlider.value = loadout[currentWeaponIndex].curAmmo;
		gunCurrentAmmoText.text = loadout[currentWeaponIndex].curAmmo.ToString();
		StartCoroutine(Shooting());
		loadout[currentWeaponIndex].canShoot = false;
    }
    
	public  void Equip(int weapon_id)
	{
		if (currentWeapon != null)
		{
			Destroy(currentWeapon);
		}
		currentWeaponIndex = weapon_id;
		GameObject newWeapon = Instantiate(loadout[weapon_id].prefab,hand.position,hand.rotation,hand) as GameObject;
		newWeapon.transform.localPosition = Vector3.zero;
		newWeapon.transform.localEulerAngles = Vector3.zero;

		currentWeapon = newWeapon;
		anim = newWeapon.GetComponentInChildren<Animator>();
		anim.SetTrigger("Equip");

        gunNameText.text = loadout[currentWeaponIndex].name;
        gunCurrentAmmoText.text = loadout[currentWeaponIndex].curAmmo.ToString();
        ReserveAmmoText.text = loadout[currentWeaponIndex].ReserveAmmo.ToString();
		AmmoSlider.maxValue = loadout[currentWeaponIndex].curAmmoMax;
		AmmoSlider.value = loadout[currentWeaponIndex].curAmmo;
	}
	private void Aim(bool isAiming)
	{
		aiming = isAiming;
		anchor = currentWeapon.transform.Find("Anchor");
		Transform hip = currentWeapon.transform.Find("States/Hip");
		Transform ads = currentWeapon.transform.Find("States/ADS");

		if (isAiming)
		{
			anchor.position = Vector3.Lerp(anchor.position, ads.position, loadout[currentWeaponIndex].aimSpeed * Time.deltaTime);
			target = ads.position;
			player.lookSpeedX = 0.5f;
			player.lookSpeedY = 0.5f;
		}
		else
		{
			anchor.position = Vector3.Lerp(anchor.position, hip.position, loadout[currentWeaponIndex].aimSpeed * Time.deltaTime);
			target = hip.position;
			player.lookSpeedX = 2f;
			player.lookSpeedY = 2f;
		}
		
	}
	private void Reload()
	{
		
		var ammoNeeded = loadout[currentWeaponIndex].curAmmoMax - loadout[currentWeaponIndex].curAmmo;

		if(ammoNeeded <= loadout[currentWeaponIndex].ReserveAmmo)
		{
			loadout[currentWeaponIndex].curAmmo += ammoNeeded;
			loadout[currentWeaponIndex].ReserveAmmo -= ammoNeeded;
        }
        else if (ammoNeeded > loadout[currentWeaponIndex].ReserveAmmo)
        {
            loadout[currentWeaponIndex].curAmmo += loadout[currentWeaponIndex].ReserveAmmo;
			loadout[currentWeaponIndex].ReserveAmmo = 0;
        }
		AmmoSlider.value = loadout[currentWeaponIndex].curAmmo;
		gunCurrentAmmoText.text = loadout[currentWeaponIndex].curAmmo.ToString();
        ReserveAmmoText.text = loadout[currentWeaponIndex].ReserveAmmo.ToString();
    }

    
	public void Recoil()
	{
		anchor.transform.localPosition -= Vector3.forward * (loadout[currentWeaponIndex].recoilPower/10);
		anchor.position = Vector3.Lerp(anchor.position, target,Time.deltaTime);
	}
	IEnumerator Shooting()
	{
		anim.SetTrigger("Shoot");
        PlaySound(loadout[currentWeaponIndex].shootSound);
        Recoil();
		yield return new WaitForSeconds(60/loadout[currentWeaponIndex].fireRate);
		loadout[currentWeaponIndex].canShoot = true;
	}
	IEnumerator Reloading()
	{
		anim.SetTrigger("Reload");
		PlaySound(loadout[currentWeaponIndex].reloadSound);

       loadout[currentWeaponIndex].reloading = true;
		yield return new WaitForSeconds(loadout[currentWeaponIndex].reloadTime);
		Reload();
		loadout[currentWeaponIndex].reloading= false;
	}
	public void PlaySound(AudioClip _clip)
	{
        AudioSource.clip = _clip;
        AudioSource.Play();
    }
}
