using UnityEngine;


public enum UpgradeType
{
    StatUpgrade,
    WeaponUpgrade
}

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    public UpgradeType type;
    public Sprite Icon ;
    public string UpgradeName ;
    public int UpgradeLevel;
    public string Description;
    public UpgradeData NextUpgrade;
}
