using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float triggerForce = 0.5f;
    [SerializeField] private float explosiveRadius = 5;
    [SerializeField] private float explosiveForce = 500;
	[SerializeField] private Gun gun;
    //[SerializeField] private GameObject particles;
	

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude >= triggerForce)
		{
			var surroundingObjects = Physics.OverlapSphere(transform.position, explosiveRadius);

			foreach (var obj in surroundingObjects)
			{
                Debug.Log("62");
                var rb = obj.GetComponent<Rigidbody>();
				if (rb == null) continue;

				rb.AddExplosionForce(explosiveForce, transform.position, explosiveRadius);
			}
            foreach (var obj in surroundingObjects)
            {
                if (obj.CompareTag("Enemy"))
                {
                    Debug.Log("31");
                    obj.transform.gameObject.GetComponent<AiHealth>().TakeDamage(gun.damage);
                }
                if (obj.CompareTag("BossEye"))
                {
                    obj.transform.gameObject.GetComponent<BossEyeHealth>().TakeDamage(gun.damage);
                    
                }
                if (obj.CompareTag("BossHead"))
                {
                    obj.transform.gameObject.GetComponent<BossHeadHealth>().TakeDamage(gun.damage);
                    
                }
            }
            ////Instantiate(particles,transform.position, Quaternion.identity);

            Destroy(gameObject);
		}
	}
}
