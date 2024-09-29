using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;

public class MenuInGameBehavior : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txt_rescueCount;

    [SerializeField]
    private Button btn_next;

    [SerializeField]
    private Button btn_replay;

    [SerializeField]
    private Button btn_backToMenu;

    private void Start()
    {
        if (btn_next != null)
            btn_next.onClick.AddListener(HandleNextButton);

        if (btn_replay != null)
            btn_replay.onClick.AddListener(HandleReplayButton);

        if (btn_backToMenu != null)
            btn_backToMenu.onClick.AddListener(HandleBackToMenuButton);
    }

    private void HandleNextButton()
    {
        ChangeScene(1,LevelController.AtualCurrentLevelID + 1);
    }

    private void HandleReplayButton()
    {
        ChangeScene(1,LevelController.AtualCurrentLevelID);
    }

    private void HandleBackToMenuButton()
    {
        ChangeScene(0,0);
    }

    private void ChangeScene(int index,int actualLevel)
    {
        GameController.Unload(() =>
        {
            LevelController.ActivateLevel(index, actualLevel);
        });
    }
}
