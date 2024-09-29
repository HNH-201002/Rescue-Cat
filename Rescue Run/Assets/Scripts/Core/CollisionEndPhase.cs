using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class CollisionEndPhase : MonoBehaviour
{
    [SerializeField]
    private MenuInGameManager menuInGameManager;

    [SerializeField]
    private PlayerProgress playerProgress;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PhysicsHelper.TAG_PLAYER)
        {
            menuInGameManager.SetActiveMenuInGame(true);
            menuInGameManager.SetAmountOfRescueObject(playerProgress.GetCurrentAmountOfRescueObjects(),
                                                      playerProgress.GetMaxAmountOfRescueObjects());
        }
    }
}
