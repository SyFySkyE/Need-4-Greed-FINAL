using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun :  MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 15f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float xConstraint = 4.9f;

    [Header("Movement Increments")]
    [SerializeField] private float forwardSpeedIncrement = 10f;
    [SerializeField] private float horizontalSpeedIncrement = 2.5f;

    [Header("Particle System Prefabs")]
    [SerializeField] ParticleSystem dustKickVfx;

    [Header("Sound Effects")]
    [SerializeField] AudioClip runSfx;

    [Header("Sfx Volume")]
    [SerializeField] private float runSfxVolume = 1f;

    private Rigidbody playerRB;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private PlayerStateManager playerManager;

    private bool canRun = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerManager = GetComponent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canRun)
        {
            PlayRunVfx();
            MoveForward();
            MoveHorizontally();
            ConstrainHorizontalMovement();
        }
    }

    private void PlayRunVfx()
    {
        if (!dustKickVfx.isPlaying)
        {
            dustKickVfx.Play();
        }
    }

    private void MoveForward()
    {
        if (playerManager.CurrentState == PlayerState.Running)
        {
            playerAnim.SetFloat("Speed_f", 0.7f);            
        }

        if (playerManager.CurrentState != PlayerState.Dead)
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, forwardSpeed);
        }
    }

    private void MoveHorizontally()
    {
        float xAxisRaw = Input.GetAxis("Horizontal");
        playerRB.velocity = new Vector3(xAxisRaw * horizontalSpeed, playerRB.velocity.y, playerRB.velocity.z);
    }

    private void ConstrainHorizontalMovement()
    {
        if (transform.position.x >= xConstraint)
        {
            transform.position = new Vector3(xConstraint, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -xConstraint)
        {
            transform.position = new Vector3(-xConstraint, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            forwardSpeed += forwardSpeedIncrement;
            horizontalSpeed += horizontalSpeedIncrement;
        }
    }

    public void GameOver()
    {
        canRun = false;
    }
}
