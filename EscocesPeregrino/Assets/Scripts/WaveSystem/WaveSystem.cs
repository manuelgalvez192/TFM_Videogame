using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{

    private EnemySpawner spawner;

    public int currentWave = 0;
    public int enemiesToSpawn = 0;
    public int enemiesLeft = 0;

    public bool changeWave;
    // Start is called before the first frame update
    void Start()
    {
        spawner= GetComponent<EnemySpawner>();
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWave()
    {
        spawner.stopSpawning = false;
        changeWave= false;
        StartCoroutine(StartWaveRoutine());

    }

    public void EndWave()
    {
        StartCoroutine(EndWaveRoutine());
    }
    IEnumerator StartWaveRoutine()
    {
        yield return new WaitForSeconds(5f);
        if(enemiesLeft != enemiesToSpawn)
        {
            spawner.Spawn();
        }
    }
    IEnumerator EndWaveRoutine()
    {
        yield return new WaitForSeconds(2f);

        changeWave = true;
        currentWave++;
        enemiesToSpawn += 5;

        StartWave();
    }
}
