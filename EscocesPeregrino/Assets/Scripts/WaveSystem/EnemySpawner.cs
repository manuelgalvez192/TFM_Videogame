using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner Instance;
    private WaveSystem waveSystem;

    public Transform[] spawners;
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
                print("Spawned");
                //instance = SpawnPool.Instance.Spawn(enemies[Random.Range(0, enemies.Length)].transform, spawners[Random.Range(0, spawners.Length)]);
                 DaniPool.Instance.Spawn(enemies[Random.Range(0, enemies.Length)], spawners[Random.Range(0, spawners.Length)]);
                waveSystem.enemiesLeft++;
                enemiesSpawned++;
                yield return new WaitForSeconds(3);
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
