using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AnimationState
{
    public float Horizontal = 0.0f;
    public float Vertical = 0.0f;
    public bool Grounded = false;

    public void Set(AnimationState state)
    {
        Horizontal = state.Horizontal;
        Vertical = state.Vertical;
        Grounded = state.Grounded;
    }
}

public class AnimationHandler : MonoBehaviour
{
    
    public Animator animator;
    public AnimationState currentState;
    public PlayerController pc;

    public void Initialize(PlayerController pc, Animator anim)
    {
        this.pc = pc;
        animator = anim;
        currentState = new AnimationState();
    }

    private void Update()
    {
        animator.SetFloat("Horizontal", currentState.Horizontal);
        animator.SetFloat("Vertical", currentState.Vertical);
        animator.SetBool("Grounded", currentState.Grounded);
    }

    public void SetAnimationState(AnimationState newState)
    {
        currentState.Set(newState);
    }
}
