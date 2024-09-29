using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    [SerializeField]
    private ObjectObstacleSAO[] prefabs;

    [SerializeField]
    private float maxDistance = 400;

    [SerializeField]
    private float startPosition = 50;

    [SerializeField]
    private float fluctuationPercentage = 0.1f;

    private Dictionary<ObstacleSizeType, List<GameObject>> prefabsBySize;

    private List<GameObject> spawnedObstacles = new List<GameObject>();
    private void Awake()
    {
        InitializePrefabDictionary();
    }

    private void InitializePrefabDictionary()
    {
        prefabsBySize = new Dictionary<ObstacleSizeType, List<GameObject>>();

        foreach (ObjectObstacleSAO obstacle in prefabs)
        {
            ObstacleSizeType sizeType = obstacle.ObstacleType.ObstacleSizeType;
            if (!prefabsBySize.ContainsKey(sizeType))
            {
                prefabsBySize[sizeType] = new List<GameObject>();
            }
            prefabsBySize[sizeType].Add(obstacle.ObstacleType.Prefab);
        }
    }

    public void InitializeObjects(int smallObstacleCount, int mediumObstacleCount, int totalQuantity)
    {
        float minDistanceBetweenObjects = maxDistance / totalQuantity;
        float fluctuationRange = minDistanceBetweenObjects * fluctuationPercentage;

        List<Vector3> spawnPositions = new List<Vector3>();

        for (int i = 0; i < totalQuantity; i++)
        {
            float spawnZ = startPosition + i * minDistanceBetweenObjects;
            spawnZ += Random.Range(-fluctuationRange, fluctuationRange);

            if (spawnZ > maxDistance) break;

            float randomX = Random.Range(-22, 22);
            Vector3 spawnPos = new Vector3(randomX, 0f, spawnZ);

            spawnPositions.Add(spawnPos);
        }

        ShuffleList(spawnPositions);

        List<GameObject> obstaclesToSpawn = new List<GameObject>();

        for (int i = 0; i < mediumObstacleCount; i++)
        {
            obstaclesToSpawn.Add(GetRandomPrefabBySize(ObstacleSizeType.Medium));
        }

        for (int i = 0; i < smallObstacleCount; i++)
        {
            obstaclesToSpawn.Add(GetRandomPrefabBySize(ObstacleSizeType.Small));
        }

        ShuffleList(obstaclesToSpawn);

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            GameObject spawnedObject = Instantiate(obstaclesToSpawn[i], spawnPositions[i], RandomYRotation());
            spawnedObstacles.Add(spawnedObject);
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private GameObject GetRandomPrefabBySize(ObstacleSizeType sizeType)
    {
        var prefabsOfSize = prefabsBySize[sizeType];
        int randomIndex = Random.Range(0, prefabsOfSize.Count);
        return prefabsOfSize[randomIndex];
    }

    private Quaternion RandomYRotation()
    {
        float randomYAngle = Random.Range(0f, 360f);
        return Quaternion.Euler(0f, randomYAngle, 0f);
    }

    public void DeactivateAllObstacles()
    {
        foreach (GameObject obstacle in spawnedObstacles)
        {
            obstacle.SetActive(false); 
        }
    }
}