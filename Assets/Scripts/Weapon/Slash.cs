using UnityEngine;

public class Slash : Projectil
{

    // the enemy recieve dmg only when the slash exit the collision
    private void OnTriggerExit2D(Collider2D collision)
    {
        int criteRate = GameManager.Instance.PlayerGameObject.CritRate;
        int criteDmg = GameManager.Instance.PlayerGameObject.CritDmg;

        int hitrate = Random.Range(1, 101);
        
        // update the dmg amount the enemy will recieve based on: is the attacked did crite or not and the crite dmg value
        if (collision.CompareTag("Enemy"))
            {
                if (hitrate <= criteRate) 
                {
                    GameManager.Instance.SetWeaponCritCondition(true);
                    float dmg = Mathf.Round(_WeaponDamage + _WeaponDamage * (criteDmg / 100));
                    collision.gameObject.SendMessage("ReceiveDamage", dmg);
                    DestroyBullet();
                }
                else
                {
                    GameManager.Instance.SetWeaponCritCondition(false);
                    collision.gameObject.SendMessage("ReceiveDamage", _WeaponDamage);
                    DestroyBullet();
                }

            }
    }

}
