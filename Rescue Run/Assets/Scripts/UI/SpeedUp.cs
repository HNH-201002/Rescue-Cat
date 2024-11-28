using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Watermelon;
using DG.Tweening;

public class SpeedUp : MonoBehaviour
{
    [SerializeField]
    private Level level;

    [SerializeField]
    private TMP_Text contentText;

    [SerializeField]
    private TMP_Text countdownText;

    [SerializeField]
    private float countdownTime = 3f;

    [SerializeField]
    private TMP_Text popUpTextPrefab;

    private UIGame uIGame;

    private float tapCount = 0;
    private bool isCountdownActive = false;
    private PlayerBehavior playerBehavior;
    public event Action<bool> CountdownFinished;

    private void Start()
    {
        uIGame = FindFirstObjectByType<UIGame>().GetComponent<UIGame>();
        level.OnLevelCreated += OnLevelCreated;

        StartCoroutine(StartCountdown());
    }

    private void Update()
    {
        if (isCountdownActive)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    tapCount += 1;
                    CreatePopUpText(touch.position);
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                tapCount += 1;
                CreatePopUpText(Input.mousePosition);
            }
        }
    }

    IEnumerator StartCountdown()
    {
        uIGame.PlayHideAnimation();
        float timeLeft = countdownTime;
        isCountdownActive = true;

        while (timeLeft >= 0)
        {
            countdownText.text = Mathf.Ceil(timeLeft).ToString();
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        isCountdownActive = false;

        contentText.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);
        uIGame.PlayShowAnimation();
        CountdownFinished?.Invoke(true);
    }

    private void CreatePopUpText(Vector3 inputPosition)
    {
        if (popUpTextPrefab == null)
        {
            return;
        }

        TMP_Text popUpText = Instantiate(popUpTextPrefab, transform);

        RectTransform rt = popUpText.GetComponent<RectTransform>();
        rt.position = inputPosition;

        popUpText.text = "+" + tapCount;

        float moveDuration = 1f;
        float moveDistance = 100f;

        if (playerBehavior != null)
        {
            playerBehavior.SpeedUpMaxSpeed(tapCount);
        }

        popUpText.DOFade(0, moveDuration).OnKill(() => Destroy(popUpText.gameObject));

        rt.DOMoveY(rt.position.y + moveDistance, moveDuration).SetEase(DG.Tweening.Ease.OutQuad);
    }

    public void OnLevelCreated(PlayerBehavior playerBehavior)
    {
        this.playerBehavior = playerBehavior;
    }

    private void OnDestroy()
    {
        level.OnLevelCreated -= OnLevelCreated;
    }
}
