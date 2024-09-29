using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObjects/Obstacle", order = 2)]
public class ObjectObstacleSAO : ScriptableObject
{
    [SerializeField]
    private ObstacleType obstacleType;

    public ObstacleType ObstacleType => obstacleType;
}

[System.Serializable]
public struct ObstacleType
{
    public GameObject Prefab;
    public ObstacleSizeType ObstacleSizeType;
}