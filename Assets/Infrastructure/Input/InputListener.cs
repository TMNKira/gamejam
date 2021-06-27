using System;
using UnityEngine;

public class InputListener : Singleton<InputListener>
{
    private GameControls controls;
    private EventHandler eventHandler;

    private void Awake()
    {
        this.eventHandler = EventHandler.Instance;
        this.controls = new GameControls();
    }

    private void OnEnable()
    {
        this.controls.Enable();

        this.controls.Main.Jump.started += ctx => Jump();
        this.controls.Main.SwitchSide.started += ctx => SwitchSide();
    }

    private void OnDisable()
    {         
        this.controls.Main.Jump.started -= ctx => Jump();
        this.controls.Main.SwitchSide.started -= ctx => SwitchSide();

        this.controls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return this.controls.Main.Movement.ReadValue<Vector2>();
    }

    public bool IsInterractionButtonPressed()
    {
        return this.controls.Main.Interact.triggered;
    }

    public bool IsThrowButtonPressed()
    {
        return this.controls.Main.Throw.triggered;
    }

    private void Jump()
    {
        this.eventHandler.InvokeJumpInput();
    }

    private void SwitchSide()
    {
        this.eventHandler.InvokeSwitchSideInput();
    }

    public bool IsSwitchSideButtonPressed()
    {
        return this.controls.Main.SwitchSide.triggered;
    }
}
