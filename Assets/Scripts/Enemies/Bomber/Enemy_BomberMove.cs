using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BomberMove : MonoBehaviour
{
	private GameObject player;
	[SerializeField] private Enemy_BomberAttack enemy;
	void Start()
    {
        player = GameObject.FindAnyObjectByType<PlayerMove>().gameObject;
    }

    void Update()
    {
		if (enemy.playerInRange)
		{
			transform.LookAt(player.transform.position);
		}
	}
}
