using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    // types that are allowed to spawn at this point
    // setup manually in editor
    public List<EnemyType> spawnTypes;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // do nothing
    }


    // Update is called once per frame 
    void Update()
    {
        // do nothing
    }


    // spawn an enemy at this spawn point
    public void spawn()
    {
        // choose enemy to spawn
        EnemyType type = chooseEnemy();
        EnemyType runtimeType = getRuntimeType(type);

        // spawn enemy and give it a type
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        GameObject enemyObj = Instantiate(enemyPrefab, position, rotation);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.Initialize(runtimeType);
        
        enemy.printStat();
    }


    // choose an enemy to spawn based on spawn rate
    EnemyType chooseEnemy()
    {
        if (spawnTypes == null || spawnTypes.Count == 0)
        {
            Debug.LogError("spawnTypes empty/not initialized");
            return null;
        }

        // use spawn rate
        float totalWeight = spawnTypes.Sum(e => getRuntimeType(e).spawnRate); // get runtime spawn rate
        float roll = Random.Range(0f, totalWeight);

        float cumulative = 0f;
        foreach (EnemyType type in spawnTypes)
        {
            float weight = getRuntimeType(type).spawnRate;
            if (weight <= 0f)
            {
                continue;
            }
            cumulative += weight;
            if (roll <= cumulative)
            {
                return type; // the returned EnemyType is NOT the runtime EnemyType
            }
        }

        return spawnTypes.Last();
    }


    // make this a function for better code readability
    EnemyType getRuntimeType(EnemyType type)
    {
        return Logic.instance.getRuntimeEnemyType(type);
    }
}
