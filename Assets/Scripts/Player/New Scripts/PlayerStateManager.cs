﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Running, Jumping, Landing, LandingOnEnemy, Hurt, Recovering, Dead }

public class PlayerStateManager : MonoBehaviour
{
    private PlayerState currentState;
    public PlayerState CurrentState
    {
        get { return this.currentState; }
        set
        {
            if (this.currentState != value)
            {
                this.previousState = this.currentState;
                this.currentState = value;                
            }
        }
    }
    private PlayerState previousState;
    public PlayerState PreviousState
    {
        get { return this.previousState; }
    }

    private void Start()
    {
        this.CurrentState = PlayerState.Running;
    }
    private void Update()
    {
        Debug.Log("Current State: " + this.CurrentState);
    }
}