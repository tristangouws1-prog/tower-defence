using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [Header("Wave Settings")]
    public List<Wave> waves = new List<Wave>();
    public Transform spawnPoint;
    public float timeBetweenWaves = 10f;

    int currentWave = 0;
    int enemiesAlive = 0;
    bool spawning = false;

    public int CurrentWave => currentWave;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(StartNextWave(3f));
    }

    IEnumerator StartNextWave(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (currentWave >= waves.Count) yield break;
        UIManager.Instance?.UpdateWave(currentWave + 1);
        yield return StartCoroutine(SpawnWave(waves[currentWave]));
    }

    IEnumerator SpawnWave(Wave wave)
    {
        spawning = true;
        foreach (WaveGroup group in wave.groups)
        {
            for (int i = 0; i < group.count; i++)
            {
                GameObject enemy = Instantiate(group.enemyPrefab, spawnPoint.position, Quaternion.identity);
                enemiesAlive++;
                yield return new WaitForSeconds(group.spawnInterval);
            }
            yield return new WaitForSeconds(group.delayAfterGroup);
        }
        spawning = false;
    }

    public void OnEnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0 && !spawning)
        {
            currentWave++;
            if (currentWave < waves.Count)
                StartCoroutine(StartNextWave(timeBetweenWaves));
            else
                UIManager.Instance?.ShowVictory();
        }
    }
}

[System.Serializable]
public class Wave
{
    public string waveName;
    public List<WaveGroup> groups = new List<WaveGroup>();
}

[System.Serializable]
public class WaveGroup
{
    public GameObject enemyPrefab;
    public int count = 5;
    public float spawnInterval = 0.8f;
    public float delayAfterGroup = 1f;
}
