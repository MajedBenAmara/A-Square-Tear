using UnityEngine;

public class BossProjectil : MonoBehaviour
{
    [SerializeField] int _ProjectilDmg = 2;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.gameObject.SendMessage("ReceiveDamage", _ProjectilDmg);

        }
    }
}
