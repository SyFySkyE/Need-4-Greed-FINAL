using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Player Dependency")]
    [SerializeField] private GameObject player;

    [Header("Position")]
    [SerializeField] private float yPos = 10f;
    [SerializeField] private float zPos = 25f;
    [SerializeField] private float yPosWhenCharging = -1f;

    [Header("Movement")]
    [SerializeField] private float xConstraint = 5f;
    [SerializeField] private float moveSpeed = 3f;

    [Header("Fireball Prefabs")]
    [SerializeField] private GameObject badFireBall;
    [SerializeField] private GameObject safeFireBall;

    [Header("Dragon Spawn Prefabs")]
    [SerializeField] private GameObject badDragon;
    [SerializeField] private GameObject safeDragon;

    [Header("Spawn Parameters")]
    [SerializeField] GameObject spawnLocation;
    [SerializeField] private float secondsBetweenSpawns = 1f;
    [SerializeField] private int chanceOfSpawningSafe = 7;

    private Animator bossAnim;
    private bool vulnerable = true;

    private enum BossPhase { One = 1, Two, Three }
    private BossPhase currentPhase;

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = BossPhase.One;
        StartCoroutine(Spawn());
        bossAnim = GetComponent<Animator>();
    }

    private IEnumerator Spawn()
    {
        if (currentPhase == BossPhase.One)
        {
            if (GetRandomNumber() == 0)
            {
                Instantiate(safeFireBall, spawnLocation.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(badFireBall, spawnLocation.transform.position, Quaternion.identity);
            }
        }
        else if (currentPhase == BossPhase.Two)
        {
            if (GetRandomNumber() == 0)
            {
                Instantiate(safeDragon, spawnLocation.transform.position, transform.rotation);
            }
            else
            {
                Instantiate(badDragon, spawnLocation.transform.position, transform.rotation);
            }            
        }
        else
        {
            StopCoroutine(Spawn());
        }
        yield return new WaitForSeconds(secondsBetweenSpawns);
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        StayInFrontOfPlayer();
        Move();       
    }

    private void StayInFrontOfPlayer()
    {
        transform.position = new Vector3(transform.position.x, yPos, zPos + player.transform.position.z);
    }

    private void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        if (transform.position.x >= xConstraint || transform.position.x <= -xConstraint)
        {
            moveSpeed = -moveSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11) // Set by player once hit
        {
            Destroy(other.gameObject);
            NextState();            
        }
    }

    private void NextState()
    {        
        if (vulnerable)
        {
            currentPhase++;
            Debug.Log("State++");
            vulnerable = false;            
        }
    }

    private int GetRandomNumber()
    {
        int r = Random.Range(0, chanceOfSpawningSafe);
        return r;
    }
}
