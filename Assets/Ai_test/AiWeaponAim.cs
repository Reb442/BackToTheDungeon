using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeaponAim : MonoBehaviour
{
    public Transform targetTransform;
    public Transform aimTransform;
    public List<Transform> weaponlist;
    public Vector3 targetOffset;
    public Transform currentTarget;

    private Transform listSWeapon;
    float distanceLimit = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    Vector3 GetTargetPosition()
    {
        Vector3 targetDirection = (targetTransform.position + targetOffset) - aimTransform.position;
        Vector3 aimDirection = aimTransform.forward;
        float blendOut = 0.0f;

        float targetDistance = targetDirection.magnitude;
        if(targetDistance < distanceLimit)
        {
            blendOut += distanceLimit - targetDistance; 
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return aimTransform.position + direction;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(aimTransform == null)
        {
            return;
        }
        if(targetTransform == null)
        {
            return;
        }

        Vector3 targetPosition = GetTargetPosition();

        for (int i = 0; i < weaponlist.Count;i++)
        {
            listSWeapon = weaponlist[i];
            AimAtTarget(listSWeapon, targetPosition);
        }
    }

    private void AimAtTarget(Transform trailWeapon, Vector3 targetPosition)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        trailWeapon.rotation = aimTowards * trailWeapon.rotation;//
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, 1); 
        trailWeapon.rotation = blendedRotation * trailWeapon.rotation;
    }
    public void SetTargetTransform(Transform target)
    {
        targetTransform = target;
    }
    public void SetAimTransform(Transform aim)
    {
        targetTransform = aim;
    }
    public void SetTarget(Transform target)
    {
        currentTarget = target;
        SetTargetTransform(target);
    }
    public void AiFire(bool enabled)
    {

    }
}
