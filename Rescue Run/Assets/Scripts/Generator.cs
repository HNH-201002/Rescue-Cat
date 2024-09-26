using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using Watermelon;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private PrefabSpawnData catSpawnData;

    [SerializeField]
    private List<GameObject> obstaclePrefabs;

    [SerializeField]
    private GameObject endPosition;

    [SerializeField]
    private float minDistanceBetweenCats = 2.0f;
    [SerializeField]
    private float minDistanceBetweenObstacles = 2.0f; 
    [SerializeField]
    private int obstacleCount = 10; 

    private List<Vector3> spawnedPositions = new List<Vector3>();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Spawn cats
        int catsSpawned = 0;

        while (catsSpawned < catSpawnData.Quantity)
        {
            Vector3 spawnPos = GenerateValidPosition();
            if (spawnPos != Vector3.zero)
            {
                int random = Random.Range(0, catSpawnData.Prefabs.Length);
                GameObject animalObject = Instantiate(catSpawnData.Prefabs[random], spawnPos, Quaternion.identity);
                AnimalBehaviour animalBehaviour = animalObject.GetComponent<AnimalBehaviour>();
                Animal animal = LevelController.GetAnimal(0);
                animalBehaviour.Initialise(animal);
                animalBehaviour.transform.localScale = Vector3.one;

                animalObject.SetActive(true);
                animalBehaviour.SetSpawnPoint(spawnPos);
                spawnedPositions.Add(spawnPos);
                catsSpawned++;
            }
        }

        int obstaclesSpawned = 0;
        while (obstaclesSpawned < obstacleCount)
        {
            Vector3 obstaclePos = GenerateObstaclePosition();
            if (obstaclePos != Vector3.zero)
            {
                Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)], obstaclePos, Quaternion.identity);
                spawnedPositions.Add(obstaclePos); 
                obstaclesSpawned++;
            }
        }
    }

    private Vector3 GenerateValidPosition()
    {
        int maxAttempts = 100;
        Vector3 spawnPos = Vector3.zero;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float randomX = Random.Range(-22, 22);
            float randomY = 0f;
            float randomZ = Random.Range(0f, endPosition.transform.position.z); 
            spawnPos = new Vector3(randomX, randomY, randomZ);

            if (CheckPositionValidity(spawnPos))
            {
                return spawnPos;
            }
        }

        return Vector3.zero; 
    }

    private Vector3 GenerateObstaclePosition()
    {
        int maxAttempts = 100;
        Vector3 obstaclePos = Vector3.zero;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float randomX = Random.Range(-22,22);
            float randomY = 0f;
            float randomZ = Random.Range(0f, endPosition.transform.position.z);
            obstaclePos = new Vector3(randomX, randomY, randomZ);

            if (CheckObstacleValidity(obstaclePos))
            {
                return obstaclePos;
            }
        }

        return Vector3.zero; 
    }

    private bool CheckPositionValidity(Vector3 position)
    {
        foreach (var pos in spawnedPositions)
        {
            if (Vector3.Distance(pos, position) < minDistanceBetweenCats)
            {
                return false; 
            }
        }

        return true;
    }

    private bool CheckObstacleValidity(Vector3 position)
    {
        foreach (var pos in spawnedPositions)
        {
            if (Vector3.Distance(pos, position) < minDistanceBetweenObstacles)
            {
                return false; 
            }
        }

        return true;
    }

    public int GetAmountOfCatToRescue()
    {
        return catSpawnData.Quantity;
    }
}

[System.Serializable]
struct PrefabSpawnData
{
    public GameObject[] Prefabs;
    public int Quantity;
}
