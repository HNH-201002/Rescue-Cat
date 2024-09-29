using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class ObjectRescueGeneration : MonoBehaviour
{
    [SerializeField]
    private ObjectRescueSAO objectSpawnData;

    [SerializeField]
    private float maxDistance = 400;

    [SerializeField]
    private float startPosition = 50;

    public void InitializeObjects(int quantity)
    {
        float minDistanceBetweenObjects = (maxDistance - startPosition) / quantity;
        HashSet<Vector3> spawnPositions = new HashSet<Vector3>();

        for (int i = 0; i < quantity; i++)
        {
            float spawnZ = startPosition + i * minDistanceBetweenObjects;
            if (spawnZ > maxDistance) break;

            float randomX = Random.Range(-22, 22);
            Vector3 spawnPos = new Vector3(randomX, 0f, spawnZ);
            spawnPositions.Add(spawnPos);
        }

        foreach (Vector3 spawnPos in spawnPositions)
        {
            int randomIndex = Random.Range(0, objectSpawnData.Prefabs.Length);
            GameObject animalObject = Instantiate(objectSpawnData.Prefabs[randomIndex], spawnPos, Quaternion.identity, this.transform);

            AnimalBehaviour animalBehaviour = animalObject.GetComponent<AnimalBehaviour>();
            Animal animal = LevelController.GetAnimal(0);
            animalBehaviour.Initialise(animal);
            animalBehaviour.transform.localScale = Vector3.one;

            animalObject.SetActive(true);
            animalBehaviour.SetSpawnPoint(spawnPos);
        }
    }
}
