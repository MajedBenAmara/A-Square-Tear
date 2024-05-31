using UnityEngine;

public class FullDisc : MonoBehaviour
{
    public Disc[] Discs;
    
    public void UpgradeScale(float amount)
    {
        foreach (var disc in Discs)
        {
            disc.transform.localScale = new Vector2(disc.transform.localScale.x + amount, disc.transform.localScale.y + amount);
        }
    }

    public void UpgradeWeaponDmg(int amount)
    {
        foreach (var disc in Discs)
        {
            disc._WeaponDamage += amount;
        }
    }

    // Increase the numbe of disc
    public void AwakenDiscAbility()
    {
        foreach(var disc in Discs)
        {
            if(disc.gameObject.activeInHierarchy == false)
            {
                disc.gameObject.SetActive(true);
            }
        }
    }
}
