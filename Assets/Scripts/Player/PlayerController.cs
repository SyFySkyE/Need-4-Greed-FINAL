using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 15f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float xConstraint = 4.9f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float bonusJumpForce = 10f;

    [Header("Movement Increments")]
    [SerializeField] private float forwardSpeedIncrement = 10f;
    [SerializeField] private float horizontalSpeedIncrement = 2.5f;

    private PlayerManager playerManager;
    private Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.CurrentState != PlayerState.Dead)
        {
            MoveForward();
            MoveHorizontally();
            ConstrainHorizontalMovement();
            Jump();
        }
    }

    private void MoveForward()
    {
        playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, forwardSpeed);
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

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && playerManager.CurrentState == PlayerState.Running)
        {
            playerManager.CurrentState = PlayerState.Jumping;
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && playerManager.CurrentState == PlayerState.Jumping)
        {
            playerManager.CurrentState = PlayerState.Landing;
        }
        else if (collision.gameObject.CompareTag("Checkpoint"))
        {
            forwardSpeed += forwardSpeedIncrement;
            horizontalSpeed += horizontalSpeedIncrement;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().EnemyDeath();
            LandOnEnemy();
            collision.gameObject.layer = 11; // Do not collide anymore
        }
    }

    private void LandOnEnemy()
    {
        if (playerManager.CurrentState != PlayerState.Recovering)
        {
            playerManager.CurrentState = PlayerState.LandingOnEnemy;
            playerRB.AddForce(Vector3.up * bonusJumpForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Enemy"))
        {
            if (playerManager.CurrentState != PlayerState.Recovering)
            {
                playerManager.CurrentState = PlayerState.Hurt;
                other.gameObject.layer = 11; // Do not collide
            }
        }
    }

    private void PlayerHurt()
    {
        if (playerManager.CurrentState == PlayerState.Running)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }        
    }
}
