using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public enum TimeOfDay
{
    Morning,
    Afternoon,
    Night
}

public class Logic : MonoBehaviour
{

    public static Logic instance { get; private set; }

    TimeOfDay currTime;

    [SerializeField] List<SpawnPoint> spawnPoints;

    public EnemyType RedEnemy;
    public EnemyType BrownEnemy;
    public EnemyType BlueEnemy;
    public EnemyType GreenEnemy;
    public EnemyType YellowEnemy;

    public List<EnemyType> enemyTypeAssets;
    private Dictionary<EnemyType, EnemyType> runtimeEnemyTypes;


    void Awake()
    {
        // init and find spawnpoints
        instance = this;

        spawnPoints = new List<SpawnPoint>();
        GameObject[] points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject p in points)
        {
            spawnPoints.Add(p.GetComponent<SpawnPoint>());
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        respawn();
    }


    // Update is called once per frame
    void Update()
    {
        // respawn when space pressed
        // for faster respawn because rerunning the game is too slow
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            respawn();
        }
    }


    void respawn()
    {
        // clear all spawned enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // reset TimeOfDay
        currTime = (TimeOfDay)Random.Range(0, 3);
        Debug.Log(currTime);

        // reset runtime types
        runtimeEnemyTypes = new Dictionary<EnemyType, EnemyType>();

        foreach (EnemyType original in enemyTypeAssets)
        {
            EnemyType clone = ScriptableObject.Instantiate(original);
            runtimeEnemyTypes[original] = clone;
        }

        // change stats
        changeOnTime();

        // spawn
        foreach (var point in spawnPoints)
        {
            point.spawn();
        }
    }


    // change stats based on TimeOfDay
    void changeOnTime()
    {
        switch (currTime)
        {
            case TimeOfDay.Morning:
                // Increases Spawn Rate of Archers by a range of .2 to .4.
                float range1 = Random.Range(0.2f, 0.4f);
                runtimeEnemyTypes[GreenEnemy].spawnRate += range1;
                runtimeEnemyTypes[YellowEnemy].spawnRate += range1;
                // Decreases Spawn Rate of Brown Enemies by a range of -.1 to -.3
                float range2 = Random.Range(0.1f, 0.3f);
                runtimeEnemyTypes[BrownEnemy].spawnRate -= range2;
                break;

            case TimeOfDay.Afternoon:
                // No Assassins can spawn during the Afternoon
                runtimeEnemyTypes[BlueEnemy].spawnRate = 0f;
                // All Grunts increase their Attack Power by 1
                runtimeEnemyTypes[RedEnemy].power++;
                runtimeEnemyTypes[BrownEnemy].power++;
                // All other Enemies increase/decrease their Spawn Rate by a range of -.2 to .2
                float range3 = Random.Range(-0.2f, 0.2f);
                runtimeEnemyTypes[GreenEnemy].spawnRate += range3;
                runtimeEnemyTypes[YellowEnemy].spawnRate += range3;
                break;

            case TimeOfDay.Night:
                // Assassins increase their Speed by a range of 0 to 2
                int range4 = Random.Range(0, 2);
                runtimeEnemyTypes[BlueEnemy].speed += range4;
                break;
        }
    }


    // return runtime type of given type
    public EnemyType getRuntimeEnemyType(EnemyType original)
    {
        return runtimeEnemyTypes[original];
    }
}
