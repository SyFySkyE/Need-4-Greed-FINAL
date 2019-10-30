using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator playerAnim;
    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleStates();
    }

    private void HandleStates()
    {
        switch (playerManager.CurrentState)
        {
            case PlayerState.Running:
                SetMovingAnim();
                break;
            case PlayerState.Jumping:
                SetJumpingAnim();
                break;
            case PlayerState.Landing:
                SetLandingAnim();
                break;
            case PlayerState.LandingOnEnemy:
                SetLandingOnEnemyAnim();
                break;
            case PlayerState.Dead:
                SetDyingAnim();
                break;
        }
    }
    private void SetMovingAnim()
    {
        playerAnim.SetFloat("Speed_f", 0.7f);        
    }

    private void SetJumpingAnim()
    {
        if (playerManager.PreviousState != PlayerState.Jumping)
        {
            playerAnim.SetBool("Jump_b", true);
        }
    }

    private void SetLandingAnim()
    {
        if (playerManager.PreviousState != playerManager.CurrentState)
        {
            playerAnim.SetBool("Jump_b", false);
        }
    }

    private void SetLandingOnEnemyAnim()
    {
        if (playerManager.PreviousState != playerManager.CurrentState)
        {
            playerAnim.SetBool("JumpOn_b", true);
        }
    }

    private void SetDyingAnim()
    {
        if (playerManager.PreviousState != playerManager.CurrentState)
        {
            playerAnim.SetTrigger("Death_t");
        }
    }

    private void PlayerHurt()
    {
        if (playerManager.PreviousState != playerManager.CurrentState)
        {
            playerAnim.SetTrigger("Hurt");
        }
    }
}
