using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Animation Parameters
    private const string animHorizontalSpeed = "horizontalSpeed";
    private const string animVerticalSpeed = "verticalSpeed";
    private const string animAirborne = "airborne";

    [Header("Movement")]
    [SerializeField]
    private float walkingSpeed = 2.5f;

    [Header("Jump")]
    [SerializeField]
    private float jumpForce = 4.75f;
    [SerializeField]
    private float jumpTimeCounter = 0.475f;
    [SerializeField]
    private float jumpTime = 0.475f;
    [SerializeField]
    private Transform[] feetTransform = new Transform[2];
    [SerializeField]
    private LayerMask groundLayerMask = 0;

    // Components
    private Rigidbody2D myRigidbody = null;
    private Animator myAnimator = null;

    // Inputs
    private InputActions inputActions = null;
    private float movementDirection = 0f;

    // States
    private bool onGround = false;
    private bool jump = false;
    private bool isJumping = false;
    private bool pause = false;

    //Aux
    private Collider2D[] groundCheckColliders = new Collider2D[1];

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        inputActions = new InputActions();
    }

    private void Start()
    {
        inputActions.Enable();
        pause = false;
        inputActions.player.Pause.Disable();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused)
        {
            return;
        }
        movementDirection = inputActions.player.Movement.ReadValue<float>();
        if (CheckDirectionChange())
        {
            FlipHorizontally();
        }

        myAnimator.SetFloat(animHorizontalSpeed, Mathf.Abs(myRigidbody.velocity.x));
        myAnimator.SetFloat(animVerticalSpeed, myRigidbody.velocity.y);

        if (jump && isJumping)
         {
             if (jumpTimeCounter > 0)
             {
                 myRigidbody.velocity = Vector2.up * jumpForce;
                 jumpTimeCounter -= Time.deltaTime;
             }
             else isJumping = false;
         }
    }
    private void FixedUpdate()
    {
        onGround = CheckForGround();
        myAnimator.SetBool(animAirborne, !onGround);
        myRigidbody.velocity = new Vector2(movementDirection * walkingSpeed, myRigidbody.velocity.y);
    }

    private bool CheckDirectionChange()
    {
        return transform.right.x * movementDirection < 0;
    }

    private void FlipHorizontally()
    {
        Vector3 targetRotation = transform.localEulerAngles;
        targetRotation.y += 180f;
        transform.localEulerAngles = targetRotation;
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private bool CheckForGround()
    {
        for (int i = 0; i < feetTransform.Length; i++)
        {
            if (Physics2D.OverlapPointNonAlloc(feetTransform[i].position, groundCheckColliders, groundLayerMask) > 0)
            {
                return true;
            }
        }
        return false;
    }
    public void OnJump()
    {
        this.jump = !this.jump;
        if (onGround && jump)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            myRigidbody.velocity = Vector2.up * jumpForce;
        }
        else
        {
            isJumping = false;
        }
    }

    public void OnPause()
    {
        if (Time.timeScale == 1)
        {
            this.pause = false;
        }
        this.pause = !this.pause;
        GameManager.Instance.PauseGame(pause);
    }

}