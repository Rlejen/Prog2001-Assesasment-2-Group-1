using UnityEngine;

public class SnowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject snowPrefab;
    [SerializeField] private Transform player;

    [SerializeField] private float spawnDistance = 30f;
    [SerializeField] private float spacing = 10f;
    [SerializeField] private Vector2 minMaxXPos;

    private float lastZ;

    void Start()
    {
        lastZ = player.position.z;

        // Initial spawn
        for (int i = 0; i < 10; i++)
        {
            SpawnSnow();
        }
    }

    void Update()
    {
        // Keep spawning ahead of player
        if (player.position.z + spawnDistance > lastZ)
        {
            SpawnSnow();
        }
    }

    void SpawnSnow()
    {
        float randomX = Random.Range(minMaxXPos.x, minMaxXPos.y);
        float randomSpacing = Random.Range(spacing * 0.8f, spacing * 1.2f);

        lastZ += randomSpacing;

        Vector3 spawnPos = new Vector3(randomX, 1f, lastZ);
        Instantiate(snowPrefab, spawnPos, Quaternion.identity);
    }
}