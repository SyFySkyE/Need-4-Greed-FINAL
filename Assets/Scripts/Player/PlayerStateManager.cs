using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Running, Jumping, Landing, LandingOnEnemy, Hurt, Recovering, Dead }

public class PlayerStateManager : MonoBehaviour
{
    [Header("Respawn Timer")]
    [SerializeField] private float secondsBeforeRespawn = 3f;

    private Vector3 respawnLoc = new Vector3(0f, 1f, 0f);

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

    private void Restart()
    {
        this.CurrentState = PlayerState.Dead;
        StartCoroutine(BroadcastRespawn());
    }

    private IEnumerator BroadcastRespawn()
    {
        yield return new WaitForSeconds(secondsBeforeRespawn);
        BroadcastMessage("Respawn");
    }

    private void Respawn()
    {
        this.CurrentState = PlayerState.Running;
        transform.position = respawnLoc;
        FindObjectOfType<GroundSpawner>().BroadcastMessage("Restart");
    }

    private void Update()
    {
        Debug.Log(this.CurrentState); // TODO take out
    }

    public void SetTransformLocation(Vector3 newLoc)
    {
        this.respawnLoc = newLoc;
    }
}
