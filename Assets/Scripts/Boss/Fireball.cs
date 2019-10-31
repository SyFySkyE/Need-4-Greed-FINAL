using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float backSpeed = -2f;

    [Header("FireBack Parameters")]    
    [SerializeField] private float moveTowardMotherSpeed = 10f;

    [Header("Despawn Parameters")]
    [SerializeField] private float secondsBeforeDestroy = 5f;

    private Boss mother;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, secondsBeforeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        if (mother == null)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * backSpeed);
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
