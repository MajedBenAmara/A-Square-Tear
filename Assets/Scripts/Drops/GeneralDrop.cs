using UnityEngine;

public class GeneralDrop : MonoBehaviour
{
    [HideInInspector] public int ExpLevel;
    [HideInInspector] public string DropName;
    [SerializeField] private float _Speed = 4f, _GrabDistance = 4f;
    private float _Step;
    private Transform _Player;

    private void Start()
    {
        _Player = GameManager.Instance.PlayerGameObject.transform;
    }

    private void Update()
    {
        // the drop will move towards the player if his close to it
        if (Vector2.Distance(_Player.position, transform.position) <= _GrabDistance)
        {
            _Step = _Speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _Player.position, _Step);
        }

    }

    // if the drops collided with player it will grant him exp or healing based on it's type
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(DropName == "Exp")
            {
                AddExp();
            }
            else if(DropName == "Heal")
            {
                Heal();
            }
            Destroy(gameObject);
        }
    }

    // Grant an amount of experience to the player based on the exp level
    private void AddExp()
    {
        GameManager.Instance.AddExperience(GameManager.Instance.EnemyExpAmounts[ExpLevel - 1]);
    }

    private void Heal()
    {
        GameManager.Instance.HealPlayer();
    }
}
