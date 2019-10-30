using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Running, Jumping, Landing, LandingOnEnemy, Hurt, Recovering, Dead }

public class PlayerManager : MonoBehaviour
{
    [Header("Recovery Time")]
    [SerializeField] public float RecoveryTime = 1.5f;

    private PlayerState currentState;
    public PlayerState CurrentState
    {
        get { return this.currentState; }
        set
        {
            if (this.currentState != value)
            {
                this.PreviousState = this.CurrentState;
                this.currentState = value;
                this.PreviousState = this.CurrentState;
            }
        }
    }

    private PlayerState previousState;
    public PlayerState PreviousState
    {
        get { return this.previousState; }
        private set 
        {           
            if (this.previousState != value)
            {
                this.previousState = value;                
            }            
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        this.CurrentState = PlayerState.Running;
    }

    private void Update()
    {
        Debug.Log("Player Current State: " + this.CurrentState);
        Debug.Log("Player Previous State: " + this.PreviousState);

        // TODO This used to log the single frame where CurrentState and PRevious state don't line up,
        // But other scripts didn't trigger (like PlayerSfx controller) the transition even when it did
    }
}
