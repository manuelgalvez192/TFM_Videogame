using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Camera mainCamera;
    public static EnemySpawner Instance;
    private WaveSystem waveSystem;

    public Transform[] spawners;
    private List <Transform> visibleSpawns = new List <Transform>();
    public GameObject[] enemies;

    [SerializeField] private float timeToSpawn = 1f;

    private Transform instance;

    public bool stopSpawning;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        waveSystem= GetComponent<WaveSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        visibleSpawns.Clear();
        foreach(Transform spawn in spawners)
        {
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(spawn.position);

            if (screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0)
            {
                visibleSpawns.Add(spawn);
            }
        }
    }

    public void Spawn()
    {
     
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        int enemiesSpawned = 0;
        yield return new WaitForSeconds(timeToSpawn);

        while(stopSpawning !=true)
        {
            if(enemiesSpawned < waveSystem.enemiesToSpawn)
            {
                List<Transform> nonVisibleSpawns = new List<Transform>(spawners);
                foreach (Transform spawn in visibleSpawns)
                {
                    nonVisibleSpawns.Remove(spawn);
                }

                if (nonVisibleSpawns.Count > 0)
                {
                    Transform randomSpawn = nonVisibleSpawns[UnityEngine.Random.Range(0, nonVisibleSpawns.Count)];
                    print("Spawned");
                    //instance = SpawnPool.Instance.Spawn(enemies[Random.Range(0, enemies.Length)].transform, spawners[Random.Range(0, spawners.Length)]);
                    DaniPool.Instance.Spawn(enemies[UnityEngine.Random.Range(0, enemies.Length)], randomSpawn);
                    waveSystem.enemiesLeft++;
                    enemiesSpawned++;
                    yield return new WaitForSeconds(3);
                }
              
            }
            else
            {
                waveSystem.changeWave = false;
                stopSpawning = true;

                waveSystem.enemiesLeft = 0;
                enemiesSpawned = 0;
                waveSystem.EndWave();
            }
        }
    }

  
}
