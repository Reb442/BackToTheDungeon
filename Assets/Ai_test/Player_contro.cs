using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_contro : MonoBehaviour
{
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
    }
}
