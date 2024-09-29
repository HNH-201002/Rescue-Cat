using UnityEngine;
using Watermelon;

public class GenerationManager : MonoBehaviour
{
    [SerializeField]
    private ObstacleGeneration obstacleGeneration;

    [SerializeField]
    private ObjectRescueGeneration objectRescueGeneration;

    private int level = 1;

    private int initialObstacleCount = 10;
    private int initialObjectRescueCount = 5;

    private int objectRescueCount = 5;

    private void Awake()
    {
        level = LevelController.AtualCurrentLevelID;
        UpdateGameSettings(level);
    }

    private void UpdateGameSettings(int currentLevel)
    {
        int obstacleCount = initialObstacleCount + (int)(currentLevel * 0.5); 
        objectRescueCount = initialObjectRescueCount + (int)(Mathf.Sqrt(currentLevel));

        float mediumPercentage = Mathf.Clamp((currentLevel - 1) * 0.05f, 0f, 0.7f);

        int mediumObstacleCount = Mathf.RoundToInt(obstacleCount * mediumPercentage);
        int smallObstacleCount = obstacleCount - mediumObstacleCount;

        obstacleGeneration.InitializeObjects(smallObstacleCount, mediumObstacleCount, obstacleCount);
        objectRescueGeneration.InitializeObjects(objectRescueCount);
    }

    public int GetAmountOfCatToRescue()
    {
        return objectRescueCount;
    }
}
