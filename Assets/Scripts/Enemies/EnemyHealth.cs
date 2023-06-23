using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int HealthMax;
    float CurHealth;
    //bool isDead;
    public GameObject HealthDrop;
    void Start() => CurHealth = HealthMax;

    private void LateUpdate()
    {
        if (CurHealth <= 0) {

            //isDead = true;
            PlayerUse.Instance.money++;

            Instantiate(HealthDrop, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }
    public void TakeDamage(float amount)
    {
        CurHealth -= amount;
    }
}
