using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    [SerializeField] private GameObject Hand;

    public float duration;
    public float strength;
	void Awake() => Instance = this;
	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.L))
        {
            CamShake();
        }
	}
	public void CamShake()
    {
		DOTween.Rewind(Camera.main);
		DOTween.Rewind(Hand.transform);
		Camera.main.DOShakePosition(duration, strength,fadeOut:true);
		Hand.transform.DOShakePosition(duration, strength / 10);
	}
    public void CamShake(float duration,float strength)
    {
        DOTween.Rewind(Camera.main);
		DOTween.Rewind(Hand.transform);
		Camera.main.DOShakePosition(duration, strength, fadeOut: true);
		Hand.transform.DOShakePosition(duration, strength / 10);
	}
}
