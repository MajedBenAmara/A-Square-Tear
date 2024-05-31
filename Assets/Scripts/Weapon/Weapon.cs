using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float _WeaponDamage, _InitialWeaponDamage;

    // upgrade the dmg stat in the different weapons
    public void UpgradeWeaponDmg(float dmgBonus)
    {   
        _WeaponDamage += dmgBonus ;
    }


}
