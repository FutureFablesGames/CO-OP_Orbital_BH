using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    // ================================================
    // COMPONENTS / COMPONENTS / COMPONENTS / COMPONEN
    // ================================================

    [Header("Components")]
    private Rigidbody rb;
    public AnimationHandler animationHandler;
    private GravityBody gb;
    public PlayerCharacter player;

    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    [Header("Motion")]
    public Vector2 v_MoveInput;
    public float f_MoveSpeed = 10f;
    [Range(0, 25)] public float f_MaxSpeed = 15.0f;
    public float f_JumpForce = 500.0f;
    private Vector3 v_MoveDir;
    private bool b_CanJump = true;
    public Vector3 v_NextPosition;

    [Header("Camera")]
    public GameObject FollowTarget;
    public Vector2 v_LookInput;
    public Vector3 v_LookDirection;
    public float f_CameraSensitivity = 0.5f;
    public float f_CameraLerpSpeed = 0.5f;    

    [Header("Aiming")]
    public GameObject mainCamera;
    public GameObject aimCamera;
    public GameObject aimReticle;
    public bool b_IsAiming;
    private Coroutine AimCoroutine;

    // ================================================
    // MONOBEHAVIOUR / MONOBEHAVIOUR/ MONOBEHAVIOUR /    
    // ================================================

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();  
        animationHandler = GetComponentInChildren<AnimationHandler>();
        gb = GetComponent<GravityBody>();
        player = GetComponent<PlayerCharacter>();
    }

    private void Start()
    {
        if (player != null)
        {
            player.Initialize();
        }

        animationHandler.SetOwnership(this);
    }

    private void Update()
    {
        // If there is no player controlling this, do nothing
        if (player == null) return;

        player.Tick();
        HandleCamera();
        HandleAiming();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    // ================================================
    // INPUT EVENTS / INPUT EVENTS / INPUT EVENTS / INP 
    // ================================================

    public void OnMove(InputAction.CallbackContext ctx)
    {
        v_MoveInput = ctx.ReadValue<Vector2>();

        if (ctx.canceled)
        {
            v_MoveInput = Vector2.zero;
        }               
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        v_LookInput = ctx.ReadValue<Vector2>();

        if (ctx.canceled)
        {
            v_LookInput = Vector2.zero;
        }         
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            player.m_Stats.Movement_Speed += 0.5f;
            animationHandler.animator.SetFloat("MoveSpeedMultiplier", player.m_Stats.Movement_Speed);
        }

        else if (ctx.canceled)
        {
            player.m_Stats.Movement_Speed -= 0.5f;
            animationHandler.animator.SetFloat("MoveSpeedMultiplier", player.m_Stats.Movement_Speed);
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {           

            Debug.Log("Jump Called");
            HandleJump();
        }              
    }

    public void OnSecondaryFire(InputAction.CallbackContext ctx)
    {        
        if (ctx.started)
        {            
            player.inventory.CurrentWeapon.SecondaryFire();
        }

        else if (ctx.canceled)
        {            
            player.inventory.CurrentWeapon.SecondaryCancel();
        }
    }

    public void OnPrimaryFire(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            animationHandler.animator.SetBool("Attack", true);
        }

        else if (ctx.canceled)
        {     
            player.inventory.CurrentWeapon.PrimaryCancel();
        }              
    }

    public void OnInteract(InputAction.CallbackContext ctx)        
    {
        Debug.Log("Interaction Called");
    }

    public void OnSwap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            player.inventory.SwapWeapon();
        }
    }

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================
    
    private void CalculateMovement()
    {
        // Set the movement Variable
        Vector3 forward = transform.forward * v_MoveInput.y;
        Vector3 right = transform.right * v_MoveInput.x;
        v_MoveDir = forward + right;
        v_NextPosition = transform.position + v_MoveDir * (f_MoveSpeed * player.m_Stats.Movement_Speed) * Time.fixedDeltaTime;        
    }

    private void HandleMovement()
    {
        // Update AnimationHandler
        animationHandler.currentState.Horizontal = v_MoveInput.x;
        animationHandler.currentState.Vertical = v_MoveInput.y;
        animationHandler.currentState.Grounded = gb.IsGrounded;

        // If we're not moving
        if (v_MoveInput == Vector2.zero)
        {
            // But we're aiming
            if (b_IsAiming)
            {
                // Set the player rotation based on the look transform
                transform.RotateAround(transform.position, FollowTarget.transform.rotation * Vector3.up, v_LookInput.x * f_CameraSensitivity);
                //transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);

                // Reset the y rotation of the look transform
                FollowTarget.transform.localEulerAngles = new Vector3(v_LookDirection.x, 0, 0);
            }

            return;
        }

        // Calculate the next position
        CalculateMovement();        

        // Move the Player
        float smooth = Mathf.Min(1.0f, Time.fixedDeltaTime / 0.15f);
        rb.MovePosition(Vector3.Lerp(transform.position, v_NextPosition, smooth));        
                
        // Set the player rotation based on the look transform
        transform.RotateAround(transform.position, FollowTarget.transform.rotation * Vector3.up, v_LookInput.x * f_CameraSensitivity);
        //transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);
        
        // Reset the y rotation of the look transform
        FollowTarget.transform.localEulerAngles = new Vector3(v_LookDirection.x, 0, 0);
    }

    private void HandleCamera()
    {
        // Horizontal Rotation
        FollowTarget.transform.rotation *= Quaternion.AngleAxis(v_LookInput.x * f_CameraSensitivity, Vector3.up);


        // Vertical Rotation
        FollowTarget.transform.rotation *= Quaternion.AngleAxis(-v_LookInput.y * f_CameraSensitivity, Vector3.right);        
        v_LookDirection = FollowTarget.transform.localEulerAngles;
        v_LookDirection.z = 0;

        // Clamp the Up/Down Rotation
        float vertical = FollowTarget.transform.localEulerAngles.x;        
        if (vertical > 180 && vertical < 340) {
            v_LookDirection.x = 340;
        } else if (vertical < 180 && vertical > 40) {
            v_LookDirection.x = 40;
        }

        // Set the rotation of the camera pivot
        FollowTarget.transform.localEulerAngles = v_LookDirection;
    }

    private void HandleAiming()
    {
        // If we're trying to aim and we're not already aiming
        if (b_IsAiming && !aimCamera.activeInHierarchy)
        {
            // Set the main camera inactive and the aim camera active
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);

            // Allow time for the camera to blend before enabling the UI
            AimCoroutine = StartCoroutine(ShowReticule());
        }

        // If we're already aiming and we're trying to stop
        else if (!b_IsAiming && !mainCamera.activeInHierarchy)
        {
            // Stop the Aim Coroutine if it's in the middle of running
            StopCoroutine(AimCoroutine);

            // Set the main camera active and the reticule and aim camera inactive.
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            aimReticle.SetActive(false);
        }
    }

    private void HandleJump()
    {
        if (gb.IsGrounded && b_CanJump)
        {
            rb.AddForce(gb.Up * (f_JumpForce * player.m_Stats.Jump_Power));
            animationHandler.animator.SetTrigger("Jump");
            StartCoroutine(DelayJumpAllowance());
        }
    }

    private IEnumerator ShowReticule()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(true);
    }

    private IEnumerator DelayJumpAllowance()
    {
        b_CanJump = false;
        yield return new WaitForSeconds(0.5f);
        b_CanJump = true;
    }
}
