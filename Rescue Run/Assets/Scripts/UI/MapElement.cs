using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;
using Watermelon.Upgrades;

public class MapElement : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txt_level;

    [SerializeField]
    private Button btn_map;

    private int level;

    private UpgradeUI upgradeUI;

    private void Start()
    {
        btn_map.onClick.AddListener(ChangeScene);
    }

    public void SetLevelText(int level)
    {
        txt_level.text = level.ToString();
        this.level = level;
    }

    public void SetUpgradeUI(UpgradeUI upgradeUI)
    {
        this.upgradeUI = upgradeUI;
    }

    private void ChangeScene()
    {
        GameController.Unload(() =>
        {
            LevelController.ActivateLevel(1, level);
            LevelController.SetSpeed(upgradeUI.Speed);
        });
    }

}
