using UnityEngine;
using Watermelon;
using System;

public class CollisionStartPhase : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress playerProgress;

    public event Action PlayerCompletedStartPhase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PhysicsHelper.TAG_PLAYER)
        {
            PlayerCompletedStartPhase?.Invoke();
            playerProgress.HandleEndPhase();
        }
    }
}
