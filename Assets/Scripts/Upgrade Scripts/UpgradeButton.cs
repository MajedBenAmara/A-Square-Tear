using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Image _UpgradeIcon;
    [SerializeField] private TextMeshProUGUI _UpgradeName;
    [SerializeField] private TextMeshProUGUI _UpgradeDescription;

    // Set the different properties(frame/icon sprite, upgrade name and description and so on) of every button when the upgrade mebu is activated 
    public void SetUpgradeProperties(UpgradeData upgrade)
    {
        _UpgradeIcon.sprite = upgrade.Icon;
        _UpgradeName.text = upgrade.UpgradeName + " Lv " + upgrade.UpgradeLevel;

        int upgradeLevel = upgrade.UpgradeLevel -1;
        string upgradeName = upgrade.UpgradeName;

        switch (upgrade.type)
        {
            case UpgradeType.StatUpgrade:

                if (upgradeName == "Max Health")
                {
                    string maxHealthText = GameManager.Instance.MaxHealthBonus[upgradeLevel].ToString();
                    _UpgradeDescription.text = upgrade.Description + maxHealthText;
                }

                if (upgradeName == "Crite Dmg")
                {
                    string criteDmgText = GameManager.Instance.CritDmgBonus[upgradeLevel].ToString();
                    _UpgradeDescription.text = upgrade.Description + criteDmgText;
                }

                if (upgradeName == "Crite Rate")
                {
                    string criteRateText = GameManager.Instance.CritRateBonus[upgradeLevel].ToString();
                    _UpgradeDescription.text = upgrade.Description + criteRateText;
                }

                if (upgradeName == "Movement Speed")
                {
                    string movementSpeedText = GameManager.Instance.MovementSpeedBonus[upgradeLevel].ToString();
                    _UpgradeDescription.text = upgrade.Description + movementSpeedText;
                }

                break;
            case UpgradeType.WeaponUpgrade:

                if (upgradeName == "Weapon Dmg")
                {
                    string weaponDmgText = GameManager.Instance.WeaponDmgBonus[upgradeLevel].ToString();
                    _UpgradeDescription.text = upgrade.Description + weaponDmgText;
                }

                if (upgradeName == "Weapon Scale")
                {
                    string weaponScaleText = (GameManager.Instance.WeaponScaleBonus[upgradeLevel]*100).ToString();
                    _UpgradeDescription.text = "Increase The Weapon Scale By " + weaponScaleText + "%";
                }

                if (upgradeName == "Weapon Speed")
                {
                     
                    if (GameManager.Instance.GetSelectedWeaponHolder().name == "DiscHolder")
                    {
                        string rotationSpeedText = GameManager.Instance.DiscRotaionSpeedBonus[upgradeLevel].ToString();
                        _UpgradeDescription.text = "Increase The Disc Rotaiton Speed By " + rotationSpeedText + "%";  
                    }
                    else
                    {
                        string fireRateText = (GameManager.Instance.FireRateBonus[upgradeLevel]*100).ToString();
                        _UpgradeDescription.text = "Increase The Fire Rate By " + fireRateText + "%";   
                    }

                }

                if (upgradeName == "Slash Awakening" || upgradeName == "Disc Awakening" || upgradeName == "Bullet Awakening")
                {
                    _UpgradeName.text = upgrade.UpgradeName;
                    _UpgradeDescription.text = upgrade.Description;
                }

                break;
            default:
                break;

        }

    }

    public void CleanUpgradeButton()
    {
        _UpgradeIcon.sprite = null;
        _UpgradeName.text = "";
        _UpgradeDescription.text = "";
    }
}
