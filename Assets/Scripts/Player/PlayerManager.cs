using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Running, Jumping, Landing, LandingOnEnemy, Hurt, Recovering, Dead }

public class PlayerManager : MonoBehaviour
{
    [Header("Recovery Time")]
    [SerializeField] public float RecoveryTime = 1.5f;

    private PlayerState currentState;
    public PlayerState PreviousState;
    public PlayerState CurrentState
    {
        get { return this.currentState; }
        set
        {
            if (this.currentState != value)
            {
                OnStateEnter();
                this.currentState = value;
            }
        }
    }
    
    private void OnStateEnter()
    {
        Debug.Log("State is transitioning.");
    }

    // Start is called before the first frame update
    void Start()
    {
        this.CurrentState = PlayerState.Running;
    }

    private void Update()
    {
        Debug.Log("Player Current State: " + this.CurrentState);
    }
}
