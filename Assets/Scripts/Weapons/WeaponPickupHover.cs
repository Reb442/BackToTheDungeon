using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupHover : MonoBehaviour
{
    
    public float rotationSpeed = 10f;
    
    
    void LateUpdate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

    }
}
