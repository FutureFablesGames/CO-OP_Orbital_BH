using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public GameObject mesh;    
    private GravityBody gravity;
    private Rigidbody rb;
    public CameraController CameraController;

    [Header("Motion")]
    public float f_MoveSpeed = 10f;
    [Range(0, 25)] public float f_MaxSpeed = 15.0f;
    public float f_JumpForce = 500.0f;
    private Vector3 v_MoveDir;
    private bool CanJump = true;

    [Header("Interactions")]
    public Interactable targetInteractable = null;

    [Header("Handler")]    
    AnimationHandler animationHandler;    

    [Header("Resources")]
    public float CurrentResources;
    //public float MaxResources = 500.0f;

    [Header("UI")]
    public TMP_Text PromptDisplay = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gravity = GetComponent<GravityBody>();

        animationHandler = GetComponent<AnimationHandler>();
        animationHandler.Initialize(this, animationHandler.animator);             
    }

    private void OnEnable()
    {
        if (Manager.Input != null)
        {
            Manager.Input.JumpCallback += Jump;
            Manager.Input.AttackCallback += Attack;
            Manager.Input.InteractCallback += Interact;
            Manager.Input.SetEnable(InputMap.Game, true);
        }
        
    }

    private void OnDisable()
    {
        if (Manager.Input != null)
        {
            Manager.Input.JumpCallback -= Jump;
            Manager.Input.AttackCallback -= Attack;
            Manager.Input.InteractCallback -= Interact;
            Manager.Input.SetEnable(InputMap.Game, false);
        }        
    }

    private void Update()
    {        
        //Debug.Log(Manager.Input.MotionInput);

        if (Manager.Game.State != GameState.Playing) return;

        HandleMotion();      
    }

    private void FixedUpdate()
    {
        animationHandler.currentState.Horizontal = Manager.Input.MotionInput.x;
        animationHandler.currentState.Vertical = Manager.Input.MotionInput.y;
        animationHandler.currentState.Grounded = gravity.Grounded;

        rb.MovePosition(rb.position + v_MoveDir * f_MoveSpeed * Time.fixedDeltaTime);
    }

    private void HandleMotion()
    {
        Vector3 forward = mesh.transform.forward * Manager.Input.MotionInput.y;
        Vector3 right = mesh.transform.right * Manager.Input.MotionInput.x;

        v_MoveDir = forward + right;
        
        LimitSpeed();       
    }

    private void LimitSpeed()
    {
        if (rb.velocity.magnitude > f_MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * f_MaxSpeed;
        }
    }

    public void Jump()
    {
        if (gravity.Grounded && CanJump)
        {
            Vector3 gravityUp = (transform.position - gravity.attractor.transform.position).normalized;
            rb.AddForce(gravityUp * f_JumpForce);
            animationHandler.animator.SetTrigger("Jump");
            StartCoroutine(DelayJumpAllowance());
        }        
    }

    private IEnumerator DelayJumpAllowance()
    {
        CanJump = false;
        yield return new WaitForSeconds(0.5f);
        CanJump = true;
    }


    public void Attack()
    {
        // Trigger Animation -- Currently only works with pickaxe.
        animationHandler.animator.SetTrigger("Swing");
    }

    public void Interact()
    {
        if (targetInteractable != null)
        {
            targetInteractable.Interact(this);
        }
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactable")
        {
            if (targetInteractable == null)
            {
                targetInteractable = other.GetComponent<Interactable>();
                PromptDisplay.text = "'E' to Interact";
                PromptDisplay.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            if (targetInteractable != null)
            {
                targetInteractable = null;
                PromptDisplay.text = "";
                PromptDisplay.enabled = false;
            }
        }
    }
}
