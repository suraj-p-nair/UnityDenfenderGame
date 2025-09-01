using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Models.Enums;

public class UpgradeManager : MonoBehaviour
{
    public GameObject UpgradeCanvas;
    public Button UpgradeButton1;
    public Button UpgradeButton2;
    public TMP_Text UpgradeText1;
    public TMP_Text UpgradeText2;

    private Action<Upgrade> _onUpgradeSelected;

    public void ShowUpgrades(Upgrade upgrade1, Upgrade upgrade2, Action<Upgrade> onUpgradeSelected)
    {
        _onUpgradeSelected = onUpgradeSelected;

        UpgradeText1.text = upgrade1.Name + "\n" + upgrade1.Description;
        UpgradeText2.text = upgrade2.Name + "\n" + upgrade2.Description;

        UpgradeButton1.GetComponent<Image>().color = upgrade1.RarityColor;
        UpgradeButton2.GetComponent<Image>().color = upgrade2.RarityColor;


        UpgradeButton1.onClick.RemoveAllListeners();
        UpgradeButton2.onClick.RemoveAllListeners();

        UpgradeButton1.onClick.AddListener(() => SelectUpgrade(upgrade1));
        UpgradeButton2.onClick.AddListener(() => SelectUpgrade(upgrade2));

        UpgradeCanvas.SetActive(true);
    }


    private void SelectUpgrade(Upgrade chosen)
    {
        UpgradeCanvas.SetActive(false);
        _onUpgradeSelected?.Invoke(chosen);
    }
}
