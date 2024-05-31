using UnityEngine;

public class Enemy : Fighter
{
    public FloatingText FlaotingTextParent;
    [HideInInspector] public bool EnemyIsDead = false;
    public int EnemyLevel = 1;
    public string EnemyName = "";

    private Transform _Player;
    private float _Step;

    protected override void Start()
    {
        base.Start();
        _Player = GameManager.Instance.PlayerGameObject.GetComponent<Transform>();
    }

    void Update()
    {
        // enemy follow the playe if the distance between them is more the .7
        if(Vector2.Distance(transform.position, _Player.position) > .7f)
        {
            _Step = MovementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _Player.position, _Step);
        }

    }

    // The player will recieve dmg the moment he enter the collision, if he stay in the collision and the moment he exit it
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("ReceiveDamage", _Damage);
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("ReceiveDamage", _Damage);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("ReceiveDamage", _Damage);
        }

    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   
    // Defining what happen when the nemy is dead
    protected override void Death()
    {
        Player player = GameManager.Instance.PlayerGameObject;

        // Adding experience to the player based on enemy level
        int experience = GameManager.Instance.EnemyExpAmounts[EnemyLevel - 1];
        GameManager.Instance.AddExperience(experience);

        // depanding on thenemy type or level player gain some amount of point that will be added to his score
        if (EnemyName == "Boss")
        {
            Debug.Log("Boss is dead");
            player.PlayerScore += 500;
            GameManager.Instance.EndGameMenuObject.ActivateEndGamePanel();
        }
        else if (EnemyName == "Higher Enemy")
        {
            player.PlayerScore += 250;
            player.IncreaseHigherEnemyScore();
        }
        else if (EnemyName == "Enemy Lvl 4")
            player.PlayerScore += 200;
        else if (EnemyName == "Enemy Lvl 3")
            player.PlayerScore += 150;
        else if (EnemyName == "Enemy Lvl 2")
            player.PlayerScore += 50;
        else 
            player.PlayerScore += 25;

        EnemyIsDead = true;
        // instaniate drop at the enemy position
        GetComponent<DropBag>().InstantiateDrop(transform);
        Destroy(gameObject);
    }
}


