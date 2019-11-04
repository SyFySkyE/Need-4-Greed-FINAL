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

    [Header("Spawn Parameters")]
    [SerializeField] private float zPosToSpawn;

    [Header("Movement")]
    [SerializeField] private float xConstraint = 5f;
    [SerializeField] private float moveSpeed = 3f;

    [Header("Fireball Prefabs")]
    [SerializeField] private GameObject badFireBall;
    [SerializeField] private GameObject safeFireBall;

    [Header("Dragon Spawn Prefabs")]
    [SerializeField] private GameObject badDragon;
    [SerializeField] private GameObject safeDragon;

    [Header("Object Spawn Parameters")]
    [SerializeField] GameObject spawnLocation;
    [SerializeField] private float secondsBetweenSpawns = 1f;
    [SerializeField] private int chanceOfSpawningSafe = 7;

    [Header("Final Phase Particle System")]
    [SerializeField] private ParticleSystem eatGrass;
    [SerializeField] private ParticleSystem deathVfx;

    private Animator bossAnim;
    private bool vulnerable = true;
    private bool hasSpawned = false;

    private enum BossPhase { One = 1, Two, Three }
    private BossPhase currentPhase;

    // Start is called before the first frame update
    void Start()
    {
        bossAnim = GetComponent<Animator>();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(secondsBetweenSpawns);
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
        else if (currentPhase == BossPhase.Three)
        {
            bossAnim.SetTrigger("Charge");
        }
        else
        {
            StopCoroutine(Spawn());
        }
        
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned)
        {
            CheckForSpawn();
        }
        StayInFrontOfPlayer();
        Move();       
    }

    private void CheckForSpawn()
    {
        if (player.transform.position.z >= zPosToSpawn)
        {
            currentPhase = BossPhase.One;
            StartCoroutine(Spawn());            
            bossAnim.SetTrigger("Spawn");
            hasSpawned = true;
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        }
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
        if (other.gameObject.layer == 11 && !other.gameObject.CompareTag("Obstacle")) // Set by player once hit
        {
            Destroy(other.gameObject);
            NextState();            
        }
    }

    private void NextState()
    {        
        if (vulnerable)
        {
            bossAnim.SetTrigger("Hurt");
            currentPhase++;
            Debug.Log("State++");
            vulnerable = false;
            StartCoroutine(ToggleVulnerable());
        }
    }

    private int GetRandomNumber()
    {
        int r = Random.Range(0, chanceOfSpawningSafe);
        return r;
    }

    private IEnumerator ToggleVulnerable()
    {
        yield return new WaitForSeconds(1f);
        vulnerable = true;
    }
    private void ToggleParticleSystem()
    {
        if (!eatGrass.isPlaying)
        {
            eatGrass.Play();
        }
        else
        {
            eatGrass.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bossAnim.SetTrigger("Death");
            deathVfx.Play();
        }
    }

    private void OnDeath()
    {
        FindObjectOfType<GameSceneManager>().BroadcastMessage("Victory");
        Destroy(this.gameObject);        
    }
}
