using UnityEngine;

public class BulletHolder : Holder
{

    [SerializeField] protected  Transform _ShootPoint;
    [SerializeField] protected float _TimeBetweenBullet = 1f, _XOffset = 1f, _AngleOffset;
    [SerializeField] protected AudioPlayer _AudioPlayer;
    protected float _ShootMoment;
    protected Quaternion _Rotaion;
    protected float _Angle;
    [HideInInspector] public bool ActivateSlashAwakening = false, ActivateBulletAwakening = false;

    private Vector3 _MousePosition, _ShootDirection;

    private void Start()
    {
        // initialize the different stats (scale, speed(rotaion speed and fire rate), dmg) of the different weapons(disc, slash,bullet)
        WeaponToHold.gameObject.transform.localScale = Vector3.one / 2;
        WeaponToHold.gameObject.GetComponent<Projectil>().ProjectilSpeed = WeaponToHold.gameObject.GetComponent<Projectil>().InitialProjectilSpeed;
        WeaponToHold._WeaponDamage = WeaponToHold._InitialWeaponDamage;
        // initialize the shoot point(the point where the projectile will be fired from) position
        _ShootPoint.position = new Vector2(transform.position.x + _XOffset, _ShootPoint.position.y);
    }

    protected virtual void FixedUpdate()
    {
        // getting the mouse coor in world space coordiante system
        _MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // calculating the direction from the weapon holder to the mouse
        _ShootDirection = (_MousePosition - transform.position).normalized;
        // calculating the produced angle by the shooting direction vector annd transforming into into degree
        _Angle = Mathf.Atan2(_ShootDirection.y, _ShootDirection.x) * Mathf.Rad2Deg;
        // Creating a rotation using the angle that we previously made
        _Rotaion = Quaternion.AngleAxis(_Angle, Vector3.forward);
        // We rotate the wheapon holder using that rotation
        transform.rotation = _Rotaion;

        if (Time.time - _ShootMoment > _TimeBetweenBullet)
            {
                _ShootMoment = Time.time;
                
                Instantiate(WeaponToHold, _ShootPoint.position, _Rotaion);
                
                if (WeaponToHold.name == "Bullet")
                {
                    if (ActivateBulletAwakening)
                        BulletAwakening();
                    _AudioPlayer.PlayOnBulletFire();
                }
                else if (WeaponToHold.name == "Slash")
                {
                    if(ActivateSlashAwakening)
                        gameObject.GetComponent<SlashHolder>().SlashAwakening();
                    _AudioPlayer.PlayOnSlashShoot();
                }
            }

    }


    // Shoot 3 bullets rather then one 
    private void BulletAwakening()
    {
        Quaternion rotaion1 = Quaternion.AngleAxis(_Angle + _AngleOffset, Vector3.forward);
        Instantiate(WeaponToHold, _ShootPoint.position, rotaion1);
        Quaternion rotaion2 = Quaternion.AngleAxis(_Angle - _AngleOffset, Vector3.forward);
        Instantiate(WeaponToHold, _ShootPoint.position, rotaion2);
    }

    public void UpgradeFireRate(float amount)
    {
        _TimeBetweenBullet -= amount;
    }
}
