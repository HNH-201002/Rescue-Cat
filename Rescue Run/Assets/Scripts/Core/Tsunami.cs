using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class Tsunami : MonoBehaviour
{
    [SerializeField]
    private MenuInGameManager menuInGameManager;

    [SerializeField]
    private CollisionStartPhase collisionStartPhase;

    [SerializeField]
    private float speedStartPhase = 10;

    [SerializeField]
    private float speedEndPhase = 20;

    [SerializeField]
    private SpeedUp speedUp;

    private float speed;

    private bool isCountDownFinished = false;

    private void Start()
    {
        collisionStartPhase.PlayerCompletedStartPhase += UpgradeSpeed;
        speedUp.CountdownFinished += IsCountDownFinished;
        speed = speedStartPhase;
    }

    private void Update()
    {
        if (isCountDownFinished)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PhysicsHelper.TAG_CHARACTER)
        {
            menuInGameManager.SetFailMenuActive(true);
        }
    }

    private void UpgradeSpeed()
    {
        speed = speedEndPhase;
    }

    private void IsCountDownFinished(bool isCountDownFinished)
    {
        this.isCountDownFinished = isCountDownFinished;
    }

    private void OnDestroy()
    {
        collisionStartPhase.PlayerCompletedStartPhase -= UpgradeSpeed;
        speedUp.CountdownFinished -= IsCountDownFinished;
    }
}
