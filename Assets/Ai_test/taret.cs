using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class taret : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject topG_Center;
    [SerializeField] private GameObject bottomG_Center;
    [SerializeField] private GameObject topG_nuzzle;
    [SerializeField] private GameObject bottomG_nuzzle;
    public Transform playerTransform;
    [SerializeField] private float attackRange = 50;
    [SerializeField] private bool TopcanShot =true, BottoncanShot=true;
    [SerializeField] private float TlaunchForce, BlaunchForce;
    [SerializeField] private float topRoF, BottonRoF;
    [SerializeField] private AudioSource audioSourceTOP;
    [SerializeField] private AudioSource audioSourceBOTTOM;
    [SerializeField] public bool live = true;
    [SerializeField] public float currentHealth = 100f;
    public GameObject ammop;
    public GameObject healthp;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        TopcanShot = true;
        BottoncanShot = true;
    }


    // Update is called once per frame
    void Update()
    {

        if (live)
        {
            Vector3 direction = playerTransform.position - transform.position;
            if (direction.magnitude < attackRange)
            {
                topG_Center.transform.LookAt(playerTransform.position);
                bottomG_Center.transform.LookAt(playerTransform);
                if (TopcanShot == true)
                {
                    StartCoroutine(topShoot(topG_nuzzle, TlaunchForce, topRoF));
                    TopcanShot = false;
                }
                if (BottoncanShot == true)
                {
                    StartCoroutine(bottonShoot(bottomG_nuzzle, BlaunchForce, BottonRoF));
                    BottoncanShot = false;
                }
            }
        }
    }
    
    IEnumerator topShoot(GameObject nuzzle,float launchForce,float rateOfFire)
    {
       
            audioSourceTOP.Play();

        GameObject bullet = Instantiate(bulletPrefab, nuzzle.transform.position, nuzzle.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(nuzzle.transform.forward * launchForce, ForceMode.Impulse);
        yield return new WaitForSeconds(rateOfFire);
        TopcanShot = true;
    }

    IEnumerator bottonShoot(GameObject nuzzle, float launchForce, float rateOfFire)
    {
        
            audioSourceBOTTOM.Play();

        GameObject bullet = Instantiate(bulletPrefab, nuzzle.transform.position, nuzzle.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(nuzzle.transform.forward * launchForce, ForceMode.Impulse);
        yield return new WaitForSeconds(rateOfFire);
        BottoncanShot = true;
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            live = false;
        }
    }
    IEnumerator itemDrop()
    {
        yield return new WaitForSeconds(4f);
        float randomValue = Random.value;
        float pRandomValue = Random.value;
        if (randomValue <= 0.4f)
        {
            if (pRandomValue <= 0.5f)
            {
                GameObject ammopup = Instantiate(ammop, transform.position + Vector3.up, transform.rotation);
                ammopup.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
            else
            {
                GameObject healtpup = Instantiate(healthp, transform.position + Vector3.up, transform.rotation);
                healtpup.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
        }
        else
        {

        }
    }
}
