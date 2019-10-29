using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject player;

    [Header("Spawn Logic")]
    [SerializeField] GameObject groundToSpawn;
    [SerializeField] private int zPosToSpawn = 175;
    [SerializeField] private int zPosIncrement = 50;
    [SerializeField] private int zPosSpawnTrigger = 0;
    [SerializeField] private int zPosDespawnTrigger = 250;

    private Queue<GameObject> groundObjectsInScene = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        QueueFirstGround();
    }

    private void QueueFirstGround()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, zPosToSpawn);
        GameObject startGround = Instantiate(groundToSpawn, spawnPos, Quaternion.identity);
        groundObjectsInScene.Enqueue(startGround);
        zPosToSpawn += zPosIncrement;
        zPosSpawnTrigger = zPosIncrement; // TODO is this needed?
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, zPosToSpawn);
        if (player.transform.position.z > zPosSpawnTrigger)
        {
            GameObject newGround = Instantiate(groundToSpawn, spawnPos, Quaternion.identity);
            groundObjectsInScene.Enqueue(newGround);

            zPosToSpawn += zPosIncrement;
            zPosSpawnTrigger += zPosIncrement; // TODO see above
        }

        if (player.transform.position.z > zPosDespawnTrigger)
        {
            zPosDespawnTrigger += zPosIncrement;
            Destroy(groundObjectsInScene.Dequeue());
        }
    }
}
