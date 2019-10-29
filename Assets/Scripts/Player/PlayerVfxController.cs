using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVfxController : MonoBehaviour
{
    [Header("Particle System Prefabs")]
    [SerializeField] ParticleSystem dustKickVfx;    
    [SerializeField] ParticleSystem jumpingVfx;
    [SerializeField] ParticleSystem landVfx;
    [SerializeField] ParticleSystem landOnEnemyVfx;
    [SerializeField] ParticleSystem hurtVfx;
    [SerializeField] ParticleSystem failVfx;

    [Header("Invincibility After Hurt")]
    [SerializeField] private float playerFlashInterval = 0.01f;
    [SerializeField] private int timeWhileFlashing = 15;

    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
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
                StartMoveVfx();
                break;
            case PlayerState.Jumping:
                StartJumpVfx();
                break;
        }
    }

    private void StartMoveVfx()
    {
        if (!dustKickVfx.isPlaying)
        {
            jumpingVfx.Stop();
            dustKickVfx.Play();
        }
    }

    private void StartJumpVfx()
    {

    }
}
