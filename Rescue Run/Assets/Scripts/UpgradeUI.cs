using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    private Button btn_upgrade;

    [SerializeField]
    private Button btn_reset;

    [SerializeField]
    private TMP_Text txt_upgrade;

    private float speed = 5;

    public float Speed => speed;

    private void Start()
    {
        btn_upgrade.onClick.AddListener(Upgrade);
        btn_reset.onClick.AddListener(ResetUpgradeValue);
        txt_upgrade.text = speed.ToString();
    }

    private void Upgrade()
    {
        speed += 1f;
        txt_upgrade.text = speed.ToString();
    }

    private void ResetUpgradeValue() 
    {
        speed = 0;
        txt_upgrade.text = "1";
    }
}
