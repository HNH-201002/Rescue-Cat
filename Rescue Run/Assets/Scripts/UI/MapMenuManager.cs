using UnityEngine;

public class MapMenuManager : MonoBehaviour
{
    [SerializeField]
    private int amountOfMaps = 10;

    [SerializeField]
    private MapElement mapElementPrefab;

    [SerializeField]
    private Transform containArea;

    [SerializeField]
    private UpgradeUI upgradeUI;

    private void Start()
    {
        for (int i = 0; i < amountOfMaps; i++)
        {
            GameObject mapGO = Instantiate(mapElementPrefab.gameObject);

            mapGO.transform.parent = containArea;
            mapGO.transform.localScale = Vector3.one;

            mapGO.GetComponent<MapElement>().SetLevelText(i + 1);
            mapGO.GetComponent<MapElement>().SetUpgradeUI(upgradeUI);
        }
    }
}
