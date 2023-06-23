using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float intensity;
    public float smooth;

    private Quaternion originRotation;

	private void Start()
	{
		originRotation = transform.localRotation;
	}
	private void Update()
	{
		UpdateSway();
	}

	void UpdateSway()
	{
		float xMouse = Input.GetAxis("Mouse X");
		float yMouse = Input.GetAxis("Mouse Y");

		Quaternion xAdj = Quaternion.AngleAxis(-intensity*xMouse, Vector3.up);
		Quaternion yAdj = Quaternion.AngleAxis(intensity*yMouse, Vector3.up);
		Quaternion targetRotation  = originRotation * xAdj * yAdj;

		transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smooth*Time.deltaTime);

	}
}
