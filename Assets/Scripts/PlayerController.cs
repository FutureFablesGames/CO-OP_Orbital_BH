using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public GameObject mesh;
    public CameraController camController;
    private GravityBody gravity;
    private Rigidbody rb;

    [Header("Motion")]
    public float f_MoveSpeed = 10f;
    [Range(0, 25)] public float f_MaxSpeed = 15.0f;
    public float f_JumpForce = 500.0f;
    private Vector3 v_MoveDir;

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

    private void Update()
    {
        if (Manager.Game.State != GameState.Playing) return;

        Motion();
        SpeedLimiter();
        Jump();
        Attack();
    }

    private void FixedUpdate()
    {
        animationHandler.currentState.Horizontal = Input.GetAxisRaw("Horizontal");
        animationHandler.currentState.Vertical = Input.GetAxisRaw("Vertical");
        animationHandler.currentState.Grounded = gravity.Grounded;

        rb.MovePosition(rb.position + v_MoveDir * f_MoveSpeed * Time.fixedDeltaTime);
    }

    private void Motion()
    {
        Vector3 forward = mesh.transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 right = mesh.transform.right * Input.GetAxisRaw("Horizontal");

        v_MoveDir = forward + right;
        
       
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 gravityUp = (transform.position - gravity.attractor.transform.position).normalized;            
            rb.AddForce(gravityUp * f_JumpForce);
            animationHandler.animator.SetTrigger("Jump");
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animationHandler.animator.SetTrigger("Swing");
        }
    }

    private void SpeedLimiter()
    {
        if (rb.velocity.magnitude > f_MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * f_MaxSpeed;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactable")
        {
            if (!PromptDisplay.enabled)
            {
                PromptDisplay.text = "'E' to Interact";
                PromptDisplay.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                other.GetComponent<Interactable>().Interact(this);
                PromptDisplay.text = "";
                PromptDisplay.enabled = false;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            if (PromptDisplay.enabled)
            {
                PromptDisplay.text = "";
                PromptDisplay.enabled = false;
            }
        }
    }
}
