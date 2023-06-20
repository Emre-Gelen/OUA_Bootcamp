using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private Vector2 _move;
    private bool _jump;
    private bool _sprint;
    private bool _pickup;
    private bool _canPush;

    public bool analogMovement;

    public bool cursorLocked = true;

    public bool IsJumping() => _jump;
    public bool IsSprinting() => _sprint;
    public Vector2 GetMove() => _move;
    public bool IsPickup() => _pickup;
    public bool CanPush()=> _canPush;

#if ENABLE_INPUT_SYSTEM
    public void OnMove(InputAction.CallbackContext value)
    {
        MoveInput(value.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        JumpInput(value.action.triggered);
    }

    public void OnSprint(InputAction.CallbackContext value)
    {
        SprintInput(value.action.ReadValue<float>() == 1);
    }

    public void OnPickup(InputAction.CallbackContext value)
    {            
        if(value.performed)
            ChangePickupStatus();  
    }

    public void OnPush(InputAction.CallbackContext value)
    {
        ChangePushStatus();
    }

#endif


    public void MoveInput(Vector2 newMoveDirection)
    {
        _move = newMoveDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        _jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        _sprint = newSprintState;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
    
    public void ChangePickupStatus()
    {
        _pickup = !_pickup;
    }

    public void ChangePushStatus()
    {
        _canPush = !_canPush;
    }
}