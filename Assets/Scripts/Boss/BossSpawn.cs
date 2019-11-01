using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float backSpeed = -2f;

    [Header("FireBack Parameters")]    
    [SerializeField] private float moveTowardMotherSpeed = 10f;

    [Header("Despawn Parameters")]
    [SerializeField] private float secondsBeforeDestroy = 5f;

    [Header("Follow Player")]
    [SerializeField] private bool followPlayer = false;
    
    private PlayerStateManager player;
    private Boss mother;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, secondsBeforeDestroy);
        if (followPlayer)
        {
            player = FindObjectOfType<PlayerStateManager>();
            transform.LookAt(player.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mother == null)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * backSpeed, Space.World);
        }
        else
        {
            MoveTowardMother();
        }
    }

    private void MoveTowardMother()
    {
        float moveStep = moveTowardMotherSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, mother.transform.position, moveStep);
        GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.layer = 11; // Don't collide
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        mother = FindObjectOfType<Boss>();
        this.gameObject.layer = 11; // Don't Collide        
    }
}
