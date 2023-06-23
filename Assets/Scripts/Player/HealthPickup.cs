using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] private AnimationCurve curve;

    bool isfuck;
    GameObject player;
    float multi = 0;

    private void LateUpdate()
    {
        if (isfuck)
        {
            multi += Time.deltaTime * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, curve.Evaluate(multi));
            if (Vector3.Distance(player.transform.position, this.transform.position) < 0.55f)
            {
				PlayerHealth.Instance.AddHealth(30);
                Destroy(this.gameObject);
            }
        }
            
        
    }
    private void OnTriggerEnter(Collider other)
    {

    if (other.gameObject.CompareTag("Player") && PlayerHealth.Instance.GetHealth() < PlayerHealth.Instance.HealthMax)
    {
        isfuck = true;
            player = other.gameObject;  
    }

    }

}
