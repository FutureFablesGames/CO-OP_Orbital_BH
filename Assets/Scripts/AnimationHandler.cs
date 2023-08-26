using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations.Rigging;

[System.Serializable]
public class AnimationState
{    
    public float Horizontal = 0.0f;
    public float Vertical = 0.0f;
    public bool Grounded = false;
    public bool Aiming = false;

    public void Set(AnimationState state)
    {
        Horizontal = state.Horizontal;
        Vertical = state.Vertical;
        Grounded = state.Grounded;
    }
}

public class AnimationHandler : MonoBehaviour
{
    // ================================================
    // COMPONENTS / COMPONENTS / COMPONENTS / COMPONEN
    // ================================================

    public PlayerController owner;
    public Animator animator;
    public Rig rig;

    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    [Header("Animation State")]
    public AnimationState currentState;

    // ================================================
    // MONOBEHAVIOUR / MONOBEHAVIOUR/ MONOBEHAVIOUR /    
    // ================================================

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentState = new AnimationState();
        rig = GetComponentInChildren<Rig>();
    }

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================

    public void SetOwnership(PlayerController pc)
    {
        this.owner = pc;
    }

    private void Update()
    {
        animator.SetFloat("Horizontal", currentState.Horizontal);
        animator.SetFloat("Vertical", currentState.Vertical);
        animator.SetBool("Grounded", currentState.Grounded);
        animator.SetBool("Aiming", currentState.Aiming);

        rig.weight = (currentState.Aiming) ? 1 : 0;
    }
    
    public void SetAnimationState(AnimationState newState)
    {
        currentState.Set(newState);
    }
   
    public void AttackAnimation()
    {
        Debug.Log("Attack Animation Called");
        owner.player.inventory.CurrentWeapon.PrimaryFire();        
    }
}
