using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Watermelon;

public class SpeedUI : MonoBehaviour
{
    [SerializeField]
    private SpeedUp speedUp;

    [SerializeField]
    private Level level;

    [SerializeField]
    private TMP_Text txt_MaxSpeed;

    [SerializeField]
    private TMP_Text txt_CurrentSpeed;

    private PlayerBehavior playerBehavior;

    private bool isCountDownFinished = false;

    private void Start()
    {
        level.OnLevelCreated += OnLevelCreated;
        speedUp.CountdownFinished += OnCountDownFinished;
    }

    private void Update()
    {
        if (playerBehavior != null && isCountDownFinished)
        {
            txt_CurrentSpeed.text = "Current speed : " + playerBehavior.GetCurrentSpeed().ToString();
        }
    }

    public void OnCountDownFinished(bool isCountDownFinished)
    {
        this.isCountDownFinished = isCountDownFinished;
        txt_MaxSpeed.text = "Max speed : " + playerBehavior.GetMaxSpeed().ToString();
    }

    public void OnLevelCreated(PlayerBehavior playerBehavior)
    {
        this.playerBehavior = playerBehavior;
    }

    private void OnDestroy()
    {
        level.OnLevelCreated -= OnLevelCreated;
        speedUp.CountdownFinished -= OnCountDownFinished;
    }
}
