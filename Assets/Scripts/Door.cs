using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
	public GameObject e;
	private void Awake()
	{
		anim = GetComponent<Animator>();
	}
	public void DoorOpen()
    {
		anim.SetTrigger("DoorOpen");
    }

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player")) //gm isopenables
		{
			e.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				DoorOpen();
				
			}
		}
	}
}
