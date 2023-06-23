using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject replacement;
    [SerializeField] private float breakForce = 2;
    [SerializeField] private float collisionMultipiler = 100;
    [SerializeField] private bool broken;

	private void OnCollisionEnter(Collision collision)
	{
		if (broken) return;
		if (collision.relativeVelocity.magnitude >= breakForce)
		{
			broken = true;
			var _replacement = Instantiate(replacement, transform.position,transform.rotation);

			var rbs = replacement.GetComponentsInChildren<Rigidbody>();
			foreach(var rb in rbs)
			{
				rb.AddExplosionForce(collision.relativeVelocity.magnitude * collisionMultipiler, collision.contacts[0].point, 2);
			}

			Destroy(gameObject);
		}
	}
}
