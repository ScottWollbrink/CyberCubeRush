using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] int numToSpawn;
    [SerializeField] int spawnTimer;
    [SerializeField] Transform[] spawnPos;

    int spawnCount;
    int arrayPos;
    bool isSpawning;
    bool startSpawning;

    private void Start()
    {
        GameManager.Instance.UpdateEnemyCounter(numToSpawn);
    }
    // Update is called once per frame
    void Update()
    {
        if (startSpawning && !isSpawning && spawnCount < numToSpawn)
        {
            StartCoroutine(spwan());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startSpawning = true;
        }
    }

    IEnumerator spwan()
    {
        isSpawning = true;
        for (int i = 0; i < spawnPos.Length; i++)
        {
            arrayPos = i;
            Instantiate(objectToSpawn, spawnPos[arrayPos].position, spawnPos[arrayPos].rotation);
            spawnCount++;
            yield return new WaitForSeconds(spawnTimer);
            isSpawning = false;
        }
        
    }
}
