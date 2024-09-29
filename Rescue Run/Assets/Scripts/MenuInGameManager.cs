using TMPro;
using UnityEngine;

public class MenuInGameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txt_rescueCount;

    [SerializeField]
    private MenuInGameBehavior menuInGameBehavior;

    [SerializeField]
    private MenuInGameBehavior failMenu;
    
    public void SetFailMenuActive(bool isActived)
    {
        UpdateTimeScale(isActived);

        failMenu.gameObject.SetActive(isActived);
    }

    public void SetActiveMenuInGame(bool isActived)
    {
        UpdateTimeScale(isActived);

        menuInGameBehavior.gameObject.SetActive(isActived);
    }

    private void UpdateTimeScale(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void SetAmountOfRescueObject(int count, int max)
    {
        txt_rescueCount.text = $"Cats Rescued: {count}/{max}";
    }
}
