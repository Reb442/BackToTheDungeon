using UnityEngine;

public class Recoil : MonoBehaviour
{
	public Weapon weapon;
	private Vector3 currentRotation;
	private Vector3 targetRotation;

	private float recoilX,recoilY,recoilZ;
	[SerializeField] private float snappiness, returnSpeed;

	private void Update()
	{
		if (weapon.loadout[weapon.currentWeaponIndex] != null)
		{
			recoilX = -weapon.loadout[weapon.currentWeaponIndex].recoilPower;
			recoilY = weapon.loadout[weapon.currentWeaponIndex].recoilPower;
			targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
			currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
			transform.localRotation = Quaternion.Euler(currentRotation);
		}
	}

	public void RecoilFire()
	{
		if (weapon.aiming)
		{
			targetRotation += new Vector3(recoilX/2, Random.Range(-recoilY / 1.5f, recoilY / 1.5f), Random.Range(-recoilZ / 2, recoilZ / 2));
		}
		else
		{
			targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
		}
	}
}
