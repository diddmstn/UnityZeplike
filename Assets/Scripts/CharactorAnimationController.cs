﻿using System;
using UnityEngine;
public class CharactorAnimationController: AnimationController
{
    private static readonly int isWalking = Animator.StringToHash("IsWalking");
    private static readonly int isRun = Animator.StringToHash("IsRun");

    private readonly float magnituteThreadshold = 0.5f; //문턱

    protected void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        controller.OnMoveEvent += Walk;
        controller.OnDashEvent += Run;
    }

    private void Run(bool obj)
    {
        animator.SetBool(isRun, obj);
    }

    private void Walk(Vector2 vector)
    {
        animator.SetBool(isWalking, vector.magnitude > magnituteThreadshold);
    }
}

