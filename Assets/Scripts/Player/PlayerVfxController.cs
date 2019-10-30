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
            case PlayerState.Landing:
                landVfx.Play();
                break;
            case PlayerState.LandingOnEnemy:
                PlayLandingOnEnemyVfx();
                break;
            case PlayerState.Dead:
                PlayDeathVfx();
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
        if (!jumpingVfx.isPlaying)
        {
            dustKickVfx.Stop();
            jumpingVfx.Play();
        }
    }

    private void PlayLandingOnEnemyVfx()
    {
        landOnEnemyVfx.Play();
    }

    private void PlayDeathVfx()
    {
        dustKickVfx.Stop();
        failVfx.Play();
    }
    private void PlayerHurt()
    {
        StartCoroutine(FlashPlayer());
        hurtVfx.Play();
    }
    private IEnumerator FlashPlayer()
    {
        SkinnedMeshRenderer render = GetComponentInChildren<SkinnedMeshRenderer>();
        for (int i = 1; i < timeWhileFlashing; i++)
        {
            render.enabled = false;
            yield return new WaitForSeconds(playerFlashInterval);
            render.enabled = true;
            yield return new WaitForSeconds(playerFlashInterval);
        }
    }
}
