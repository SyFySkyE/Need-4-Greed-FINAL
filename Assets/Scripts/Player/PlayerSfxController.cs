using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSfxController : MonoBehaviour
{
    [Header("SFX Audioclips")]
    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip landSfx;
    [SerializeField] private AudioClip landOnEnemySfx;
    [SerializeField] private AudioClip deathSfx;

    [Header("SFX Volume")]
    [SerializeField] private float jumpSfxVolume = 5f;
    [SerializeField] private float landSfxVolume = 1f;
    [SerializeField] private float landOnEnemySfxVolume = 1f;
    [SerializeField] private float deathSfxVolume = 5f;

    private AudioSource playerAudio;
    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleState();
    }

    private void HandleState()
    {
        switch (playerManager.CurrentState)
        {
            case PlayerState.Jumping:
                ToggleJumpSfx();
                break;
            case PlayerState.Dead:
            case PlayerState.Hurt:
                PlayHurtSfx();
                break;
            case PlayerState.Landing:
                PlayLandingSfx();
                break;
            case PlayerState.LandingOnEnemy:
                PlayLandingOnEnemySfx();
                break;
        }
    }
    private void ToggleJumpSfx()
    {
        // TODO This only gets called when currentState == Jumping and prev state is running, 
        // Or simply, not the same of currentState during the frame it transitions.
        // Debug.Log() registers the frame that they aren't sync but other scripts don't.
        // This gets triggered only sometimes, unreliable.
        if (playerManager.PreviousState == PlayerState.Running)
        {
            playerAudio.PlayOneShot(jumpSfx, jumpSfxVolume);
        }
    }

    private void PlayHurtSfx()
    {
        playerAudio.PlayOneShot(deathSfx, deathSfxVolume);
    }

    private void PlayLandingSfx()
    {
        // TODO This only gets called when currentState == Jumping and prev state is running, 
        // Or simply, not the same of currentState during the frame it transitions.
        // Debug.Log() registers the frame that they aren't sync but other scripts don't.
        // This gets triggered only sometimes, unreliable.
        if (playerManager.PreviousState != playerManager.CurrentState)
        {
            playerAudio.PlayOneShot(landSfx, landSfxVolume);
        }
    }

    private void PlayLandingOnEnemySfx()
    {
        playerAudio.PlayOneShot(landOnEnemySfx, landOnEnemySfxVolume);

    }
}
