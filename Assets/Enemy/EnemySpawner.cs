using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public DayNightCycle timeSystem;
    public Transform player;

    public enum SpawnLocation
    {
        Castle,
        Shore,
        Random
    }

    [System.Serializable]
    public class EnemyType
    {
        public GameObject prefab;
        public string name;
        public bool spawnAtNight;
        public SpawnLocation location;
        public float spawnChance = 1f;

        [Header("Captain Extras")]
        public GameObject shipPrefab;
        public GameObject crewPrefab;
        public int crewCount = 3;
    }


    [Header("Spawn Points")]
    public Transform[] castleSpawnPoints;
    public Transform[] shoreSpawnPoints;
    public Transform[] randomSpawnPoints;

    [Header("Enemy Settings")]
    public List<EnemyType> enemyTypes;
    public float spawnInterval = 30f;
    public int maxEnemies = 10;

    private float timer;
    private List<GameObject> spawnedEnemies = new();
    private int lastSpawnedDay = -1;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            TrySpawnEnemy();
            timer = 0f;
        }

        spawnedEnemies.RemoveAll(e => e == null);
    }

    void TrySpawnEnemy()
    {
        if (spawnedEnemies.Count >= maxEnemies) return;

        bool isNight = timeSystem.IsNight;
        int currentDay = Mathf.FloorToInt(timeSystem.GetTimeInHours() / 24f);

        foreach (EnemyType type in enemyTypes)
        {
            if (type.spawnAtNight != isNight) continue;
            if (Random.value > type.spawnChance) continue;

            if (type.location == SpawnLocation.Castle && isNight)
            {
                SpawnFromList(type, castleSpawnPoints);
            }

            else if (type.location == SpawnLocation.Shore && !isNight && currentDay != lastSpawnedDay)
            {
                SpawnFromList(type, shoreSpawnPoints);
                lastSpawnedDay = currentDay;
            }

            else if (type.location == SpawnLocation.Random)
            {
                SpawnFromList(type, randomSpawnPoints);
            }
        }
    }

void SpawnFromList(EnemyType type, Transform[] points)
{
    if (points.Length == 0) return;
    Transform point = points[Random.Range(0, points.Length)];

    GameObject enemy = Instantiate(type.prefab, point.position, Quaternion.identity);
    spawnedEnemies.Add(enemy);

    EnemyAI ai = enemy.GetComponent<EnemyAI>();
    if (ai != null) ai.player = player;

    // 🏴‍☠️ Kapitán? Spawnni loď a posádku!
    if (type.shipPrefab != null)
    {
        Vector3 shipOffset = point.forward * 5f;
        Instantiate(type.shipPrefab, point.position + shipOffset, Quaternion.identity);
    }

    if (type.crewPrefab != null && type.crewCount > 0)
    {
        for (int i = 0; i < type.crewCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
            GameObject crew = Instantiate(type.crewPrefab, point.position + offset, Quaternion.identity);
            spawnedEnemies.Add(crew);

            EnemyAI crewAI = crew.GetComponent<EnemyAI>();
            if (crewAI != null) crewAI.player = player;
        }
    }
}

}
