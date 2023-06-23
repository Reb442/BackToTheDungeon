using System.Collections;
using UnityEngine;
using DG.Tweening;
public class PlayerMove : MonoBehaviour
{
	public bool canMove = true;
	private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
	private bool ShouldJump => Input.GetKey(jumpKey) && controller.isGrounded;
	private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnim && controller.isGrounded && !IsSprinting;
	private bool IsSliding => Input.GetKeyDown(slideKey) && IsSprinting && !sliding && controller.isGrounded;

	[Header("Functional Options")]
	private bool canSprint = true;
	private bool canJump = true;
	private bool canCrouch = true;
	private bool canUseHeadbob = true;
	private bool WillSlideOnSlopes = true;
	private bool isPaused = false;
	public GameObject pauseMenu;


	[Header("Controls")]
	public KeyCode sprintKey = KeyCode.LeftShift;
	public KeyCode jumpKey = KeyCode.Space;
	public KeyCode crouchKey = KeyCode.LeftControl;
	public KeyCode slideKey = KeyCode.C;


	[Header("Movement Parameters")]
	public float walkSpeed = 3f;
	public float sprintSpeed = 6f;
	public float crouchSpeed = 1.5f;
	public float slopeSpeed = 6f;
	public bool sprinting;
	public bool sliding;

	[Header("Jumping Parameters")]
	public float jumpForce = 8f;
	public float gravity = 30f;

	[Header("Crouch Parameters")]
	public float crouchHeight = 0.5f;
	public float standingHeight = 2f;
	public float timeToCrouch = 0.25f;
	public Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
	public Vector3 standingCenter = new Vector3(0, 0, 0);
	private bool isCrouching;
	private bool duringCrouchAnim;

	[Header("HeadBob Parameters")]
	public float walkBobSpeed = 14f;
	public float walkBobAmount = 0.05f;
	public float sprintBobSpeed = 18f;
	public float sprintBobAmount = 0.11f;
	public float crouchBobSpeed = 8f;
	public float crouchBobAmount = 0.025f;
	private float defaultYPos = 0f;
	private float timer;

	[Header("Slide Parameters")]

	private float slideTime;
	public float LengthOfSlide;
	private Vector3 slideDir;
	public float slideSpeed;

	[Header("Look Parameters")]
	[Range(1, 10)] public float lookSpeedX = 2f;
	[Range(1, 10)] public float lookSpeedY = 2f;
	[Range(1, 180)] public float upperLookLimit = 80f;
	[Range(1, 180)] public float lowerLookLimit = 80f;

	//Slope Slide
	private Vector3 hitPointNormal;
	private bool IsSlopeSliding
	{
		get
		{
			if (controller.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f))
			{
				hitPointNormal = slopeHit.normal;
				return Vector3.Angle(hitPointNormal, Vector3.up) > controller.slopeLimit;
			}
			else { return false; }
		}
	}


	public Camera PlayerCamera;
	public GameObject Hand;
	private CharacterController controller;
	private Vector3 moveDirection;
	private Vector2 currentInput;
	private float rotationX = 0;
	[SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Awake()
	{
		controller = GetComponent<CharacterController>();
		defaultYPos = PlayerCamera.transform.localPosition.y;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if(GameObject.Find("SpawnPoint") != null)
		{
			transform.position = GameObject.Find("SpawnPoint").transform.position;
        }
	}
	void Update()
	{ 
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!isPaused)
			{
				Time.timeScale = 0;
				isPaused = true;
				canMove = false;
				pauseMenu.SetActive(true);
			}
			else
			{
				Time.timeScale = 1;
				isPaused = false;
				canMove = true;
				pauseMenu.SetActive(false);
			}
		}
		if (canMove)
		{
			MovementInput();

			MouseLook();

			if (canJump)
				Jump();

			if (canCrouch)
				Crouch();

			if (canUseHeadbob)
				Headbob();

			if (IsSprinting)
			{
				sprinting = true;
			}
			else
			{
				sprinting = false;
			}
			Movements();
			if (IsSliding)
			{
				sliding = true;
				slideDir = moveDirection;
				DoFov(70f);
				controller.height = 0.5f;
				controller.center = crouchingCenter;
				slideTime = LengthOfSlide;
				canUseHeadbob = false;
			}
			

		}

	}
	private void MovementInput()
	{
		currentInput = new Vector2(
		(IsSliding ? slideSpeed : isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"),
		(IsSliding ? slideSpeed : isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal")
		);

		float moveDirectionY = moveDirection.y;
		moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
		moveDirection.y = moveDirectionY;
	}
	private void MouseLook()
	{
		rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
		rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
		PlayerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
		transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
		Hand.transform.rotation = PlayerCamera.transform.rotation;
	}
	private void Jump()
	{
		if (ShouldJump && !isCrouching)
		{
			Debug.Log("jump");
			moveDirection.y = jumpForce;
			if (sliding)
			{
				slideTime = 0;
			}
            if (audioSource.isPlaying)
            {
				audioSource.Stop();
                audioSource.clip = audioClips[0];
                //audioSource.pitch = audioSource.pitch + Random.Range(-0.5f, 0.5f);
                audioSource.Play();

            }else
			{
                audioSource.clip = audioClips[0];
                //audioSource.pitch = audioSource.pitch + Random.Range(-0.5f, 0.5f);
                audioSource.Play();
            }


        }
        
		
    }
	private void Crouch()
	{
		if (ShouldCrouch)
		{
			StartCoroutine(CrouchStand());
		}
	}
	private void Headbob()
	{
		if (!controller.isGrounded)
			return;
		if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
		{
			timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);
			PlayerCamera.transform.localPosition = new Vector3(
				PlayerCamera.transform.localPosition.x,
				defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
				PlayerCamera.transform.localPosition.z);
			//Hand.transform.localPosition = new Vector3(
			//	PlayerCamera.transform.localPosition.x,
			//	defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
			//	PlayerCamera.transform.localPosition.z);
			
            //walking
            if (Mathf.Abs(moveDirection.x) > 3f || Mathf.Abs(moveDirection.z) > 3f)
			{
                audioSource.pitch = 1.5f;
            }
			else
			{
                audioSource.pitch = 1f;
            }
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioClips[3];
                audioSource.pitch = audioSource.pitch + Random.Range(-0.5f,0.5f);
                audioSource.Play();
            }
        }
		else
		{
           /* if (audioSource.isPlaying)
            {
                audioSource.Stop();
				audioSource.pitch = 1;
            }*/
        }
	}
	private void Movements()
	{
		if (!controller.isGrounded)
		{
			moveDirection.y -= gravity * Time.deltaTime;
		}
		if (WillSlideOnSlopes && IsSlopeSliding)
		{
			moveDirection += new Vector3(hitPointNormal.x - hitPointNormal.y, hitPointNormal.z) * slopeSpeed;
		}
		if (!sliding)
		{
			controller.Move(moveDirection * Time.deltaTime);
		}
		else
		{
            if (!audioSource.isPlaying && sliding)
            {
                audioSource.clip = audioClips[2];
                audioSource.pitch = audioSource.pitch + Random.Range(-0.5f, 0.5f);
                audioSource.Play();
            }
            moveDirection = slideDir;
			slideTime -= Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
			if (slideTime <= 0)
			{
				sliding = false;
				DoFov(60f);
				controller.height = 2;
				controller.center = Vector3.zero;
				canUseHeadbob = true;
			}
            
        }
	}
	private IEnumerator CrouchStand()
	{
		if (isCrouching && Physics.Raycast(PlayerCamera.transform.position, Vector3.up, 1f))
		{
			yield break;
		}

		duringCrouchAnim = true;

		float timeElapsed = 0;
		float targetHeight = isCrouching ? standingHeight : crouchHeight;
		float currentHeight = controller.height;
		Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
		Vector3 currentCenter = controller.center;

		while (timeElapsed < timeToCrouch)
		{
			controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
			controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		controller.height = targetHeight;
		controller.center = targetCenter;

		isCrouching = !isCrouching;

		duringCrouchAnim = false;

	}
	private void DoFov(float endValue)
	{
		PlayerCamera.GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
	}
}
