using UnityEngine;

public class Projectil : Weapon
{
    public float ProjectilSpeed = 2f, InitialProjectilSpeed = 2f;
    [SerializeField] protected float _LifeDuration = 5f;


    protected virtual void Start()
    {
        // destroy the projectil after it's life duration expair
        Invoke("DestroyBullet", _LifeDuration);
    }
    protected virtual void Update()
    {
        transform.Translate(Vector2.right * ProjectilSpeed * Time.deltaTime);
    }

    protected virtual void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
