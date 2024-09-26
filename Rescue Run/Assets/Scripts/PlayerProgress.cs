using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;
using UnityEngine.UI;
using System;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField]
    private Generator generator;

    [SerializeField]
    private Transform startPoint;

    [SerializeField]
    private Transform endPoint;

    [SerializeField]
    private Slider progressSlider;

    private float totalDistance;

    private PlayerBehavior player;

    private int amountOfCatToRescue = 0;

    private void Start()
    {
        amountOfCatToRescue = generator.GetAmountOfCatToRescue();
        totalDistance = Vector3.Distance(startPoint.position, endPoint.position);
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
    }

    public void Initialise(PlayerBehavior playerBehavior)
    {
        player = playerBehavior;
        player.RescueCompleted += OnRescueCompleted;
    }

    private void UpdateProgress(float progress)
    {
        progressSlider.value = progress;
    }

    private void OnRescueCompleted(int count)
    {
        if (count == amountOfCatToRescue)
        {
            Debug.Log("Success");
        }
    }

    private void OnDisable()
    {
        if(player != null)
        {
            player.RescueCompleted -= OnRescueCompleted;
        }
    }
}
