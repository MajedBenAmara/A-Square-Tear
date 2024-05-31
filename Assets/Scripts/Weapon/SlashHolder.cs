using UnityEngine;

public class SlashHolder : BulletHolder
{
    [SerializeField] private Transform _SecondShootPoint;

    private void Start()
    {
        // initialize the different stats (scale, speed(rotaion speed and fire rate), dmg) of the different weapons(disc, slash,bullet)
        WeaponToHold.gameObject.transform.localScale = Vector3.one * 2;
        WeaponToHold.gameObject.GetComponent<Projectil>().ProjectilSpeed = WeaponToHold.gameObject.GetComponent<Projectil>().InitialProjectilSpeed;
        WeaponToHold._WeaponDamage = WeaponToHold._InitialWeaponDamage;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // Fire another slash behind the player
    public void SlashAwakening()
    {
        Quaternion rotation2 = Quaternion.AngleAxis(_Angle + _AngleOffset, Vector3.forward);
        Instantiate(WeaponToHold, _SecondShootPoint.position, rotation2);
    }
}
