using UnityEngine;

public class ScenePersistenceManager : MonoBehaviour
{
    [SerializeField]
    private SwitchCamera switchCamera;

    public SwitchCamera SwitchCamera => switchCamera;
}
