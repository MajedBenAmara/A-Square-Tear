using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] private bool WeaponDidCrit;
    private bool _PlayerCanLvlUp = true;
    private int _SelectedWeapoonIndex;
    public List<UpgradeData> SelectedUpgrades;
    private int _WeaponUpgradesCount = 0;
    [SerializeField] private List<UpgradeData> _AcquiredUpgrades;
    private UpgradeData _ChosenAwakening;

    public EndGameMenu EndGameMenuObject;
    public List<UpgradeData> Upgrades, AwakeningUpgrades;
    public static GameManager Instance;
    public Player PlayerGameObject;
    public Enemy EnemyGameObject;
    public int[] MaxHealthBonus = {}, CritDmgBonus = {}, CritRateBonus = {}, MovementSpeedBonus = {},
    WeaponDmgBonus = { }, DiscRotaionSpeedBonus = {};
    public float[] WeaponScaleBonus = {}, FireRateBonus = { };
    public int[] EnemyExpAmounts = {}, ReqExpToLvlUp = {}, HealingAmount = {};
    public Color[] ExpDropColors = {};
    public int HealingLevelIndex = 0;
    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChange;
    public GameObject[] WeaponList = {};
   

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;

        // selecting the weapon to be activated based on what the player choice in the weapon selection menu
        _SelectedWeapoonIndex = PlayerPrefs.GetInt("SelectedWeapon");
        ActivateWeapon(_SelectedWeapoonIndex);
    }

    // When an enemy die he will activate this function
    // That will activate the OnExperienceChange event with the amount of experience that enemy provide
    public void AddExperience(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }

    // Whenever the player is enable\active every time the OnExperienceChange event happen he will call this function that will add to his current exp 
    // the amount of exp the enemy provide and level him up
    public void HandleExperienceChange(int newExperience)
    {
        PlayerGameObject.CurrentExp += newExperience;

        if (PlayerGameObject.CurrentExp >= ReqExpToLvlUp[PlayerGameObject.PlayerLvl - 1])
        {
            LevelUp();
        }
    }

    // this func called when we want to level up our player
    public void LevelUp()
    {
        // Leveling up the player
        PlayerGameObject.CurrentExp -= ReqExpToLvlUp[PlayerGameObject.PlayerLvl - 1];
        PlayerGameObject.PlayerLvl += 1;

        // Upgrading
        if (_PlayerCanLvlUp)
        {
            if (SelectedUpgrades == null)
                SelectedUpgrades = new List<UpgradeData>();
            SelectedUpgrades.Clear();
            SelectedUpgrades.AddRange(GetUpgradesFromList(UpgradeMenuManager.Instance.UpgradeButtons.Count));
            UpgradeMenuManager.Instance.ActivateUpgradeMenu();
        }
        // when the player his max level e can level up anymore
        if(PlayerGameObject.PlayerLvl >= PlayerGameObject.MaxLevel)
        {
            PlayerGameObject.PlayerLvl = PlayerGameObject.MaxLevel;
            _PlayerCanLvlUp = false;
        }

    }

    // Upgrading the player stats and weapon stats based on the upgrade type and name
    public void UpgradePlayer(int SelectedUpgradeId)
    {
        UpgradeData upgradeData = SelectedUpgrades[SelectedUpgradeId];
        string upgradeName = upgradeData.UpgradeName;
        int upgradeLevel = upgradeData.UpgradeLevel -1;
        Weapon selectedWeapon = GetSelectedWeapon();
        Holder selectedWeaponHolder = GetSelectedWeaponHolder();
        BulletHolder bulletHolder = selectedWeaponHolder.gameObject.GetComponent<BulletHolder>();
        if (_AcquiredUpgrades == null)
            _AcquiredUpgrades = new List<UpgradeData>();

        switch (upgradeData.type)
        {
            case UpgradeType.StatUpgrade:
                switch (upgradeName)
                {
                    case "Max Health":

                        Debug.Log(upgradeName + " " + upgradeLevel);
                        PlayerGameObject.UpgradeMaxHealth(MaxHealthBonus[upgradeLevel]);
                        if (upgradeLevel+1 != 3)
                        {
                            Upgrades.Add(upgradeData.NextUpgrade);
                        }
                        break;

                    case "Crite Dmg":

                        Debug.Log(upgradeName + " " + upgradeLevel);
                        PlayerGameObject.UpgradeCriteDmg(CritDmgBonus[upgradeLevel]);
                        if (upgradeLevel + 1 != 3)
                        {
                            Upgrades.Add(upgradeData.NextUpgrade);
                        }
                        break;

                    case "Crite Rate":

                        Debug.Log(upgradeName + " " + upgradeLevel);
                        PlayerGameObject.UpgradeCriteRate(CritRateBonus[upgradeLevel]);
                        if (upgradeLevel + 1 != 3)
                        {
                            Upgrades.Add(upgradeData.NextUpgrade);
                        }
                        break;

                    case "Movement Speed":

                        Debug.Log(upgradeName + " " + upgradeLevel);
                        PlayerGameObject.UpgradeMovementSpeed(MovementSpeedBonus[upgradeLevel]);
                        if (upgradeLevel + 1 != 3)
                        {
                            Upgrades.Add(upgradeData.NextUpgrade);
                        }
                        break;

                    default:
                        break;
                }
                
             break;

            case UpgradeType.WeaponUpgrade:

                if (selectedWeaponHolder.name == "DiscHolder")
                {

                    FullDisc fullDisc = selectedWeaponHolder.gameObject.GetComponent<DiscHolder>().FullDisc.gameObject.GetComponent<FullDisc>() ;

                    switch (upgradeName)
                    {
                        case "Weapon Dmg":

                            Debug.Log(upgradeName + " " + upgradeLevel);
                            fullDisc.UpgradeWeaponDmg(WeaponDmgBonus[upgradeLevel]);
                            if (upgradeLevel + 1 == 3)
                            {
                                _WeaponUpgradesCount++;
                            }
                            else
                            {
                                Upgrades.Add(upgradeData.NextUpgrade);
                            }
                            break;

                        case "Weapon Scale":

                            Debug.Log(upgradeName + " " + upgradeLevel);
                            fullDisc.UpgradeScale(WeaponScaleBonus[upgradeLevel]);

                            if (upgradeLevel + 1 == 1)
                            {
                                Upgrades.Add(upgradeData.NextUpgrade);
                            }

                            if (upgradeLevel + 1 == 2)
                            {
                                _WeaponUpgradesCount++;
                            }
                            break;

                        case "Weapon Speed":

                            Debug.Log(upgradeName + " " + upgradeLevel);
                            selectedWeaponHolder.gameObject.GetComponent<DiscHolder>().UpgradeDiscRotationSpeed(DiscRotaionSpeedBonus[upgradeLevel]);

                            if (upgradeLevel + 1 == 1)
                            {
                                Upgrades.Add(upgradeData.NextUpgrade);
                            }

                            if (upgradeLevel + 1 == 2)
                            {
                                _WeaponUpgradesCount++;
                            }

                            break;
                        case "Disc Awakening":

                            Debug.Log(upgradeName + " " + upgradeLevel);
                            fullDisc.AwakenDiscAbility();

                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    switch (upgradeName)
                    {
                        case "Weapon Dmg":

                            Debug.Log(upgradeName + " " + upgradeLevel);
                            selectedWeapon.UpgradeWeaponDmg(WeaponDmgBonus[upgradeLevel]);
                            if (upgradeLevel + 1 == 3)
                            {
                                _WeaponUpgradesCount++;
                            }
                            else
                            {
                                Upgrades.Add(upgradeData.NextUpgrade);
                            }
                            break;

                        case "Weapon Scale":

                            Debug.Log(upgradeName + " " + upgradeLevel);
                            selectedWeapon.gameObject.transform.localScale =
                            new Vector2(selectedWeapon.gameObject.transform.localScale.x + WeaponScaleBonus[upgradeLevel],
                            selectedWeapon.gameObject.transform.localScale.y + WeaponScaleBonus[upgradeLevel]);

                            if (upgradeLevel + 1 == 1)
                            {
                                Upgrades.Add(upgradeData.NextUpgrade);
                            }

                            if (upgradeLevel + 1 == 2)
                            {
                                _WeaponUpgradesCount++;
                            }
                            break;

                        case "Weapon Speed":

                            Debug.Log(upgradeName + " " + upgradeLevel);
                            selectedWeaponHolder.gameObject.GetComponent<BulletHolder>().UpgradeFireRate(FireRateBonus[upgradeLevel]);

                            if (upgradeData.UpgradeLevel == 1)
                            {
                                Upgrades.Add(upgradeData.NextUpgrade);
                            }

                            if (upgradeData.UpgradeLevel == 2)
                            {
                                _WeaponUpgradesCount++;
                            }

                            break;

                        case "Slash Awakening":

                            Debug.Log(upgradeName + " " + upgradeLevel);
                            bulletHolder.ActivateSlashAwakening = true;

                            break;

                        case "Bullet Awakening":

                            Debug.Log(upgradeName + " " + upgradeLevel);
                            bulletHolder.ActivateBulletAwakening = true;

                            break;

                        default:
                            break;
                    }
                }

                break;

            default:
                break;
        }


        _AcquiredUpgrades.Add(upgradeData);
        Upgrades.Remove(upgradeData);
    }

    // Getting a list of 4 random upgrades to display it to the player
    public List<UpgradeData> GetUpgradesFromList(int upgradescount)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        bool isAwakeningInAcquiredUpgrades = _AcquiredUpgrades.Contains(_ChosenAwakening);
        bool isAwakeningInUpgrades = Upgrades.Contains(_ChosenAwakening);

        int previousRandomUpgradeIndex = 0;
        int randomUpgradeIndex = 0;

        int upgradesCounter = 0;

        if (upgradescount > Upgrades.Count)
            upgradescount = Upgrades.Count;

        if ((_WeaponUpgradesCount >= 2) && !isAwakeningInAcquiredUpgrades && !isAwakeningInUpgrades)
        {
            Debug.Log("the awakening working");
            upgradeList.Add(_ChosenAwakening);
            upgradesCounter = 1;
            while (upgradesCounter < upgradescount)
            {
                if (previousRandomUpgradeIndex != randomUpgradeIndex)
                {
                    upgradeList.Add(Upgrades[randomUpgradeIndex]);
                    previousRandomUpgradeIndex = randomUpgradeIndex;
                    upgradesCounter++;
                }
                else
                {
                    randomUpgradeIndex = Random.Range(1, Upgrades.Count);
                }
            }
        }
        else
        {

            Debug.Log("the awakening not working");
            while (upgradesCounter < upgradescount)
            {
                if (previousRandomUpgradeIndex != randomUpgradeIndex)
                {
                    upgradeList.Add(Upgrades[randomUpgradeIndex]);
                    previousRandomUpgradeIndex = randomUpgradeIndex;
                    upgradesCounter++;
                }
                else
                {
                    randomUpgradeIndex = Random.Range(0, Upgrades.Count);
                }
            }
        }

        return upgradeList;
    }

    public void HealPlayer()
    {
        PlayerGameObject.Health += HealingAmount[HealingLevelIndex];
        if (PlayerGameObject.Health > PlayerGameObject.MaxHealth)
            PlayerGameObject.Health = PlayerGameObject.MaxHealth;
    }

    private void ActivateWeapon(int weaponIndex)
    {
        Debug.Log("weaponIndex is " + weaponIndex);

        for (int i = 0; i < WeaponList.Length -1; i++)
        {
            WeaponList[i].SetActive(false);
        }

        WeaponList[weaponIndex].SetActive(true);
        _ChosenAwakening = AwakeningUpgrades[weaponIndex];
    }

    public Weapon GetSelectedWeapon()
    {
        return GetSelectedWeaponHolder().WeaponToHold;
    }

    public Holder GetSelectedWeaponHolder()
    {
        return WeaponList[_SelectedWeapoonIndex].GetComponent<Holder>();
    }

    // this two function help to identify if the xeapon did crit or not and based on the result we change the floating text
    public bool GetWeaponCritCondition()
    {
        return WeaponDidCrit;
    }
    public bool SetWeaponCritCondition(bool condition)
    {
        return WeaponDidCrit = condition;
    }

}
