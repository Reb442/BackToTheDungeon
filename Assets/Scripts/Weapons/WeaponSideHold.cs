using UnityEngine;

public class WeaponSideHold : MonoBehaviour
{
    public float checkDistance;
    public Vector3 newDirection;
    float lerpPos;
    
    

    private void Update()
    {
        
            if (Vector3.Distance(PlayerUse.Instance.hit.transform.position, this.transform.position) < checkDistance)
            {
                lerpPos = 1 - (PlayerUse.Instance.hit.distance / checkDistance);
                Debug.Log("yes");
            }
            else
            {
                Debug.Log("no");
                lerpPos = 0;
            }
            Mathf.Clamp01(lerpPos);

            transform.localRotation =
                Quaternion.Lerp(
                Quaternion.Euler(Vector3.zero),
                Quaternion.Euler(newDirection),
                lerpPos
                );
        
        
    }
}
