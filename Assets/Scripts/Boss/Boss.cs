using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss Instance;

    [HideInInspector]public Animator animator;
	public Transform BossRightHand;
    public bool ChooseAttack;



	[Header("SmashAttack")]
	public GameObject smashCircleObj;
    public float lifeTime;

    [Header("ShootingAttack")]
    public bool shootingAttack;

    [Header("EnemySpawnAttack")]
    public bool enemySpawnAttack;

	void Awake() => Instance = this;

	void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (ChooseAttack)
        {

        }   
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
        {
            animator.enabled = true;
            animator.SetTrigger("Smash");
        }
	}
    void AnimSmashAttack()
    {
        GameObject smashCircle = Instantiate(smashCircleObj,BossRightHand.position,Quaternion.identity);
        CameraShake.Instance.CamShake();
        Destroy(smashCircle,lifeTime);
    }
}
