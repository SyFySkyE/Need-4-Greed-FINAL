using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float xConstraint = 4.5f;

    [Header("Player Dependencies")]
    [SerializeField] private GameObject player;
    [SerializeField] private float distanceBeforeStartMove = 25f;

    [Header("Despawn Variables")]
    [SerializeField] private float secondsBeforeDespawn = 1f;

    private Animator enemyAnim;
    private bool isCloseEnough = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isCloseEnough = IsPlayerCloseEnough();
        if (isCloseEnough) Move();
    }

    private bool IsPlayerCloseEnough()
    {
        return (player.transform.position.z >= transform.position.z - distanceBeforeStartMove);
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.x >= xConstraint || transform.position.x <= -xConstraint)
        {
            speed = -speed;
        }
    }

    public void EnemyDeath()
    {
        enemyAnim.SetTrigger("Stomped");
        Destroy(this.gameObject, secondsBeforeDespawn);
    }
}
