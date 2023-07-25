using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public enum InputMap
{ 
    Game,
    Menu
}


public class MGR_Input
{
    public ActionMap Controls;
    private bool Initialized = false;

    // Axis Values
    public Vector2 MotionInput;
    public Vector2 CameraInput;

    // Delegate Callback Functions for Input Actions
    public delegate void InputDelegate();
    public InputDelegate MoveCallback;
    public InputDelegate JumpCallback;
    public InputDelegate LookCallback;
    public InputDelegate PrimaryFireCallback;
    public InputDelegate SecondaryFireCallback;
    public InputDelegate InteractCallback;
    public InputDelegate Item1Callback;

    // ----------------------------------------------------------------------------
    // Initialization
    // ----------------------------------------------------------------------------

    public static MGR_Input Initialize()
    {
        if (Manager.Input != null)
        {
            Debug.LogWarning("InputManager already exists.  Use \"Manager.Input\" instead.");
            return null;
        }

        // Initialize the ActionMap
        MGR_Input result = new MGR_Input
        {
            Controls = new ActionMap(),            

            // Validate the InputManager
            Initialized = true
        };

        result.Controls.FindAction("Move").performed += result.MoveCtx;
        result.Controls.FindAction("Move").canceled += result.MoveCancelCtx;
        result.Controls.FindAction("Look").performed += result.LookCtx;
        result.Controls.FindAction("Look").canceled += result.LookCancelCtx;
        result.Controls.FindAction("Jump").performed += result.JumpCtx;
        result.Controls.FindAction("PrimaryFire").performed += result.PrimaryFireCtx;
        result.Controls.FindAction("SecondaryFire").performed += result.SecondaryFireCtx;
        result.Controls.FindAction("Interact").performed += result.InteractCtx;
        result.Controls.FindAction("Item1").performed += result.Item1Ctx;
      
        return result;
    }

    public void SetEnable(InputMap map, bool state)
    {
        switch (map)
        {
            case InputMap.Game:
                if (state) Controls.Game.Enable();
                else Controls.Game.Disable();
                break;
            case InputMap.Menu:
                if (state) Controls.Menu.Enable();
                else Controls.Menu.Disable();
                break;
        }
    }

    // ----------------------------------------------------------------------------
    // CONTEXT FUNCTIONS
    // ----------------------------------------------------------------------------

    public void MoveCtx(InputAction.CallbackContext ctx)
    {
        var inputValue = ctx.ReadValue<Vector2>();
        MotionInput = inputValue;
    }

    public void MoveCancelCtx(InputAction.CallbackContext ctx)
    {
        MotionInput = Vector2.zero;
    }

    public void LookCtx(InputAction.CallbackContext ctx)
    {
        var inputValue = ctx.ReadValue<Vector2>();
        CameraInput = inputValue;
    }

    public void LookCancelCtx(InputAction.CallbackContext ctx)
    {
        CameraInput = Vector2.zero;
    }

    public void JumpCtx(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        
        JumpCallback?.Invoke();
    }

    public void PrimaryFireCtx(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        
        PrimaryFireCallback?.Invoke();
    }

    public void SecondaryFireCtx(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        SecondaryFireCallback?.Invoke();
    }

    public void InteractCtx(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        
        InteractCallback?.Invoke();
    }

    public void Item1Ctx(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        Item1Callback?.Invoke();
    }

}
