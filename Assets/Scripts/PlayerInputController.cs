﻿using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController: TopDownController
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized; 
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);

        newAim = (worldPos - (Vector2)transform.position).normalized;
        CallLookEvent(newAim);
    }
    public void OnDash(InputValue value)
    {
        CallDashEvent(value.isPressed);
    }
    public void OnEmotion(InputValue value)
    {
        CallEmoteEvent(value.isPressed);
    }
}


