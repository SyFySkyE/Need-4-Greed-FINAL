using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Running, Jumping, Landing, LandingOnEnemy, Hurt, Recovering, Dead }

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public float RecoveryTime { get; private set; } = 1.5f;

    private PlayerState currentState;
    public PlayerState CurrentState
    {
        get { return this.currentState; }
        set
        {
            if (this.currentState != value)
            {
                this.PreviousState = this.currentState;
                Debug.Log($"{this.name}'s state was {this.currentState} and is now {value}");
                this.currentState = value;
                this.PreviousState = this.currentState;
            }
        }
    }

    private PlayerState previousState;
    public PlayerState PreviousState
    {
        get { return this.previousState; }
        set
        {
            if (this.previousState != value)
            {
                Debug.Log($"{this.name}'s previous state was {this.previousState} and is now {value}");
                this.previousState = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.CurrentState = PlayerState.Running;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player state: " + this.CurrentState);
    }
}
