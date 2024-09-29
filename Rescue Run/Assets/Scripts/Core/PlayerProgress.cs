using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;
using UnityEngine.UI;
using System;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField]
    private GenerationManager generationManager;

    [SerializeField]
    private ObjectRescueSAO objectRescueData;

    [SerializeField]
    private Transform startPoint;

    [SerializeField]
    private Transform endPointStartPhase;

    [SerializeField]
    private Transform endPointEndPhase;

    [SerializeField]
    private Slider progressSlider;

    [SerializeField]
    private ObstacleGeneration obstacleGeneration;

    private float totalDistance;

    private PlayerBehavior player;

    private int amountOfCatToRescue = 5;

    private ScenePersistenceManager scenePersistence;

    public bool MoveToEndpoint { get; set; } = false; 

    public float speed = 200;

    private int currentRescueCount = 0;

    private void Start()
    {
        amountOfCatToRescue = generationManager.GetAmountOfCatToRescue();
        totalDistance = Vector3.Distance(startPoint.position, endPointStartPhase.position);
        progressSlider.minValue = 0;
        progressSlider.maxValue = 1;
    }

    private void Update()
    {
        if (player == null)
            return;

        float distanceCovered = Vector3.Distance(startPoint.position, player.transform.position);

        float progress = distanceCovered / totalDistance;

        UpdateProgress(progress);

        if (MoveToEndpoint)
        {
            MovePlayerToEndPoint();
        }
    }

    public void InitialisePlayer(PlayerBehavior playerBehavior)
    {
        player = playerBehavior;
        player.RescueCompleted += OnRescueCompleted;
    }

    public void InitialiseScenePersistenceManager(ScenePersistenceManager scenePersistenceManager)
    {
        scenePersistence = scenePersistenceManager;
    }

    private void UpdateProgress(float progress)
    {
        progressSlider.value = progress;
    }

    private void OnRescueCompleted(int count)
    {
        currentRescueCount = count;
        if (count == amountOfCatToRescue)
        {
            HandleEndPhase();
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.RescueCompleted -= OnRescueCompleted;
        }
    }

    public void HandleEndPhase()
    {
        obstacleGeneration.DeactivateAllObstacles();
        player.EnableFullMovementAnimation();
        MoveToEndpoint = true;
        scenePersistence.SwitchCamera.SwitchToStartCamera();
    }

    private void MovePlayerToEndPoint()
    {
        Vector3 targetPosition = endPointEndPhase.position;
        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 direction = (targetPosition - player.transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            player.transform.rotation = lookRotation;
        }

        if (Vector3.Distance(player.transform.position, targetPosition) < 0.1f)
        {
            player.transform.position = targetPosition;
            MoveToEndpoint = false;
        }
    }

    public int GetMaxAmountOfRescueObjects()
    {
        return objectRescueData.Quantity;
    }

    public int GetCurrentAmountOfRescueObjects()
    {
        return currentRescueCount;
    }
}