using System;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    public static InputListener instance;

    public event Action JumpInputEvent;

    private GameControls controls;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        this.controls = new GameControls();
    }

    private void OnEnable()
    {
        this.controls.Enable();

        this.controls.Main.Jump.started += ctx => Jump();
    }

    private void OnDisable()
    {         
        this.controls.Main.Jump.started -= ctx => Jump();

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
        JumpInputEvent?.Invoke();
    }
}
