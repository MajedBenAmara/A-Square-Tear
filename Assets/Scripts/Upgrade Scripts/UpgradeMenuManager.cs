using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuManager : MonoBehaviour
{
    [SerializeField] public List<UpgradeButton> UpgradeButtons;
    public static UpgradeMenuManager Instance;
    public GameObject UpgradeMenuGameObject;

    private void Awake()
    {
        Instance = this;
    }

    public void OnClickUpgrade(int pressedButtonId)
    {
        GameManager.Instance.UpgradePlayer(pressedButtonId);
        DesactivateUpgradeMenu();
    }

    public void ActivateUpgradeMenu()
    {
        // before activating the upgrade menu we clear it from it's previous properties 
        CleanUpgradeMenu();
        UpgradeMenuGameObject.SetActive(true);
        List<UpgradeData> upgrades = GameManager.Instance.SelectedUpgrades;
        // set the different properties of very button(frame/icon sprite, upgrade name and description and so on) 
        for (int i = 0; i < UpgradeButtons.Count; i++)
        {
            UpgradeButtons[i].SetUpgradeProperties(upgrades[i]);
        }

        Time.timeScale = 0f;
    }

    public void DesactivateUpgradeMenu()
    {
        UpgradeMenuGameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CleanUpgradeMenu()
    {
        for (int i = 0; i < UpgradeButtons.Count; i++)
        {
            UpgradeButtons[i].CleanUpgradeButton();
        }
    }
}
