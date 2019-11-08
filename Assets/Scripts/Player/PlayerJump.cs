using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Attributes")]
    [SerializeField] private float jumpForce = 10f;

    [Header("Particle System Prefabs")]
    [SerializeField] private ParticleSystem jumpVfx;
    [SerializeField] private ParticleSystem landVfx;
    [SerializeField] private ParticleSystem landOnEnemyVfx;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip landSfx;
    [SerializeField] private AudioClip landOnEnemySfx;

    [Header("Sfx Volume")]
    [SerializeField] private float jumpSfxVolume = 1f;
    [SerializeField] private float landSfxVolume = 1f;
    [SerializeField] private float landOnEnemySfxVolume = 3f;

    private Rigidbody playerRB;
    private AudioSource playerAudio;
    private Animator playerAnim;
    private PlayerStateManager playerManager;
    
    private bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerManager = GetComponent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            playerManager.CurrentState = PlayerState.Jumping;
            playerAnim.SetBool("Jump_b", true);
            jumpVfx.Play();
            playerAudio.PlayOneShot(jumpSfx, jumpSfxVolume);
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !canJump)
        {
            playerAnim.SetBool("Jump_b", false);
            playerManager.CurrentState = PlayerState.Running;
            jumpVfx.Stop();
            canJump = true;
            playerAudio.PlayOneShot(landSfx, landSfxVolume);
            landVfx.Play();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAnim.SetBool("JumpOn_b", true);
            collision.gameObject.layer = 11; // Stop colliding
            collision.gameObject.GetComponent<Enemy>().EnemyDeath();
            playerAudio.PlayOneShot(landOnEnemySfx, landOnEnemySfxVolume);
            landOnEnemyVfx.Play();
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("BossSpawn"))
        {
            playerAnim.SetBool("JumpOn_b", true);
            collision.gameObject.layer = 11; // Stop colliding
            playerAudio.PlayOneShot(landOnEnemySfx, landOnEnemySfxVolume);
            landOnEnemyVfx.Play();
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void PlayerHurt()
    {
        if (playerManager.CurrentState != PlayerState.Jumping)
        {
            if (GetComponent<PlayerHealth>().GetHealth() > 1)
            {
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }        
    }
}
