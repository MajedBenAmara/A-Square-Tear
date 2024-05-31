using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] public float _Damage;
    [SerializeField] public int Health, MovementSpeed;
    [SerializeField] protected float _TimeBetweenHits;
    private float _HitMoment;
    protected Animator _Animator;


    protected virtual void Start()
    {
        _Animator = GetComponent<Animator>();
    }


    // Defining what happen when the fighter game object take/receive dmg
    protected virtual void ReceiveDamage(float dmg)
    {
        // _TimeBetweenHits : is a cooldown, when expired the fighter will recieve dmg
        // Time.time is the current time we're in 
        // _HitMoment is the moment the fighter Took dmg
        // we check if the duration between the current moment and the hit moment is less then the cooldown if yes we receive dmg if no we dont
        if (Time.time - _HitMoment > _TimeBetweenHits)
        {
            _HitMoment = Time.time;
            Health -= (int)dmg;
            _Animator.Play("Hurts");
            Invoke("PLayIdle", .5f);

            // if the fighter is an enemy then we instantiate a floating text that show the amount of dmg he took 
            if (this.CompareTag("Enemy"))
            {
                gameObject.GetComponent<Enemy>().FlaotingTextParent._TextInput = dmg.ToString();
                Instantiate(gameObject.GetComponent<Enemy>().FlaotingTextParent, transform.position, Quaternion.identity);
            }

            if (Health <= 0)
                Death();

        }

    }

    protected virtual void Death()
    {
    }

    private void PLayIdle()
    {
        _Animator.Play("Idle");
    }
}
